using Ploomes.API.DTO;
using Ploomes.API.Models;

namespace Ploomes.API.Mapper
{
    public interface IUserMapper
    {
        User ToUser(UserDTO userDTO);
        UserDTO ToUserDTO(User user);
        IEnumerable<UserDTO> ToUserDTO(IEnumerable<User> users);
    }
}