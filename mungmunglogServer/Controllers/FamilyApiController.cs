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
    [Route("api/family")]
    [ApiController]
    public class FamilyApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FamilyApiController(ApplicationDbContext context)
        {
            _context = context;
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

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.FamilyId == id);
        }
    }
}
