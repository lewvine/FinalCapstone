﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Day
    {
        public string DayOfWeek { get; set; }
        public DateTime? Date { get; set; }

        public List<Day> SelectWeek(string currentDay)
        {
            List<Day> currentWeek = new List<Day>();

            switch (currentDay) 
            {
                case "Sunday":
                    currentWeek.Add(new Day() { DayOfWeek = "Sunday", Date = DateTime.Today.AddDays(0) });
                    currentWeek.Add(new Day() { DayOfWeek = "Monday", Date = DateTime.Today.AddDays(1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Tuesday", Date = DateTime.Today.AddDays(2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Wedesday", Date = DateTime.Today.AddDays(3) });
                    currentWeek.Add(new Day() { DayOfWeek = "Thursday", Date = DateTime.Today.AddDays(4) });
                    currentWeek.Add(new Day() { DayOfWeek = "Friday", Date = DateTime.Today.AddDays(5) });
                    currentWeek.Add(new Day() { DayOfWeek = "Saturday", Date = DateTime.Today.AddDays(6) });
                    break;
                case "Monday":
                    currentWeek.Add(new Day() { DayOfWeek = "Sunday", Date = DateTime.Today.AddDays(-1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Monday", Date = DateTime.Today.AddDays(0) });
                    currentWeek.Add(new Day() { DayOfWeek = "Tuesday", Date = DateTime.Today.AddDays(1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Wedesday", Date = DateTime.Today.AddDays(2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Thursday", Date = DateTime.Today.AddDays(3) });
                    currentWeek.Add(new Day() { DayOfWeek = "Friday", Date = DateTime.Today.AddDays(4) });
                    currentWeek.Add(new Day() { DayOfWeek = "Saturday", Date = DateTime.Today.AddDays(5) });
                    break;
                case "Tuesday":
                    currentWeek.Add(new Day() { DayOfWeek = "Sunday", Date = DateTime.Today.AddDays(-2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Monday", Date = DateTime.Today.AddDays(-1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Tuesday", Date = DateTime.Today.AddDays(0) });
                    currentWeek.Add(new Day() { DayOfWeek = "Wedesday", Date = DateTime.Today.AddDays(1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Thursday", Date = DateTime.Today.AddDays(2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Friday", Date = DateTime.Today.AddDays(3) });
                    currentWeek.Add(new Day() { DayOfWeek = "Saturday", Date = DateTime.Today.AddDays(4) });
                    break;
                case "Wednesday":
                    currentWeek.Add(new Day() { DayOfWeek = "Sunday", Date = DateTime.Today.AddDays(-3) });
                    currentWeek.Add(new Day() { DayOfWeek = "Monday", Date = DateTime.Today.AddDays(-2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Tuesday", Date = DateTime.Today.AddDays(-1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Wedesday", Date = DateTime.Today.AddDays(0) });
                    currentWeek.Add(new Day() { DayOfWeek = "Thursday", Date = DateTime.Today.AddDays(1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Friday", Date = DateTime.Today.AddDays(2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Saturday", Date = DateTime.Today.AddDays(3) });
                    break;
                case "Thursday":
                    currentWeek.Add(new Day() { DayOfWeek = "Sunday", Date = DateTime.Today.AddDays(-4) });
                    currentWeek.Add(new Day() { DayOfWeek = "Monday", Date = DateTime.Today.AddDays(-3) });
                    currentWeek.Add(new Day() { DayOfWeek = "Tuesday", Date = DateTime.Today.AddDays(-2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Wedesday", Date = DateTime.Today.AddDays(-1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Thursday", Date = DateTime.Today.AddDays(0) });
                    currentWeek.Add(new Day() { DayOfWeek = "Friday", Date = DateTime.Today.AddDays(-1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Saturday", Date = DateTime.Today.AddDays(-2) });
                    break;
                case "Friday":
                    currentWeek.Add(new Day() { DayOfWeek = "Sunday", Date = DateTime.Today.AddDays(-5) });
                    currentWeek.Add(new Day() { DayOfWeek = "Monday", Date = DateTime.Today.AddDays(-4) });
                    currentWeek.Add(new Day() { DayOfWeek = "Tuesday", Date = DateTime.Today.AddDays(-3) });
                    currentWeek.Add(new Day() { DayOfWeek = "Wedesday", Date = DateTime.Today.AddDays(-2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Thursday", Date = DateTime.Today.AddDays(-1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Friday", Date = DateTime.Today.AddDays(0) });
                    currentWeek.Add(new Day() { DayOfWeek = "Saturday", Date = DateTime.Today.AddDays(1) });
                    break;
                case "Saturday":
                    currentWeek.Add(new Day() { DayOfWeek = "Sunday", Date = DateTime.Today.AddDays(-6) });
                    currentWeek.Add(new Day() { DayOfWeek = "Monday", Date = DateTime.Today.AddDays(-5) });
                    currentWeek.Add(new Day() { DayOfWeek = "Tuesday", Date = DateTime.Today.AddDays(-4) });
                    currentWeek.Add(new Day() { DayOfWeek = "Wedesday", Date = DateTime.Today.AddDays(-3) });
                    currentWeek.Add(new Day() { DayOfWeek = "Thursday", Date = DateTime.Today.AddDays(-2) });
                    currentWeek.Add(new Day() { DayOfWeek = "Friday", Date = DateTime.Today.AddDays(-1) });
                    currentWeek.Add(new Day() { DayOfWeek = "Saturday", Date = DateTime.Today.AddDays(0) });
                    break;
            }

            return currentWeek;
        }


    }
}
