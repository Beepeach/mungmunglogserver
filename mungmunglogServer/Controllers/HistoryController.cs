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
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: History
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.History.Include(h => h.FamilyMember).Include(h => h.Pet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: History/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _context.History
                .Include(h => h.FamilyMember)
                .Include(h => h.Pet)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (history == null)
            {
                return NotFound();
            }

            return View(history);
        }

        // GET: History/Create
        public IActionResult Create()
        {
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId");
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed");
            return View();
        }

        // POST: History/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryId,Type,Date,Contents,Deleted,FileUrl1,FileUrl2,FileUrl3,FileUrl4,FileUrl5,PetId,FamilyMemberId")] History history)
        {
            if (ModelState.IsValid)
            {
                _context.Add(history);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId", history.FamilyMemberId);
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed", history.PetId);
            return View(history);
        }

        // GET: History/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _context.History.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId", history.FamilyMemberId);
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed", history.PetId);
            return View(history);
        }

        // POST: History/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryId,Type,Date,Contents,Deleted,FileUrl1,FileUrl2,FileUrl3,FileUrl4,FileUrl5,PetId,FamilyMemberId")] History history)
        {
            if (id != history.HistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(history);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoryExists(history.HistoryId))
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
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId", history.FamilyMemberId);
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed", history.PetId);
            return View(history);
        }

        // GET: History/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _context.History
                .Include(h => h.FamilyMember)
                .Include(h => h.Pet)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (history == null)
            {
                return NotFound();
            }

            return View(history);
        }

        // POST: History/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var history = await _context.History.FindAsync(id);
            _context.History.Remove(history);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoryExists(int id)
        {
            return _context.History.Any(e => e.HistoryId == id);
        }
    }
}
