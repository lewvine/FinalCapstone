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
    public class AppointmentController : Controller
    {
        private ApplicationDbContext _context;
        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

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
            Day day = new Day();
            ViewBag.SelectedWeek = day.SelectWeek(currentDay);

            return View(salesperson);
        }

        public ActionResult AddAppointments(int id)
        {
            int dayOfYear = id;
            int year = DateTime.Now.Year;
            DateTime date = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var salesperson = _context.Salespeople.Include(s=>s.Appointments).Where(s => s.IdentityUserId == userId).FirstOrDefault();
            var appointments = _context.Appointments;
            for (int i = 0; i < 4; i++)
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
                if (value == true)
                {
                    continue;
                }
                else
                {
                    appointments.Add(appt);
                    _context.SaveChanges();
                    salesperson.Appointments.Add(appt);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Salesperson");
        }

        [HttpGet]
        public ActionResult BookOpenAppointment(int id)
        {
            Appointment appt = _context.Appointments.Where(a => a.id == id).FirstOrDefault();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Salesperson salesperson = _context.Salespeople.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            ViewBag.Projects = _context.Projects.Where(p => p.Salesperson.id == salesperson.id && p.IsSold == false).ToList();
            return View(appt);
        }

        [HttpPost]
        public ActionResult BookOpenAppointment(Appointment appt)
        {
            Appointment apptInDB = _context.Appointments.Where(a => a.id == appt.id).FirstOrDefault();
            apptInDB.IsBooked = true;
            apptInDB.IsCompleted = false;
            apptInDB.IsOpen = false;
            apptInDB.ProjID = appt.ProjID;
            apptInDB.InteractionType = appt.InteractionType;
            apptInDB.Notes = appt.Notes;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            var appointment = _context.Appointments
                .Where(a => a.id == id)
                .Include(a=>a.Project)
                    .ThenInclude(p =>p.Customer)
                .Include(a => a.Project)
                    .ThenInclude(p => p.Salesperson)
                .Include(a => a.Project)
                    .ThenInclude(p => p.Grass)
                .FirstOrDefault();
            ViewBag.MapUrl = $"https://www.google.com/maps/embed/v1/place?key=" + Utilities.APIs.MapsKey + "&q=" + appointment.Project.LatAddress.ToString() + "," + appointment.Project.LongAddress.ToString();
            return View(appointment);
        }
        public ActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Where(a => a.id == id).FirstOrDefault();
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment appointment)
        {
            var appointmentInDB = _context.Appointments
                .Include(a=>a.Project)
                .Where(a => a.id == appointment.id).FirstOrDefault();
            appointmentInDB.Notes = appointment.Notes;
            appointmentInDB.InteractionType = appointment.InteractionType;
            _context.SaveChanges();
            return RedirectToAction("Index", "Salesperson");
        }

        public ActionResult Delete(int id)
        {

            var appointment = _context.Appointments
                .Include(a => a.Project)
                .ThenInclude(p => p.Salesperson)
                .Where(a => a.id == id)
                .FirstOrDefault();
            var salesperson = _context.Salespeople.Where(s=>s.id == appointment.Project.SalesID).FirstOrDefault();
            appointment.IsBooked = false;
            appointment.IsCompleted = false;
            appointment.IsOpen = true;
            appointment.ProjID = null;
            appointment.Notes = "This appointment is open";
            appointment.Project = null;
            _context.SaveChanges();
            salesperson.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Complete(Appointment appointment)
        {
            var appointmentInDB = _context.Appointments
                .Include(a => a.Project)
                    .ThenInclude(p=>p.Salesperson)
                .Include(a => a.Project)
                    .ThenInclude(p => p.Customer)
                .Include(a => a.Project)
                    .ThenInclude(p => p.Grass)
                .Where(a => a.id == appointment.id).FirstOrDefault();
            appointmentInDB.IsCompleted = true;
            appointmentInDB.IsBooked = true;
            appointmentInDB.IsOpen = false;
            if(appointment.Notes == null)
            {
                appointmentInDB.Notes = "No notes from the appointment";
            }
            else
            {
                appointmentInDB.Notes = appointment.Notes;
            }
            if(appointment.InteractionType == null) 
            {
                appointmentInDB.InteractionType = "No interaction type noted";
            }
            else
            {
                appointmentInDB.InteractionType = appointment.InteractionType;

            }
            int id = appointmentInDB.Project.id;
            _context.SaveChanges();
            return RedirectToAction("Index", "Salesperson");
        }
    }
}
