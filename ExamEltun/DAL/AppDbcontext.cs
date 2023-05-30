using ExamEltun.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Configuration;

namespace ExamEltun.DAL
{
    public class AppDbcontext:IdentityDbContext<AppUser>
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options):base(options) 
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Settings> Settings { get; set; }


    }
}
