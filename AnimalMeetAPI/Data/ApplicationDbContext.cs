using AnimalMeetAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } 
        public DbSet<AnimalType> Animals { get; set; } 
        public DbSet<AnimalSubtype> AnimalSubtypes { get; set; } 
        public DbSet<Pets> Pets { get; set; } 
    }

}
