using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // 사진파일을 스토리지에 저장하고 해당 URL을 가져오는 메서드
        public async Task<string> PictureUpload(IFormFile file)
        {
            try
            {
                var account = new CloudStorageAccount(new StorageCredentials("mungmunglogstorage", "sHVj01H5c6bd4JCeyCAPZGn4/TI/kOa03DKmUuKC40kv1U167Uaew9RhuxTuDjNXE6ChF53TNthKQCElDYucsQ=="), true);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("main");

                var fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                var blob = container.GetBlockBlobReference(fileName);

                await blob.UploadFromStreamAsync(file.OpenReadStream());

                return blob.Uri.AbsoluteUri;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return null;
        }

        // 기존에 있는 file을 삭제
        public async Task PictureDelete(string path)
        {
            try
            {
                var account = new CloudStorageAccount(new StorageCredentials("mungmunglogstorage", "sHVj01H5c6bd4JCeyCAPZGn4/TI/kOa03DKmUuKC40kv1U167Uaew9RhuxTuDjNXE6ChF53TNthKQCElDYucsQ=="), true);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("main");

                var id = path.Substring(path.LastIndexOf("/") + 1);
                var blob = container.GetBlockBlobReference(id);

                await blob.DeleteIfExistsAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }


        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.AttachmentFile != null)
                {
                    var filePath = await PictureUpload(user.AttachmentFile);
                    if (filePath != null)
                    {
                        user.FileUrl = filePath;
                    }
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (user.AttachmentFile != null)
                {
                    if (!string.IsNullOrEmpty(user.FileUrl))
                    {
                        await PictureDelete(user.FileUrl);
                    }

                    var filePath = await PictureUpload(user.AttachmentFile);

                    if (filePath != null)
                    {
                        user.FileUrl = filePath;
                    }
                }


                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
