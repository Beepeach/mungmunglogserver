using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{

    [Route("api/login")]
    [ApiController]
    public class LoginApiController : CommonApiController
    {
        public LoginApiController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration,
            IHostEnvironment environment):base(userManager, signInManager, context, configuration, environment)
        {

        }


        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailLoginRequestModel model)
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

        [HttpPost("sns")]
        public async Task<IActionResult> PostSNS(SNSLoginRequestModel model)
        {
            var result = await _signInManager.ExternalLoginSignInAsync(model.Provider, model.Id, false);

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
                            Message = "SNS 로그인 성공",
                            UserId = user.Id,
                            Token = token
                        });

                    }
                    else
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Fail,
                            Message = "Token 생성 실패"
                        });
                    }
                } else
                {
                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.NotFound,
                        Message = "사용자를 찾지 못함"
                    });
                }
            }
            else
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    // 같은 메일이 존재하면 로그인 추가 기능은 어떻게 구현해야할까??

                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.Fail,
                        Message = "이미 가입한 계정입니다."
                    });
                }

                var newUser = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    NickName = "",
                    Relationship = "",
                    Gender = true,
                    FileUrl = "",
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(newUser);

                if (createResult.Succeeded)
                {
                    var loginInfo = new UserLoginInfo(model.Provider, model.Id, model.Email);
                    var addLoginResult = await _userManager.AddLoginAsync(newUser, loginInfo);

                    if (addLoginResult.Succeeded)
                    {
                        var token = GetApiToken(newUser);

                        if (!token.Contains("fail"))
                        {
                            return Ok(new LoginResponseModel
                            {
                                Code = Models.StatusCode.Ok,
                                Message = "SNS 로그인 성공",
                                UserId = newUser.Id,
                                Token = token
                            });
                        }
                        else
                        {
                            return Ok(new LoginResponseModel
                            {
                                Code = Models.StatusCode.Fail,
                                Message = "Token 생성 실패"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Fail,
                            Message = "로그인 생성 실패"
                        });
                    }
                }
                else
                {
                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.Fail,
                        Message = "유저 생성 실패"
                    });
                }
            }
        }
    }
}