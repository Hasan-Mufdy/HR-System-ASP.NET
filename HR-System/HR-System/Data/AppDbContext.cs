using HR_System.Controllers;
using HR_System.Models;
using HR_System.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Data
{
    public class AppDbContext : IdentityDbContext<AuthUser>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Position> Positions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "admin", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "HR", Name = "HR", NormalizedName = "HR" },
            new IdentityRole { Id = "User", Name = "User", NormalizedName = "User" }
            );

            var adminUser = new AuthUser
            {
                UserName = "Admin",
                Email = "admin@a.com",
                IsApproved = true,
            };

            var passwordHasher = new PasswordHasher<AuthUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin!23");

            modelBuilder.Entity<AuthUser>().HasData(adminUser);

            base.OnModelCreating(modelBuilder);
        }

    }
}
