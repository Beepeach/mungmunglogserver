using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserApiController : CommonApiController
    {
        public UserApiController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration,
            IHostEnvironment environment
            ): base(userManager, signInManager, context, configuration, environment)
        {

        }

        [HttpGet]
        public async Task<ActionResult<ListResponse<User>>> GetUsers()
        {
            var users = await _context.Users
                .OrderBy(u => u.NickName)
                .ThenBy(u => u.Id)
                .ToListAsync();

            return Ok(new ListResponse<User>
            {
                Code = Models.StatusCode.Ok,
                Message = "회원 목록 API 호출 성공",
                List = users

            });
        }
    }
}