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
    [Route("api/history/deleteTrigger")]
    [ApiController]
    public class DeleteTriggerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeleteTriggerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DeleteTrigger
        [HttpGet]
        public async Task<ActionResult<CommonResponse>> GetHistoryDeleteTrigger()
        {
            var Deletedhistory = await _context.History.Where(h => h.Deleted == true).ToListAsync();

            _context.History.RemoveRange(Deletedhistory);

            return Ok();
        }
    }
}
