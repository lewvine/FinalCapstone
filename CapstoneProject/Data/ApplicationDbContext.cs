using CapstoneProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Salesperson> Salespeople { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Grass> Grasses { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = "Salesperson",
                    NormalizedName = "SALESPERSON"
                },
                new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
                );
            builder.Entity<Grass>()
                .HasData(
                new Grass
                {
                    id = 1,
                    Name = "Tall Fescue",
                    Info = "A cool season grass ideally suited for climates with cool summers and mild winters.  Stays green all year round!",
                    Cost = 2.50,
                    ImageUrl = "/images/TallFescue.PNG"
                },
                new Grass
                {
                    id = 2,
                    Name = "Bermuda",
                    Info = "A warm season grass best suited for climates with hot summers and moderate winters.  Spreads on it's own and browns in the winter.",
                    Cost = 3.50,
                    ImageUrl = "/images/BermudaGrass.PNG"
                },
                new Grass
                {
                    id = 3,
                    Name = "Kentucky Bluegrass",
                    Info = "A warm season grass known for it's bluish tint.  Great for Southern landscapes.  Tolerates heat well.",
                    Cost = 4.50,
                    ImageUrl = "/images/KentuckyBluegrass.PNG"
                });
        }
    }
}
