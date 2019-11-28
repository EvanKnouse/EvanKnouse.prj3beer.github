using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using prj3beer.Models;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Allows you to use SQLite for all CRUD operations
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   // Setup the optionsbuilder to use SQLite using the passed in path
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
