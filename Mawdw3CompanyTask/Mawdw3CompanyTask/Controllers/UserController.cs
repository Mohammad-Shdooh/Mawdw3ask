using BAL.Interfaces;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public IActionResult SignUp([FromBody] User user)
        {
            var userResponse = userService.SignUp(user);
            return Ok(userResponse);

        }
        // here is tha api for public APi given this api get and add the data separated in two services .
        [HttpGet("getUserFromExternalPublicSource")]
        public async Task<IActionResult> getUserFromExternalPublicSource()
        {
            var user = await userService.getUserFromExternalPublicSource();
            if(user != null )
            {
                return Ok(userService.SignUp(user) ); 
            }
            return BadRequest();
            
        }


    }
}
