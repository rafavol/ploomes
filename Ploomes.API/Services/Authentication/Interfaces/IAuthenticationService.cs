
using Ploomes.API.DTO;

namespace Ploomes.API.Services.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<AuthenticatedUserDTO> CreateUser(UserDTO userDTO);
        Task<IEnumerable<UserDTO>> GetUsers();
        public Task<AuthenticatedUserDTO> Login(string userName, string password);
    }
}