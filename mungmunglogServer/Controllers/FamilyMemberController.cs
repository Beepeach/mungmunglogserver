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
    public class FamilyMemberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamilyMemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FamilyMember
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FamilyMember.Include(f => f.Family);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FamilyMember/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMember
                .Include(f => f.Family)
                .FirstOrDefaultAsync(m => m.FamilyMemberId == id);
            if (familyMember == null)
            {
                return NotFound();
            }

            return View(familyMember);
        }

        // GET: FamilyMember/Create
        public IActionResult Create()
        {
            ViewData["FamilyId"] = new SelectList(_context.Family, "FamilyId", "FamilyId");
            return View();
        }

        // POST: FamilyMember/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FamilyMemberId,IsMaster,Status,UserId,FamilyId")] FamilyMember familyMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familyMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyId"] = new SelectList(_context.Family, "FamilyId", "FamilyId", familyMember.FamilyId);
            return View(familyMember);
        }

        // GET: FamilyMember/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMember.FindAsync(id);
            if (familyMember == null)
            {
                return NotFound();
            }
            ViewData["FamilyId"] = new SelectList(_context.Family, "FamilyId", "FamilyId", familyMember.FamilyId);
            return View(familyMember);
        }

        // POST: FamilyMember/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FamilyMemberId,IsMaster,Status,UserId,FamilyId")] FamilyMember familyMember)
        {
            if (id != familyMember.FamilyMemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyMemberExists(familyMember.FamilyMemberId))
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
            ViewData["FamilyId"] = new SelectList(_context.Family, "FamilyId", "FamilyId", familyMember.FamilyId);
            return View(familyMember);
        }

        // GET: FamilyMember/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyMember = await _context.FamilyMember
                .Include(f => f.Family)
                .FirstOrDefaultAsync(m => m.FamilyMemberId == id);
            if (familyMember == null)
            {
                return NotFound();
            }

            return View(familyMember);
        }

        // POST: FamilyMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var familyMember = await _context.FamilyMember.FindAsync(id);
            _context.FamilyMember.Remove(familyMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyMemberExists(int id)
        {
            return _context.FamilyMember.Any(e => e.FamilyMemberId == id);
        }
    }
}
