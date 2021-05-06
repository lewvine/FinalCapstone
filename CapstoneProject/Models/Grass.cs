using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Grass
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public double Cost { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Project> Projects { get; set; } 
    }
}
