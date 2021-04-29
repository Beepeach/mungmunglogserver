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
    [Route("api/familyMember")]
    //[Authorize]
    [ApiController]
    public class FamilyMemberApiController : CommonApiController
    {
        public FamilyMemberApiController(UserManager<User> userManager,
           SignInManager<User> signInManager,
           ApplicationDbContext context,
           IConfiguration configuration,
           IHostEnvironment environment) : base(userManager, signInManager, context, configuration, environment)
        {

        }
        // GET: api/familyMember/{familyMemberId}
        [HttpGet("{familyMemberId}")]
        public async Task<ActionResult<ListResponse<FamilyMemberDto>>> GetFamilyMember(int familyMemberId)
        {
            var familyMembers = await _context.FamilyMember
                .Where(m => m.FamilyMemberId == familyMemberId)
                .Include(m => m.Histories)
                .Include(m => m.WalkHistories)
                .Select(m => new FamilyMemberDto(m))
                .ToListAsync();

            return new ListResponse<FamilyMemberDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Get FamilyMemeber",
                List = familyMembers
            };
        }


        // GET: api/familyMember/list/{familyId}
        [HttpGet("list/{familyId}")]
        public async Task<ActionResult<ListResponse<FamilyMemberDto>>> GetFamilyMembers(int familyId)
        {
            var familyMembers = await _context.FamilyMember
                .Where(m => m.FamilyId == familyId)
                .Include(m => m.Histories)
                .Include(m => m.WalkHistories)
                .Select(m => new FamilyMemberDto(m))
                .ToListAsync();

            return new ListResponse<FamilyMemberDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Get FamilyMemeber List",
                List = familyMembers
            };
        }


        // PUT: api/familyMember/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<CommonResponse>> PutFamilyMember(int id, FamilyMemberPutModel model)
        {
            var targetMember = await _context
                .FamilyMember
                .Where(m => m.FamilyMemberId == id)
                .FirstOrDefaultAsync();

            if (targetMember == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The TargetMember"
                };
            }

            _context.Entry(targetMember).State = EntityState.Modified;

            targetMember.IsMaster = model.IsMaster;
            targetMember.Status = model.Status;

            try
            {
                await _context.SaveChangesAsync();

                return new CommonResponse
                {
                    Code = Models.StatusCode.Ok,
                    Message = "Success Put TargetMember"
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyMemberExists(id))
                {
                    return new CommonResponse
                    {
                        Code = Models.StatusCode.NotFound,
                        Message = "Not Found The TargetMember"
                    };
                } else
                {
                    return new CommonResponse
                    {
                        Code = Models.StatusCode.Unknown,
                        Message = "Unknown Error"
                    };
                }
            }
        }

        // POST: api/familyMember
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SingleResponse<FamilyMemberDto>>> PostFamilyMember(FamilyMemberPostModel model)
        {
            var family = await _context.Family.Where(f => f.InvitationCode == model.Code).FirstOrDefaultAsync();

            if (family == null)
            {
                return new SingleResponse<FamilyMemberDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found the Family For Code"
                };
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new SingleResponse<FamilyMemberDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found the User"
                };
            }

            user.FamilyId = family.FamilyId;

            var familyMember = new FamilyMember
            {
                IsMaster = false,
                Status = 2,
                UserId = user.Id,
                FamilyId = family.FamilyId
            };

            _context.FamilyMember.Add(familyMember);
            await _context.SaveChangesAsync();

            return new SingleResponse<FamilyMemberDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Post FamilyMember",
                Data = new FamilyMemberDto(familyMember)
            };
        }

        // DELETE: api/familyMember/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommonResponse>> DeleteFamilyMember(int id)
        {
            var familyMember = await _context.FamilyMember.FindAsync(id);
            if (familyMember == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The FamilyMember"
                };
            }
            _context.FamilyMember.Remove(familyMember);
            await _context.SaveChangesAsync();

            return new CommonResponse
            {
                Code = Models.StatusCode.Ok,
                Message = "Success to Delete FamilyMember"
            };
        }

        private bool FamilyMemberExists(int id)
        {
            return _context.FamilyMember.Any(e => e.FamilyMemberId == id);
        }
    }
}
