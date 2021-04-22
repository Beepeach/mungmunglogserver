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
    [Route("api/user")]
    [ApiController]
    //[Authorize]
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

        // api/user/list
        [HttpGet("list")]
        public async Task<ActionResult<ListResponse<User>>> GetUsers()
        {
            var users = await _context.Users
                .OrderBy(u => u.Nickname)
                .ThenBy(u => u.Id)
                .ToListAsync();

            return Ok(new ListResponse<User>
            {
                Code = Models.StatusCode.Ok,
                Message = "회원 목록 API 호출 성공",
                List = users

            });
        }

        // Post: api/user/familyMemberId
        [HttpGet("{familyMemberId}")]
        public async Task<ActionResult<SingleResponse<User>>> GetUserFromFamilyMember(int familyMemberId)
        {
            var familyMember = _context.FamilyMember.Where(m => m.FamilyMemberId == familyMemberId).FirstOrDefault();

            if (familyMember == null)
            {
                return NotFound(new SingleResponse<FamilyMemberDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The Memeber"
                });
            }

            var user = await _context.Users.Where(u => u.Id == familyMember.UserId).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new SingleResponse<User>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The User"
                });
            }

            return Ok(new SingleResponse<User>
            {
                Code = Models.StatusCode.Ok,
                Message = "Find The User",
                Data = user
            }); 
        }
    }
}