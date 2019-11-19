using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;


namespace prj3beer.Models
{
    public class BrandContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        private string _databasePath { get; set; }

        public BrandContext()
        {

        }

        public BrandContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
