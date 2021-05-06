using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        public string InteractionType { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public string? Notes { get; set; }

        public bool IsOpen { get; set; } = true;
        public bool IsBooked { get; set; } = false;
        public bool IsCompleted { get; set; } = false;

        [ForeignKey("Project")]
        public int? ProjID { get; set; }
        public Project? Project { get; set; }

    }
}
