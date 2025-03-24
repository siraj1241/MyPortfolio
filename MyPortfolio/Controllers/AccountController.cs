using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models.Data;
using MyPortfolio.Models;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MyPortfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly PortfolioDbContext _context;

        public AccountController(PortfolioDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Hash the password before saving to the database
            //model.Password = PasswordHelper.HashPassword(model.Password);

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Registration successful. Please login.";
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user != null && user.Password == model.Password) // Use hashed password verification in production
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role) // Ensure the Role exists in your User model
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity),
                                              authProperties);

                // Redirect based on Role
                if (user.Role == "Admin")
                {
                    TempData["SuccessMessage"] = "Welcome Admin! You have full access.";
                    
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (user.Role == "User")
                {
                    ViewBag.Message = "Welcome User! You have limited access.";
                    return RedirectToAction("UserDashboard", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Message = "Invalid credentials!";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Projects", "Home");
        }
       
    }
}
