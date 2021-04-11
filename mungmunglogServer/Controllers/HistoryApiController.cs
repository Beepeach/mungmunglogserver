using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{
    [Route("api/history")]
    //[Authorize]
    [ApiController]
    public class HistoryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoryApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/history/list/pet/{petId}
        [HttpGet("list/pet/{petId}")]
        public async Task<ActionResult<ListResponse<HistoryDto>>> GetPetHistories(int petId)
        {
            var histories = await _context.History
                .Where(h => h.PetId == petId)
                .Select(h => new HistoryDto(h))
                .ToListAsync();

            if (histories == null)
            {
                return new ListResponse<HistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found Historys"
                };
            }

            return new ListResponse<HistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Get Histories",
                List = histories
            };
        }

        // GET: api/history/list/member/{memberId}
        [HttpGet("list/member/{memberId}")]
        public async Task<ActionResult<ListResponse<HistoryDto>>> GetMemberHistories(int memberId)
        {
            var histories = await _context.History
                .Where(h => h.FamilyMemberId == memberId)
                .Select(h => new HistoryDto(h))
                .ToListAsync();

            if (histories == null)
            {
                return new ListResponse<HistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found Historys"
                };
            }

            return new ListResponse<HistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Get Histories",
                List = histories
            };
        }

        // GET: api/history/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleResponse<HistoryDto>>> GetHistory(int id)
        {
            var history = await _context.History.FindAsync(id);

            if (history == null)
            {
                return new SingleResponse<HistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found Historys"
                };
            }

            return new SingleResponse<HistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Get Histories",
                Data = new HistoryDto(history)
            };
        }

        // PUT: api/history/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<CommonResponse>> PutHistory(int id, HistoryPutModel model)
        {
            var history = await _context.History.Where(h => h.HistoryId == id).FirstOrDefaultAsync();

            if (history == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The History"
                };
            }

            history.Type = model.Type;
            history.Date = new DateTime((long)model.Date * 10_000_000, DateTimeKind.Utc);
            history.Contents = model.Contents;
            history.FileUrl1 = model.FileUrl1;
            history.FileUrl2 = model.FileUrl2;
            history.FileUrl3 = model.FileUrl3;
            history.FileUrl4 = model.FileUrl4;
            history.FileUrl5 = model.FileUrl5;

            _context.Entry(history).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return new CommonResponse
                {
                    Code = Models.StatusCode.Ok,
                    Message = "Success To Put History "
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryExists(id))
                {
                    return new CommonResponse
                    {
                        Code = Models.StatusCode.NotFound,
                        Message = "Not Found The History"
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

        // POST: api/history
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SingleResponse<HistoryDto>>> PostHistory(HistoryPostModel model)
        {
            var pet = await _context.Pet
                .Where(p => p.PetId == model.PetId)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                return new SingleResponse<HistoryDto>
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
                return new SingleResponse<HistoryDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The FamilyMember"
                };
            }

            var newHistory = new History
            {
                Type = model.Type,
                Date = new DateTime((long)model.Date * 10_000_000, DateTimeKind.Utc),
                Contents = model.Contents,
                Deleted = false,
                FileUrl1 = model.FileUrl1,
                FileUrl2 = model.FileUrl2,
                FileUrl3 = model.FileUrl3,
                FileUrl4 = model.FileUrl4,
                FileUrl5 = model.FileUrl5,
                PetId = model.PetId,
                FamilyMemberId = model.FamilyMemberId
            };

            _context.History.Add(newHistory);
            await _context.SaveChangesAsync();

            pet.Histories.Add(newHistory);
            familyMember.Histories.Add(newHistory);

            await _context.SaveChangesAsync();

            return new SingleResponse<HistoryDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Post",
                Data = new HistoryDto(newHistory)
            };
        }

        // DELETE: api/history/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommonResponse>> DeleteHistory(int id)
        {
            var history = await _context.History.FindAsync(id);
            if (history == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The History"
                };
            }

            history.Deleted = true;
            await _context.SaveChangesAsync();

            return new CommonResponse
            {
                Code = Models.StatusCode.Ok,
                Message = "Success To Delete History "
            };
        }

        private bool HistoryExists(int id)
        {
            return _context.History.Any(e => e.HistoryId == id);
        }
    }
}
