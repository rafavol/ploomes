using Ploomes.API.DTO;
using Ploomes.API.Exceptions;
using Ploomes.API.Helpers;
using Ploomes.API.Infra.UnitOfWork;
using Ploomes.API.Mapper;
using Ploomes.API.Models;
using Ploomes.API.Services.Authentication.Interfaces;
using System.Linq;

namespace Ploomes.API.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserMapper _mapper;
        private readonly string salt = Environment.GetEnvironmentVariable("HASH_SALT");

        public AuthenticationService(IUnitOfWork uow, IUserMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        private async Task<User> CreateUserDB(User user)
        {
            user.GenerateHash();
            user.PrimeiroLogin = true;
            var userDB = await _uow.UserRepository.Create(user);
            return userDB;
        }

        public async Task<AuthenticatedUserDTO> CreateUser(UserDTO userDTO)
        {

            await VerificaUserName(userDTO.UserName);

            var user = _mapper.ToUser(userDTO);
            var userDB = await CreateUserDB(user);           

            var userDTONovo = _mapper.ToUserDTO(userDB);

            return await Login(userDTO.UserName, userDTO.Password);

        }

        private async Task VerificaUserName(string username)
        {
            if(await _uow.UserRepository.VerificaUserNameDisponivel(username))
            {
                throw new DuplicatedEntryException("Já existe usuário com esse nome");
            }

        }

        public async Task<AuthenticatedUserDTO> Login(string userName, string password)
        {
            var userDB = await _uow.UserRepository.GetByUserName(userName);

            if (!IsValidUser(userDB, password))
            {
                return null;   
            }
            var usuario = await _uow.UserRepository.GetInfoByUserId(userDB.Id);
            var authenticatedUser = new AuthenticatedUserDTO
            {
                UserName = userDB.UserName,
                Nome = usuario.NomeCompleto,
                Role = usuario.GetType().Name,
                PrimeiroLogin = userDB.PrimeiroLogin,
            };

            authenticatedUser.GenerateToken();
            userDB.PrimeiroLogin = false;
            await _uow.UserRepository.Update(userDB.Id, userDB);
            authenticatedUser.GenerateToken();
            return authenticatedUser;
        }

        private bool IsValidUser(User user, string password)
        {
            if (user is null)
            {
                return false;
            }
            return user.Password.Equals(HashGenerator.ComputeHash(password, user.Salt));
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var usersDB = _mapper.ToUserDTO(await _uow.UserRepository.GetUsers());
            return usersDB;
        }
    }
}
