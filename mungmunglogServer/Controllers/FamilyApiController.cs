using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/family")]
    [ApiController]
    public class FamilyApiController : CommonApiController
    {
    
        public FamilyApiController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration,
            IHostEnvironment environment) : base(userManager, signInManager, context, configuration, environment)
        {

        }

        // 잠시 보류
        //// GET: api/family/list
        //[HttpGet("list")]
        //public async Task<ActionResult<ListResponse<Family>>> GetFamily(int familyMemberId)
        //{
        //    var familyMember = await _context.Family.fin

        //    return await _context.Family.ToListAsync();
        //}

        //// GET: api/family/list
        //[HttpGet("list/master")]
        //public async Task<ActionResult<ListResponse<Family>>> GetMasterFamily()
        //{
        //    return await _context.Family.ToListAsync();
        //}

        // GET: api/family/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetFamily(int id)
        {
            var family = await _context.Family.FindAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return family;
        }

        private static Random random = new Random();

        public static string RandomString(int lentgh)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";

            return new string(Enumerable.Repeat(chars, lentgh)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet("invitation/{id}")]
        public async Task<ActionResult<SingleResponse<string>>> GetGenerateInvitationCode(int id)
        {
            var family = await _context.Family.FindAsync(id);

            if (family == null)
            {
                return Ok(new SingleResponse<string>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The Family"
                });
            }

            family.InvitationCode = RandomString(8);
            family.CodeExpirationDate = DateTime.UtcNow.AddHours(1);

            await _context.SaveChangesAsync();

            return Ok(new SingleResponse<string>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success",
                Data = family.InvitationCode
            });
        }

        [HttpPost("invitation")]
        public async Task<ActionResult<CommonResponse>> PostInvitaionCode(string code, string email)
        {
            var family = await _context.Family.Where(f => f.InvitationCode == code).FirstOrDefaultAsync();

            if (family == null)
            {
                return Ok(new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Invalid Code"
                });
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found User"
                });
            }

            var familyMember = new FamilyMember
            {
                IsMaster = false,
                Status = 2,
                UserId = user.Id,
                FamilyId = family.FamilyId
            };

            user.FamilyId = family.FamilyId;

            _context.FamilyMember.Add(familyMember);
            await _context.SaveChangesAsync();

            return Ok(new CommonResponse
            {
                Code = Models.StatusCode.Ok,
                Message = "Success to Request Invitation"
            });
        }

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.FamilyId == id);
        }
    }
}
