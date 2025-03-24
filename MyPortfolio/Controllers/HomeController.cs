using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models;
using MyPortfolio.Models.Data;

namespace MyPortfolio.Controllers
{
    public class HomeController(PortfolioDbContext context) : Controller
    {
        private readonly PortfolioDbContext _context = context;

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
        public IActionResult UserDashboard()
        {
            return View();
        }
        [HttpPut]
        public async Task<IActionResult> EditProfile(User model)
        {
            //var user = await _context.Users.Update();

            _context.Update(model);
            await _context.SaveChangesAsync();



            return View();
        }


        public async Task<IActionResult> Projects()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactForm form)
        {
            if (ModelState.IsValid)
            {
                _context.ContactForms.Add(form);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Thank you for your message!";
            }
            return View();
        }
    }

}
