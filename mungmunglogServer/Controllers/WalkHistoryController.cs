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
    public class WalkHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalkHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WalkHistory
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WalkHistory.Include(w => w.FamilyMember).Include(w => w.Pet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WalkHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkHistory = await _context.WalkHistory
                .Include(w => w.FamilyMember)
                .Include(w => w.Pet)
                .FirstOrDefaultAsync(m => m.WalkHistoryId == id);
            if (walkHistory == null)
            {
                return NotFound();
            }

            return View(walkHistory);
        }

        // GET: WalkHistory/Create
        public IActionResult Create()
        {
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId");
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed");
            return View();
        }

        // POST: WalkHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WalkHistory walkHistory)
        {
            if (ModelState.IsValid)
            {
                if (walkHistory.AttachmentFile1 != null)
                {
                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile1);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl1 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile2 != null)
                {
                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile2);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl2 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile3 != null)
                {
                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile3);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl3 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile4 != null)
                {
                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile4);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl4 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile5 != null)
                {
                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile5);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl5 = filePath;
                    }
                }
                _context.Add(walkHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId", walkHistory.FamilyMemberId);
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed", walkHistory.PetId);
            return View(walkHistory);
        }

        // GET: WalkHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkHistory = await _context.WalkHistory.FindAsync(id);
            if (walkHistory == null)
            {
                return NotFound();
            }
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId", walkHistory.FamilyMemberId);
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed", walkHistory.PetId);
            return View(walkHistory);
        }

        // POST: WalkHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WalkHistory walkHistory)
        {
            if (id != walkHistory.WalkHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (walkHistory.AttachmentFile1 != null)
                {
                    if (!string.IsNullOrEmpty(walkHistory.FileUrl1))
                    {
                        await BlobController.Delete(walkHistory.FileUrl1);
                    }

                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile1);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl1 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile2 != null)
                {
                    if (!string.IsNullOrEmpty(walkHistory.FileUrl2))
                    {
                        await BlobController.Delete(walkHistory.FileUrl2);
                    }

                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile2);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl2 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile3 != null)
                {
                    if (!string.IsNullOrEmpty(walkHistory.FileUrl3))
                    {
                        await BlobController.Delete(walkHistory.FileUrl3);
                    }

                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile3);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl3 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile4 != null)
                {
                    if (!string.IsNullOrEmpty(walkHistory.FileUrl4))
                    {
                        await BlobController.Delete(walkHistory.FileUrl4);
                    }

                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile4);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl4 = filePath;
                    }
                }

                if (walkHistory.AttachmentFile5 != null)
                {
                    if (!string.IsNullOrEmpty(walkHistory.FileUrl5))
                    {
                        await BlobController.Delete(walkHistory.FileUrl5);
                    }

                    var filePath = await BlobController.UploadImage(walkHistory.AttachmentFile5);
                    if (filePath != null)
                    {
                        walkHistory.FileUrl5 = filePath;
                    }
                }
                try
                {
                    _context.Update(walkHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalkHistoryExists(walkHistory.WalkHistoryId))
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
            ViewData["FamilyMemberId"] = new SelectList(_context.FamilyMember, "FamilyMemberId", "FamilyMemberId", walkHistory.FamilyMemberId);
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "Breed", walkHistory.PetId);
            return View(walkHistory);
        }

        // GET: WalkHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkHistory = await _context.WalkHistory
                .Include(w => w.FamilyMember)
                .Include(w => w.Pet)
                .FirstOrDefaultAsync(m => m.WalkHistoryId == id);
            if (walkHistory == null)
            {
                return NotFound();
            }

            return View(walkHistory);
        }

        // POST: WalkHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walkHistory = await _context.WalkHistory.FindAsync(id);
            _context.WalkHistory.Remove(walkHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalkHistoryExists(int id)
        {
            return _context.WalkHistory.Any(e => e.WalkHistoryId == id);
        }
    }
}
