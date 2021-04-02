using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{

    [Route("api/login")]
    [ApiController]
    public class LoginApiController : CommonApiController
    {
        public LoginApiController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration):base(userManager, signInManager, configuration)
        {

        }


        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailLoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = GetApiToken(user);
                    if (!token.Contains("fail"))
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Ok,
                            Message = "Login Succeed",
                            UserId = user.Id,
                            Token = token
                        });
                    }
                    else
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Fail,
                            Message = "Fail to Create Token"
                        });
                    }
                }
                else
                {
                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.NotFound,
                        Message = "Not Found User"
                    });
                }
            }

            return Ok(new LoginResponseModel
            {
                Code = Models.StatusCode.Fail,
                Message = "Fail to Login"
            });
        }
    }
}