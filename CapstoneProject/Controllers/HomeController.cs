using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.HasProjects = false;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (this.User.IsInRole("Salesperson"))
            {
                var isTrue = _context.Salespeople.Where(s => s.IdentityUserId == userId).ToList().FirstOrDefault();
                if(isTrue == null)
                {
                    return RedirectToAction("Create", "Salesperson");

                }
                return RedirectToAction("Index", "Salesperson");
            }
            ViewBag.Grasses = _context.Grasses.ToList();
            ViewBag.IsHome = true;
            if (this.User.IsInRole("Customer")) 
            {
                var customer = _context.Customers
                    .Include(c => c.Projects)
                    .Where(c=>c.IdentityUserId == userId)
                    .FirstOrDefault();
                if(customer.Projects.Count() == 0)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Customer");

                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
