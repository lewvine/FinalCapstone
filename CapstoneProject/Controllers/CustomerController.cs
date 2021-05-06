using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CapstoneProject.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerController
        public ActionResult Index(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers
                .Where(c => c.IdentityUserId == userId)
                .Include(c=>c.Projects)
                    .ThenInclude(p=>p.Salesperson)
                        .ThenInclude(p=>p.Appointments)
                .Include(c=>c.Projects)
                    .ThenInclude(p=>p.Grass)
                .FirstOrDefault();
            ViewBag.MapUrl = $"https://www.google.com/maps/embed/v1/place?key=" + Utilities.APIs.MapsKey + "&q=" + customer.LatAddress.ToString() + "," + customer.LongAddress.ToString();
            if (customer == null)
            {
                return RedirectToAction(nameof(Create));
            }
            else 
            {
                return View(customer);
            }
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = this.User.FindFirstValue(ClaimTypes.Email);
                customer.IdentityUserId = userId;
                customer.EMailAddress = email;
                string address = customer.StreetAddress
                                 + ", "
                                 + customer.CityAddress
                                 + ", "
                                 + customer.StateAddress
                                 + " "
                                 + customer.ZipAddress;
                customer.SetGeocode(address);
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Where(c => c.id == id).FirstOrDefault();
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            var customerInDB = _context.Customers.Where(c => c.id == customer.id).FirstOrDefault();
            customerInDB.FirstName = customer.FirstName;
            customerInDB.LastName = customer.LastName;
            customerInDB.StreetAddress = customer.StreetAddress;
            customerInDB.CityAddress = customer.CityAddress;
            customerInDB.StateAddress = customer.StateAddress;
            customerInDB.ZipAddress = customer.ZipAddress;
            customerInDB.PhoneNumber = customer.PhoneNumber;
            customerInDB.EMailAddress = customer.EMailAddress;
            string address = customerInDB.StreetAddress
                 + ", "
                 + customerInDB.CityAddress
                 + ", "
                 + customerInDB.StateAddress
                 + " "
                 + customerInDB.ZipAddress;
            customerInDB.SetGeocode(address);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
         }


        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
