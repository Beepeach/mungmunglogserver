using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{
    [Route("api/walkHistory")]
    [ApiController]
    public class WalkHistoryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WalkHistoryApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/walkHistory/list/pet/petId
        [HttpGet("list/pet/{petId}")]
        public async Task<ActionResult<ListResponse<WalkHistoryDto>>> GetPetWalkHistories(int petId)
        {
            var walkHistories = await _context.WalkHistory
                .Where(h => h.PetId == petId)
                .Select(h => new WalkHistoryDto(h))
                .ToListAsync();

            if (walkHistories == null)
            {
                return new ListResponse<WalkHistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found WalkHistorys"
                };
            }

            return new ListResponse<WalkHistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success To Get WalkHistorys"
            };
        }

        // GET: api/walkHistory/list/pet/petId
        [HttpGet("list/pet/{memberId}")]
        public async Task<ActionResult<ListResponse<WalkHistoryDto>>> GetMemberWalkHistories(int memberId)
        {
            var walkHistories = await _context.WalkHistory
                .Where(h => h.PetId == memberId)
                .Select(h => new WalkHistoryDto(h))
                .ToListAsync();

            if (walkHistories == null)
            {
                return new ListResponse<WalkHistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found WalkHistorys"
                };
            }

            return new ListResponse<WalkHistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success To Get WalkHistorys"
            };
        }

        // GET: api/walkHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleResponse<WalkHistoryDto>>> GetWalkHistory(int id)
        {
            var walkHistory = await _context.WalkHistory.FindAsync(id);

            if (walkHistory == null)
            {
                return new SingleResponse<WalkHistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The WalkHistory"
                };
            }

            return new SingleResponse<WalkHistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success To Get The WalkHistory",
                Data = new WalkHistoryDto(walkHistory)
            };
        }

        // PUT: api/walkHistory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<CommonResponse>> PutWalkHistory(int id, WalkHistoryPutModel model)
        {
            var walkHistory = await _context.WalkHistory.Where(h => h.WalkHistoryId == id).FirstOrDefaultAsync();

            if (walkHistory == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The WalkHistory"
                };
            }

            walkHistory.StartTime = new DateTime((long)model.StartTime * 10_000_000);
            walkHistory.EndTime = new DateTime((long)model.EndTime * 10_000_000);
            walkHistory.Distance = model.Distance;
            walkHistory.Contents = model.Contents;
            walkHistory.FileUrl1 = model.FileUrl1;
            walkHistory.FileUrl1 = model.FileUrl2;
            walkHistory.FileUrl1 = model.FileUrl3;
            walkHistory.FileUrl1 = model.FileUrl4;
            walkHistory.FileUrl1 = model.FileUrl5;

            _context.Entry(walkHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return new CommonResponse
                {
                    Code = Models.StatusCode.Ok,
                    Message = "Success To Put The WalkHistory"
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalkHistoryExists(id))
                {
                    return new CommonResponse
                    {
                        Code = Models.StatusCode.NotFound,
                        Message = "Not Found The WalkHistory"
                    };
                }
                else
                {
                    return new CommonResponse
                    {
                        Code = Models.StatusCode.Unknown,
                        Message = "Unknown Error"
                    };
                }
            }
        }

        // POST: api/walkHistory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SingleResponse<WalkHistoryDto>>> PostWalkHistory(WalkHistoryPostModel model)
        {
            var pet = await _context.Pet
                .Where(p => p.PetId == model.PetId)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                return new SingleResponse<WalkHistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The Pet"
                };
            }

            var familyMember = await _context.FamilyMember.
                Where(m => m.FamilyMemberId == model.FamilyMemberId)
                .FirstOrDefaultAsync();

            if (familyMember == null)
            {
                return new SingleResponse<WalkHistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The FamilyMember"
                };
            }

            var newWalkHistory = new WalkHistory
            {
                StartTime = new DateTime((long)model.StartTime * 10_000_000),
                EndTime = new DateTime((long)model.EndTime * 10_000_000),
                Distance = model.Distance,
                Contents = model.Contents,
                FileUrl1 = model.FileUrl1,
                FileUrl2 = model.FileUrl2,
                FileUrl3 = model.FileUrl3,
                FileUrl4 = model.FileUrl4,
                FileUrl5 = model.FileUrl5,
                PetId = model.PetId,
                FamilyMemberId = model.FamilyMemberId
            };

            _context.WalkHistory.Add(newWalkHistory);
            await _context.SaveChangesAsync();

            pet.WalkHistories.Add(newWalkHistory);
            familyMember.WalkHistories.Add(newWalkHistory);
            await _context.SaveChangesAsync();

            return new SingleResponse<WalkHistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Post",
                Data = new WalkHistoryDto(newWalkHistory)
            };

        }

        // DELETE: api/walkHistory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommonResponse>> DeleteWalkHistory(int id)
        {
            var walkHistory = await _context.WalkHistory.FindAsync(id);
            if (walkHistory == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The WalkHistory"
                };
            }


            walkHistory.Deleted = true;
            await _context.SaveChangesAsync();

            return new CommonResponse
            {
                Code = Models.StatusCode.Ok,
                Message = "Success To Delete The WalkHistory"
            };
        }

        private bool WalkHistoryExists(int id)
        {
            return _context.WalkHistory.Any(e => e.WalkHistoryId == id);
        }
    }
}
