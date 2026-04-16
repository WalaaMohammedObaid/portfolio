using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;

namespace PortfolioApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _db.Projects
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return View(projects);
        }
    }
}
