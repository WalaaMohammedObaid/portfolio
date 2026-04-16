using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;
using PortfolioApp.Models;

namespace PortfolioApp.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminCookies")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AdminController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // عرض جميع المشاريع
        public async Task<IActionResult> Index()
        {
            var projects = await _db.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return View(projects);
        }

        // صفحة إضافة مشروع جديد
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // حفظ مشروع جديد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(project);

            if (imageFile != null && imageFile.Length > 0)
            {
                project.ImagePath = await SaveImage(imageFile);
            }

            project.CreatedAt = DateTime.UtcNow;
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();

            TempData["Success"] = "تم إضافة المشروع بنجاح!";
            return RedirectToAction("Index");
        }

        // صفحة تعديل مشروع
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // حفظ التعديلات
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project, IFormFile? imageFile)
        {
            if (id != project.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(project);

            var existing = await _db.Projects.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Title = project.Title;
            existing.Description = project.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                // حذف الصورة القديمة
                if (!string.IsNullOrEmpty(existing.ImagePath))
                    DeleteImage(existing.ImagePath);

                existing.ImagePath = await SaveImage(imageFile);
            }

            await _db.SaveChangesAsync();

            TempData["Success"] = "تم تعديل المشروع بنجاح!";
            return RedirectToAction("Index");
        }

        // حذف مشروع
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            if (!string.IsNullOrEmpty(project.ImagePath))
                DeleteImage(project.ImagePath);

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            TempData["Success"] = "تم حذف المشروع بنجاح!";
            return RedirectToAction("Index");
        }

        // ---- Helper Methods ----

        private async Task<string> SaveImage(IFormFile file)
        {
            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return "/uploads/" + fileName;
        }

        private void DeleteImage(string imagePath)
        {
            try
            {
                var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }
            catch { }
        }
    }
}
