using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Utilities;
using System.Linq;
using prj3beer.Models;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace prj3beer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //Story 01
            MockTempReadings.StartCounting();

            //Story20
            BeerContext context = new BeerContext();
            context.Database.EnsureCreated();
            //Pre-load list only for debug purposes in production - for now, load list anyway
            //if (System.Diagnostics.Debugger.IsAttached) { loadFixtures(context); }

            //Load Brands list
            loadFixtures(context);

            MainPage = new MainPage(context);
        }

        private async void loadFixtures(BeerContext bc)
        {
            try
            {
                await bc.Database.EnsureDeletedAsync();
                await bc.Database.EnsureCreatedAsync();
                var count = bc.Brands.Count();
                //If count is 0, add new brands to the brand list for validation, before sending to database/context
                if (count == 0)
                {
                    List<Brand> brandList = new List<Brand>();
                    brandList.Add(new Brand() { brandID = 4, brandName = "Great Western Brewery" });
                    brandList.Add(new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" });
                    brandList.Add(new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" });
                    brandList.Add(new Brand() { brandID = 7, brandName = new string('a', 61) });
                    brandList.Add(new Brand() { brandID = 3, brandName = "" });

                    ValidateBrands(brandList, bc);
                }
                await bc.SaveChangesAsync();
            }
            catch (SqliteException)
            {
                throw;
            }
        }

        /// <summary>
        /// This helper function will validate all of the brands in the brands list 
        /// that is being loaded into the app from the database fixture before saving into the database
        /// </summary>
        /// <param name="brandList"></param>
        /// <param name="bc"></param>
        private void ValidateBrands(List<Brand> brandList, BeerContext bc)
        {
            foreach (Brand brand in brandList)
            {
                if (ValidationHelper.Validate(brand).Count() == 0)               //If the validation returns 0 for count, brand is valid
                {
                    bc.Brands.Add(brand);
                }
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
