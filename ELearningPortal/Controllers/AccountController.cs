using ELearningPortal.Helpers;
using ELearningPortal.Interfaces;
using ELearningPortal.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers
{
    // Common login / logout controller for ALL roles (SuperAdmin, Admin, User).
    // After a successful login we redirect the user to their portal based on role.
    public class AccountController : Controller
    {
        private readonly IAuthService authService;
        private readonly JwtHelper jwtHelper;

        public AccountController(IAuthService authService, JwtHelper jwtHelper)
        {
            this.authService = authService;
            this.jwtHelper = jwtHelper;
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // If already logged in, redirect to their dashboard
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectByRole(User.GetRole());

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = authService.ValidateUser(model);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            // Generate JWT and store in HttpOnly cookie so MVC [Authorize] works automatically.
            var token = jwtHelper.GenerateToken(user);

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,        // set true in production over HTTPS
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });

            return RedirectByRole(user.Role?.RoleName ?? string.Empty);
        }

        // GET: /Account/Logout
        [HttpGet]
        [Route("/Account/Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");
        }

        // GET: /Account/AccessDenied
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // ---- helper: redirect based on role ----
        private IActionResult RedirectByRole(string roleName)
        {
            return roleName switch
            {
                "Super Admin" => RedirectToAction("SuperBranch", "Branch"),
                "Admin"      => RedirectToAction("AdminUser", "User"),
                "Student"       => RedirectToAction("Dashboard", "MyCourse", new { area = "" }), // User portal home
                _            => RedirectToAction("Login")
            };
        }
    }
}
