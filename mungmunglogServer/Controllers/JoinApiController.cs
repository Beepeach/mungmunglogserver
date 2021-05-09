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


        [HttpPost("email")]
        public async Task<IActionResult> PostEmail(EmailJoinRequestModel model)
        {
            var duplicationUser = await _userManager.FindByEmailAsync(model.Email);

            if (duplicationUser != null)
            {
                return Ok(new JoinResponseModel
                {
                    Code = Models.StatusCode.FailWithDuplication,
                    Message = "존재하는 메일입니다."
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

                // 이메일 검증은 추후에 변경
                EmailConfirmed = true
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
                        Email = newUser.Email,
                        User = newUser,
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
                Code = Models.StatusCode.Unknown,
                Message = result.Errors.FirstOrDefault()?.Description
            });
        }

        [HttpPost("info")]
        public async Task<IActionResult> PostInfo(JoinInfoRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                user.Nickname = model.Nickname;
                user.Relationship = model.Relationship;
                user.Gender = model.Gender;
                user.FileUrl = model.FileUrl;

                await _userManager.UpdateAsync(user);

                return Ok(new JoinResponseModel
                {
                    Code = Models.StatusCode.Ok,
                    Message = "정보 등록 성공",
                    User = user
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