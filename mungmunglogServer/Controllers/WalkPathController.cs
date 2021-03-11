using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{
    public class WalkPathController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalkPathController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WalkPath
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WalkPath.Include(w => w.WalkHistory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WalkPath/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkPath = await _context.WalkPath
                .Include(w => w.WalkHistory)
                .FirstOrDefaultAsync(m => m.WalkPathId == id);
            if (walkPath == null)
            {
                return NotFound();
            }

            return View(walkPath);
        }

        // GET: WalkPath/Create
        public IActionResult Create()
        {
            ViewData["WalkHistoryId"] = new SelectList(_context.WalkHistory, "WalkHistoryId", "WalkHistoryId");
            return View();
        }

        // POST: WalkPath/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WalkPathId,Latitude,Longitude,LocationBasedTime,WalkHistoryId")] WalkPath walkPath)
        {
            if (ModelState.IsValid)
            {
                _context.Add(walkPath);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WalkHistoryId"] = new SelectList(_context.WalkHistory, "WalkHistoryId", "WalkHistoryId", walkPath.WalkHistoryId);
            return View(walkPath);
        }

        // GET: WalkPath/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkPath = await _context.WalkPath.FindAsync(id);
            if (walkPath == null)
            {
                return NotFound();
            }
            ViewData["WalkHistoryId"] = new SelectList(_context.WalkHistory, "WalkHistoryId", "WalkHistoryId", walkPath.WalkHistoryId);
            return View(walkPath);
        }

        // POST: WalkPath/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WalkPathId,Latitude,Longitude,LocationBasedTime,WalkHistoryId")] WalkPath walkPath)
        {
            if (id != walkPath.WalkPathId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(walkPath);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalkPathExists(walkPath.WalkPathId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["WalkHistoryId"] = new SelectList(_context.WalkHistory, "WalkHistoryId", "WalkHistoryId", walkPath.WalkHistoryId);
            return View(walkPath);
        }

        // GET: WalkPath/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkPath = await _context.WalkPath
                .Include(w => w.WalkHistory)
                .FirstOrDefaultAsync(m => m.WalkPathId == id);
            if (walkPath == null)
            {
                return NotFound();
            }

            return View(walkPath);
        }

        // POST: WalkPath/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walkPath = await _context.WalkPath.FindAsync(id);
            _context.WalkPath.Remove(walkPath);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalkPathExists(int id)
        {
            return _context.WalkPath.Any(e => e.WalkPathId == id);
        }
    }
}
