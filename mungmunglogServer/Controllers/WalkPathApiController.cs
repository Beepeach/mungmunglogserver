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
    [Route("api/walkpath")]
    [ApiController]
    public class WalkPathApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WalkPathApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/walkpath
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalkPath>>> GetWalkPath()
        {
            return await _context.WalkPath.ToListAsync();
        }

        // GET: api/walkpath/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WalkPath>> GetWalkPath(int id)
        {
            var walkPath = await _context.WalkPath.FindAsync(id);

            if (walkPath == null)
            {
                return NotFound();
            }

            return walkPath;
        }

        // PUT: api/walkpath/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWalkPath(int id, WalkPath walkPath)
        {
            if (id != walkPath.WalkPathId)
            {
                return BadRequest();
            }

            _context.Entry(walkPath).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalkPathExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/walkpath
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WalkPath>> PostWalkPath(WalkPath walkPath)
        {
            _context.WalkPath.Add(walkPath);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWalkPath", new { id = walkPath.WalkPathId }, walkPath);
        }

        // DELETE: api/walkpath/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WalkPath>> DeleteWalkPath(int id)
        {
            var walkPath = await _context.WalkPath.FindAsync(id);
            if (walkPath == null)
            {
                return NotFound();
            }

            _context.WalkPath.Remove(walkPath);
            await _context.SaveChangesAsync();

            return walkPath;
        }

        private bool WalkPathExists(int id)
        {
            return _context.WalkPath.Any(e => e.WalkPathId == id);
        }
    }
}
