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
    [Route("api/join")]
    [ApiController]
    public class JoinApiController : CommonApiController
    {
        public JoinApiController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration,
            IHostEnvironment environment): base(userManager, signInManager, context, configuration, environment)
        {

        }

        [HttpGet("dummy")]
        public async Task<IActionResult> GetDummy()
        {
            var dummyUser = new User
            {
                Email = "test1@test.com",
                Id = "test1@test.com",
                NickName = "TestUser",
                Relationship = "Bro",
                Gender = true,
                UserName = "TestUser"
            };

            var result = await _userManager.CreateAsync(dummyUser, "Test1!");

            if (result.Succeeded)
            {
                var token = GetApiToken(dummyUser);
                if (!token.Contains("fail"))
                {
                    return Ok(new JoinResponseModel
                    {
                        Code = Models.StatusCode.Ok,
                        Message = "회원가입 성공",
                        UserId = dummyUser.Id,
                        Token = token
                    });
                }
                else
                {
                    return Ok(new JoinResponseModel
                    {
                        Code = Models.StatusCode.Fail,
                        Message = "Token 생성 실패"
                    });
                }
            }

            return Ok(new JoinResponseModel
            {
                Code = Models.StatusCode.Fail,
                Message = result.Errors.FirstOrDefault()?.Description
            });
        }



        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailJoinModel model)
        {
            var newUser = new User
            {
                Email = model.Email,
                UserName = model.Email,
                NickName = "",
                Relationship = "",
                Gender = true,
                FileUrl = ""
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                var token = GetApiToken(newUser);
                if (!token.Contains("fail"))
                {
                    return Ok(new JoinResponseModel
                    {
                        Code = Models.StatusCode.Ok,
                        Message = "회원가입 성공",
                        UserId = newUser.Id,
                        Token = token
                    });
                } else
                {
                    return Ok(new JoinResponseModel
                    {
                        Code = Models.StatusCode.Fail,
                        Message = "Token 생성 실패"
                    });
                }
            }

            return Ok(new JoinResponseModel
            {
                Code = Models.StatusCode.Fail,
                Message = result.Errors.FirstOrDefault()?.Description
            });
        }

        [HttpPost("emailinfo")]
        public async Task<IActionResult> PostEmailInfo(EmailInfoJoinModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                user.NickName = model.NickName;
                user.Relationship = model.Relationship;
                user.Gender = model.Gender;
                user.FileUrl = model.FileUrl;

                await _userManager.UpdateAsync(user);

                return Ok(new JoinResponseModel
                {
                    Code = Models.StatusCode.Ok,
                    Message = "정보 등록 성공"
                });
            } else
            {
                return Ok(new JoinResponseModel
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "존재하지 않는 회원"
                });
            }
        }
    }
}