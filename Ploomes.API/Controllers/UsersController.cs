using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ploomes.API.DTO;
using Ploomes.API.Services.Authentication.Interfaces;

namespace Ploomes.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }



        // POST: Users/CreateUser
        [HttpPost]
        public async Task<ActionResult<AuthenticatedUserDTO>> CreateUser(UserDTO userDTO)
        {
            try
            {
                var userNovo = await _authenticationService.CreateUser(userDTO);
                return Created("CreateEstudante", userNovo);
            }
            catch (Exception e)
            {
                userDTO.Password = string.Empty;
                return UnprocessableEntity(new { errors = e.Message, userDTO });
            }
        }

        // Users/GetUsers
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            IEnumerable<UserDTO> users = await _authenticationService.GetUsers();
            return base.Ok(users);
        }
    }
}