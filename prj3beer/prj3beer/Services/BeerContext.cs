using Microsoft.EntityFrameworkCore;
using prj3beer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Services
{
    class BeerContext : DbContext
    {
        public DbSet<Beverage> Beverages { get; set; }

        private string _databasePath;

        public void DatabaseContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }

    }
}
