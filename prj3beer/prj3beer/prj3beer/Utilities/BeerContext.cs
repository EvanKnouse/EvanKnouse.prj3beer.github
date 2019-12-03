using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using prj3beer.Models;
using Microsoft.EntityFrameworkCore;
using prj3beer.Utilities;
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
            var dbPath = "SQLiteDataBase.db3";
            switch (Device.RuntimePlatform)
            {
                //case Device.iOS:
                case Device.Android:
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbPath);
                    break;
            }
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        // Getter/Setter for the database path
        private string _databasePath { get; set; }

        /// <summary>
        /// Empty Constructor for Beer Context
        /// </summary>
        public BeerContext()
        {

        }

        /// <summary>
        /// Constructor that takes in a database path
        /// </summary>
        /// <param name="databasePath"></param>
        public BeerContext(string databasePath)
        {   // This will set the database path from the passed in string
            _databasePath = databasePath;
        }

      
    }
}
