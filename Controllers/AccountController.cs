using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Models;
using System.Security.Claims;

namespace PortfolioApp.Controllers
{
    public class AccountController : Controller
    {
        // بيانات الأدمن - يمكن تغييرها هنا
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Admin");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Username == AdminUsername && model.Password == AdminPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, "AdminCookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AdminCookies", principal);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور غير صحيحة");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminCookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
