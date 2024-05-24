using ApiForCrud.Model;
using ApiForCrud.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForCrud.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService service) {
          this.userService = service;
        }

        [HttpPost("userregisteration")]

        public async Task<IActionResult> UserRegisteration(UserRegister userRegister)
        {
            var data =  await this.userService.UserRegisteration(userRegister);
            return Ok(data);
        }

        [HttpPost("confrimregisteration")]
        public async  Task<IActionResult> confrimregisteration (Confirmpassword confirmpassword)
        {
            var data = await this.userService.ConfrimRegister(confirmpassword.userid, confirmpassword.username,confirmpassword.otptext);
            return Ok(data);
        }


        [HttpPost("resetpassword")]
        public async Task<IActionResult> resetpassword(string username, string oldPassword, string NewPassword)
        {
            var data = await this.userService.ResetPassword(username, oldPassword,NewPassword);
            return Ok(data);
        }

        [HttpPost("forgetpassword")]
        public async Task<IActionResult> forgetpassword(string username)
        {
            var data = await this.userService.ForgetPassword(username);
            return Ok(data);
        }

        [HttpPost("updatepassword")]
        public async Task<IActionResult> updatepassword(string username, string password,string otpText)
        {
            var data = await this.userService.UpdatePassword(username,password,otpText);
            return Ok(data);
        }
        [HttpPost("updatestatus")]
        public async Task<IActionResult> updatestatus(string username, bool userstatus)
        {
            var data = await this.userService.UpdateStatus(username, userstatus);
            return Ok(data);
        }
        [HttpPost("updaterole")]
        public async Task<IActionResult> updaterole(string username, string userrole)
        {
            var data = await this.userService.UpdateRole(username, userrole);
            return Ok(data);
        }
    }
}
