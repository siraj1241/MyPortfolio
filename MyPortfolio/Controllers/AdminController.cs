using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models.Data;
using MyPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace MyPortfolio.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly PortfolioDbContext _context;

        public AdminController(PortfolioDbContext context)
        {
            _context = context;
        }
        
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> ManageProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageProjects");
            }
            return View(project);
        }

        public async Task<IActionResult> EditProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageProjects");
            }
            return View(project);
        }

        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ManageProjects");
        }
    }
}



