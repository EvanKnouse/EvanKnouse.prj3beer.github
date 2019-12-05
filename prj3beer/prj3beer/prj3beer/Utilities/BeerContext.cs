using System;
using prj3beer.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using System.IO;

namespace prj3beer.Utilities
{
    /// <summary>
    /// Inherit Database Context, this will be responsible for CRUD operations with the Local Storage
    /// </summary>
    public class BeerContext : DbContext
    {
        /// <summary>
        /// Database set for Brands
        /// </summary>
        public DbSet<Brand> Brands { get; set; }

        // Add more DBsets<> here

        //Configuration for connecting to database which stores brand objects
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = "Beverages.db3";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    break;
                case Device.Android:
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbPath);
                    break;
            }
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
      
    }
}
