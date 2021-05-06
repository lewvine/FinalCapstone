﻿using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var salesperson = _context.Salespeople.Where(s => s.id == appointment.Project.Salesperson.id).FirstOrDefault();
            var appointments = _context.Appointments.ToList();
            salesperson.Appointments.Remove(appointment);
            appointments.Remove(appointment);
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