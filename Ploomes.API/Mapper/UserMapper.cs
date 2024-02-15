using Ploomes.API.DTO;
using Ploomes.API.Models;

namespace Ploomes.API.Mapper
{
    public class UserMapper : IUserMapper
    {
        public UserMapper()
        {

        }

        public UserDTO ToUserDTO(User user)
        {
            var userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                DataNascimento = user.DataNascimento,
                NomeCompleto = user.NomeCompleto,
            };
            return userDTO;
        }

        public User ToUser(UserDTO userDTO)
        {
            var user = new User
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                DataNascimento = userDTO.DataNascimento,
                NomeCompleto = userDTO.NomeCompleto,
                Password = userDTO.Password
            };
            return user;
        }

        public IEnumerable<UserDTO> ToUserDTO(IEnumerable<User> users)
        {
            return users.Select(ToUserDTO);
        }
    }
}
