using Digital_Core_Layer.Services.Abstract;
using Digital_Core_Layer.Services.Concrete;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUp([FromBody] RegisterModel model)
        {
            var response = await userService.Register(model);

            return Ok(response);

        }

        [HttpPost("SignIn")]
        public async Task<ActionResult> SignIn([FromBody] LoginModel model)
        {
            var response = await userService.Login(model);

            return Ok(response);

        }

    }
}
