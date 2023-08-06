using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraIA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUser()
        {
            var users = _userService.GetAllUser();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO user)
        {
            var userCreated = await _userService.AddUser(user);

            return Ok(userCreated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] string id)
        {
            await _userService.DeleteUser(id);

            return Ok();
        }

    }
}
