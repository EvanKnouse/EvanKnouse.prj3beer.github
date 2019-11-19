using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace prj3beer.Models
{
    public class DBHelper
    {
        public static string dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public static string fileName = "Brands.db";
        public string dbFullPath = Path.Combine(dbFolder, fileName);

       


    }
}
