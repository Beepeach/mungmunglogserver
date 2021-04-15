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
        public async Task<IActionResult> Create(History history)
        {
            if (ModelState.IsValid)
            {
                if (history.AttachmentFile1 != null)
                {
                    var filePath = await BlobController.UploadImage(history.AttachmentFile1);
                    if (filePath != null)
                    {
                        history.FileUrl1 = filePath;
                    }
                }

                if (history.AttachmentFile2 != null)
                {
                    var filePath = await BlobController.UploadImage(history.AttachmentFile2);
                    if (filePath != null)
                    {
                        history.FileUrl2 = filePath;
                    }
                }

                if (history.AttachmentFile3 != null)
                {
                    var filePath = await BlobController.UploadImage(history.AttachmentFile3);
                    if (filePath != null)
                    {
                        history.FileUrl3 = filePath;
                    }
                }

                if (history.AttachmentFile4 != null)
                {
                    var filePath = await BlobController.UploadImage(history.AttachmentFile4);
                    if (filePath != null)
                    {
                        history.FileUrl4 = filePath;
                    }
                }

                if (history.AttachmentFile5 != null)
                {
                    var filePath = await BlobController.UploadImage(history.AttachmentFile5);
                    if (filePath != null)
                    {
                        history.FileUrl5 = filePath;
                    }
                }

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
        public async Task<IActionResult> Edit(int id, History history)
        {
            if (id != history.HistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (history.AttachmentFile1 != null)
                {
                    if (!string.IsNullOrEmpty(history.FileUrl1))
                    {
                        await BlobController.Delete(history.FileUrl1);
                    }

                    var filePath = await BlobController.UploadImage(history.AttachmentFile1);
                    if (filePath != null)
                    {
                        history.FileUrl1 = filePath;
                    }
                }

                if (history.AttachmentFile2 != null)
                {
                    if (!string.IsNullOrEmpty(history.FileUrl2))
                    {
                        await BlobController.Delete(history.FileUrl2);
                    }

                    var filePath = await BlobController.UploadImage(history.AttachmentFile2);
                    if (filePath != null)
                    {
                        history.FileUrl2 = filePath;
                    }
                }

                if (history.AttachmentFile3 != null)
                {
                    if (!string.IsNullOrEmpty(history.FileUrl3))
                    {
                        await BlobController.Delete(history.FileUrl3);
                    }

                    var filePath = await BlobController.UploadImage(history.AttachmentFile3);
                    if (filePath != null)
                    {
                        history.FileUrl3 = filePath;
                    }
                }

                if (history.AttachmentFile4 != null)
                {
                    if (!string.IsNullOrEmpty(history.FileUrl4))
                    {
                        await BlobController.Delete(history.FileUrl4);
                    }

                    var filePath = await BlobController.UploadImage(history.AttachmentFile4);
                    if (filePath != null)
                    {
                        history.FileUrl4 = filePath;
                    }
                }

                if (history.AttachmentFile5 != null)
                {
                    if (!string.IsNullOrEmpty(history.FileUrl5))
                    {
                        await BlobController.Delete(history.FileUrl5);
                    }

                    var filePath = await BlobController.UploadImage(history.AttachmentFile5);
                    if (filePath != null)
                    {
                        history.FileUrl5 = filePath;
                    }
                }

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
