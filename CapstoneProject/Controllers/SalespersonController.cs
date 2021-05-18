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
    public class SalespersonController : Controller
    {
        private ApplicationDbContext _context;


        public SalespersonController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: SalespersonController
        public ActionResult Index(double addDays)
        {
            double currentDay = DateTime.Today.DayOfYear;
            if (addDays != 0)
            {
                currentDay = addDays;
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var salesperson = _context.Salespeople
                .Include(s => s.Projects)
                   .ThenInclude(p => p.Customer)
                .Include(s => s.Projects)
                    .ThenInclude(p => p.Grass)
                .Include(s => s.Appointments)
                .Where(s => s.IdentityUserId == userId)
                .FirstOrDefault();


            if (salesperson.Projects.Where(p => p.IsSold == false).ToList().Count() > 0)
            {
                ViewBag.OpenOpportunities = salesperson.Projects.Where(p => p.IsSold == false).ToList().Count();
            }
            else
            {
                ViewBag.OpenOpportunities = 0;
            };
            ViewBag.CurrentMonthSales = Math.Round(salesperson.Projects
                .Where(p => p.ConvertedToSale <= DateTime.Now && p.ConvertedToSale >= DateTime.Now.AddDays(-30)).Select(p=>p.Cost).Sum());
            ViewBag.CurrentWeekSales = salesperson.Projects
                .Where(p => p.ConvertedToSale >= DateTime.Now.AddDays(-7) && p.ConvertedToSale < DateTime.Now)
                .Select(p=>p.Cost).Sum();
            ViewBag.FirstPreviousWeekSales = salesperson.Projects
                .Where(p => p.ConvertedToSale >= DateTime.Now.AddDays(-14) && p.ConvertedToSale < DateTime.Now.AddDays(-7))
                .Select(p => p.Cost).Sum();
            ViewBag.SecondPreviousWeekSales = salesperson.Projects
                .Where(p => p.ConvertedToSale >= DateTime.Now.AddDays(-21) && p.ConvertedToSale < DateTime.Now.AddDays(-14))
                .Select(p => p.Cost).Sum();
            ViewBag.ThirdPreviousWeekSales = salesperson.Projects
                .Where(p => p.ConvertedToSale >= DateTime.Now.AddDays(-28) && p.ConvertedToSale < DateTime.Now.AddDays(-21))
                .Select(p => p.Cost).Sum();

            ViewBag.CloseRate = Math.Round((Convert.ToDouble(salesperson.TotalProjects) / Convert.ToDouble(salesperson.TotalOpportunities)) * 100);
            Day day = new Day();
            ViewBag.SelectedWeek = day.SelectWeek(currentDay);
            if (salesperson == null)
            {
                return RedirectToAction("Create");
            }
            return View(salesperson);
        }

        // GET: SalespersonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalespersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalespersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Salesperson salesperson)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = this.User.FindFirstValue(ClaimTypes.Email);
                salesperson.IdentityUserId = userId;
                salesperson.EMailAddress = email;
                string address = salesperson.StreetAddress 
                                 + ", " 
                                 + salesperson.CityAddress 
                                 + ", " 
                                 + salesperson.StateAddress 
                                 + " " 
                                 + salesperson.ZipAddress;
                salesperson.SetGeocode(address);
                salesperson.Appointments = new List<Appointment>();
                DateTime apptTime = new DateTime();
                apptTime = DateTime.Today;
                for (int days = 1; days <= 5; days++)
                {
                    DateTime today = apptTime.AddDays(days);
                    for (int apptIndex = 0; apptIndex < 4; apptIndex++)
                    {
                        int apptHour = 8 + (apptIndex * 2);
                        Appointment appt = new Appointment
                        {
                            AppointmentStart = today.AddHours(apptHour),
                            AppointmentEnd = today.AddHours(apptHour + 2),
                            IsBooked = false,
                            IsCompleted = false,
                            IsOpen = true,
                            Notes = "This appointment is open",
                            InteractionType = "Open appointment",
                            Project = null,
                            ProjID = null,
                        };
                        salesperson.Appointments.Add(appt);
                        _context.SaveChanges();
                    };
                }
                _context.Salespeople.Add(salesperson);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalespersonController/Edit/5
        public ActionResult Edit(int id)
        {
            var salesperson = _context.Salespeople.Where(s => s.id == id).FirstOrDefault();
            return View(salesperson);
        }

        // POST: SalespersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Salesperson salesperson)
        {
            var salespersonInDB = _context.Salespeople.Where(s => s.id == salesperson.id).FirstOrDefault();
            salespersonInDB.FirstName = salesperson.FirstName;
            salespersonInDB.LastName = salesperson.LastName;
            salespersonInDB.StreetAddress = salesperson.StreetAddress;
            salespersonInDB.CityAddress = salesperson.CityAddress;
            salespersonInDB.StateAddress = salesperson.StateAddress;
            salespersonInDB.ZipAddress = salesperson.ZipAddress;
            salespersonInDB.EMailAddress = salesperson.EMailAddress;
            salespersonInDB.PhoneNumber = salesperson.PhoneNumber;
            _context.SaveChanges();
            return RedirectToAction("Index", "Salesperson");
        }

        [HttpGet]
        public ActionResult CreateAppointments(int id)
        {
            var salesperson = _context.Salespeople.Include(s=>s.Appointments).Where(s => s.id == id).FirstOrDefault();
            ViewBag.LastAppointment = salesperson.Appointments.OrderBy(a => a.AppointmentEnd).Reverse().FirstOrDefault().AppointmentEnd;
            return View(salesperson);
        }

        [HttpPost]
        public ActionResult CreateAppointments(Salesperson salesperson)
        {
            DateTime date = new DateTime();
            date = Convert.ToDateTime(salesperson.NewAppointment);
            var salespersonInDB = _context.Salespeople.Include(s=>s.Appointments).Where(s => s.id == salesperson.id).FirstOrDefault();
            var appointments = _context.Appointments;
            for (int i = 0; i< 4; i++)
            {
                int start = 8 + (i * 2);
                int end = 10 + (i * 2);
                Appointment appt = new Appointment()
                {
                    AppointmentStart = date.AddHours(start),
                    AppointmentEnd = date.AddHours(end),
                    Notes = "This appointment is open",
                    InteractionType = "Nothing to show",
                    IsOpen = true,
                    IsBooked = false,
                    IsCompleted = false
                };
                bool value = _context.Appointments.Where(a => a.AppointmentStart == appt.AppointmentStart).Any();
                if(value == true)
                {
                    continue;
                }
                else
                {
                    appointments.Add(appt);
                    _context.SaveChanges();
                    salespersonInDB.Appointments.Add(appt);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index","Salesperson");
        }

        // GET: SalespersonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalespersonController/Delete/5
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
