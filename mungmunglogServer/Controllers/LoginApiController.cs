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
            IHostEnvironment environment) : base(userManager, signInManager, context, configuration, environment)
        {

        }


        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailLoginRequestModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser == null)
            {
                return Ok(new LoginResponseModel
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found User"
                });
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var familyMember = _context.FamilyMember
                    .Where(m => m.UserId == existingUser.Id)
                    .Select(m => new FamilyMemberDto(m))
                    .FirstOrDefault();

                var token = GetApiToken(existingUser);
                if (!token.Contains("fail"))
                {
                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.Ok,
                        Message = "Login Succeed",
                        Email = model.Email,
                        Token = token,
                        User = existingUser,
                        FamilyMember = familyMember
                    });
                }
                else
                {
                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.TokenError,
                        Message = "Fail to Create Token"
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
                    var familyMember = _context.FamilyMember
                   .Where(m => m.UserId == user.Id)
                   .Select(m => new FamilyMemberDto(m))
                   .FirstOrDefault();

                    var token = GetApiToken(user);
                    if (!token.Contains("fail"))
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Ok,
                            Message = "SNS ????????? ??????",
                            Email = model.Email,
                            Token = token,
                            User = user,
                            FamilyMember = familyMember
                        });

                    }
                    else
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.TokenError,
                            Message = "Token ?????? ??????"
                        });
                    }
                }
                else
                {
                    //kakao?????? ????????? ???????????? ????????? ?????? FindByEmail
                    var existingUser = await _userManager.FindByLoginAsync(model.Provider, model.Id); //.FindByEmailAsync(model.Email);

                    if (existingUser != null)
                    {
                        // ?????? ????????? ???????????? ????????? ?????? ????????? ????????? ????????????????????

                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Fail,
                            Message = "?????? ????????? ???????????????."
                        });
                    }

                    var newUser = new User
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        Nickname = "",
                        Relationship = "",
                        Gender = true,
                        FileUrl = "",
                        // ????????? ?????? ?????? - model??? ???????????? ??????
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
                                    Message = "SNS ????????? ??????",
                                    Email = newUser.Email,
                                    Token = token,
                                    User = newUser
                                    
                                });
                            }
                            else
                            {
                                return Ok(new LoginResponseModel
                                {
                                    Code = Models.StatusCode.Fail,
                                    Message = "Token ?????? ??????"
                                });
                            }
                        }
                        else
                        {
                            return Ok(new LoginResponseModel
                            {
                                Code = Models.StatusCode.Fail,
                                Message = "????????? ?????? ??????"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Fail,
                            Message = "?????? ?????? ??????"
                        });
                    }
                }
            }
            else
            {
                //kakao?????? ????????? ???????????? ????????? ?????? FindByEmail
                var existingUser = await _userManager.FindByLoginAsync(model.Provider, model.Id); //.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    // ?????? ????????? ???????????? ????????? ?????? ????????? ????????? ????????????????????

                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.Fail,
                        Message = "?????? ????????? ???????????????."
                    });
                }

                var newUser = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Nickname = "",
                    Relationship = "",
                    Gender = true,
                    FileUrl = "",
                    // ????????? ?????? ?????? - model??? ???????????? ??????
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
                                Message = "SNS ????????? ??????",
                                Email = newUser.Email,
                                Token = token,
                                User = newUser
                            });
                        }
                        else
                        {
                            return Ok(new LoginResponseModel
                            {
                                Code = Models.StatusCode.Fail,
                                Message = "Token ?????? ??????"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new LoginResponseModel
                        {
                            Code = Models.StatusCode.Fail,
                            Message = "????????? ?????? ??????"
                        });
                    }
                }
                else
                {
                    return Ok(new LoginResponseModel
                    {
                        Code = Models.StatusCode.Fail,
                        Message = "?????? ?????? ??????"
                    });
                }
            }
        }
    }
}