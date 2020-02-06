using Xamarin.Forms;
using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using prj3beer.ViewModels;

namespace prj3beer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Settings.URLSetting = default;

            StatusViewModel.timerOn = false;

            //This was moved to improve reliability of the tests - potentially moving some time
            //MockTempReadings.StartCounting();

            // Instantiate a new Context (Database)
            BeerContext context = new BeerContext();

            //Instantiate a new API Manager
            APIManager apiManager = new APIManager();

            // Connect to the API and store Beverages/Brands in the Database
            FetchData(context, apiManager);
          
            MainPage = new MainPage(context);
        }

        public static async void FetchData(BeerContext context, APIManager apiManager)
        {
            // REMOVE FOR PERSISTENT Data
            context.Database.EnsureDeleted();

            // Ensure the Database is Created
            context.Database.EnsureCreated();

            // Set URL of api Manager to point to the Brands API
            // Load the Brands that Validate into the Local Storage
            context.Brands.AddRange(await apiManager.GetBrandsAsync());

            // Set URL of api Manager to point to the Beverage API
            // Load the Beverages that Validate into the Local Storage
            context.Beverage.AddRange(await apiManager.GetBeveragesAsync());

            // Save changes to the Local Storage
            Task databaseWrite = context.SaveChangesAsync();
            databaseWrite.Wait();
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
