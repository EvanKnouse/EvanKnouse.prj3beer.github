using Xamarin.Forms;
using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace prj3beer
{
    public partial class App : Application
    {
        public static UserProfile CurrentUser;
        public static bool WelcomeMessage;

        public App()
        {
            InitializeComponent();

            if(CurrentUser == null)
            {
                CurrentUser = new UserProfile();
            }
            WelcomeMessage = true;

            // Set Up App Default Presets
            SetUpPreset();

            MainPage = new MainPage();
        }

        private void SetUpPreset()
        {
            // Set the default URL of API to default
            Settings.URLSetting = default;

            // TODO: Replace With Functioning BlueTooth Device
            MockTempReadings.StartCounting();

            // Instantiate a new Context (Database)
            BeerContext context = new BeerContext();

            //Instantiate a new API Manager
            APIManager apiManager = new APIManager();

            // Connect to the API and store Beverages/Brands in the Database
            FetchData(context, apiManager);
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
