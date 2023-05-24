using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.CQRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.CQRS.Data
{
    public class MyAppDbContext : IdentityDbContext
    {
        private SeedData _seedData;
        public MyAppDbContext(DbContextOptions options, SeedData seedData) : base(options)
        {
            _seedData = seedData;
        }
        public DbSet<Department> tblDepartments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _seedData.SeedRoles(modelBuilder);
        }
    }
}
