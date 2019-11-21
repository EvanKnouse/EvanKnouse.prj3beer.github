using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using prj3beer.Models;
using Microsoft.EntityFrameworkCore;


namespace prj3beer.Models
{
    public class BeerContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        // Add more DBsets<> here



        private string _databasePath { get; set; }

        public BeerContext()
        {

        }

        public BeerContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
