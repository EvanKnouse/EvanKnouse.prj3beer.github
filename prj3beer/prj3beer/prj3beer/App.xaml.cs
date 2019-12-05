using Xamarin.Essentials;
using Xamarin.Forms;
using prj3beer.Utilities;
using System.Linq;
using prj3beer.Models;
using Microsoft.Data.Sqlite;


namespace prj3beer
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();


            BeerContext bc = new BeerContext();
            bc.Database.EnsureCreated();
            if (System.Diagnostics.Debugger.IsAttached) { loadFixtures(bc); }
            
            MainPage = new NavigationPage(new prj3beer.Views.MainPage(bc));
        }

        private async void loadFixtures(BeerContext bc)
        {
            try
            {
                await bc.Database.EnsureDeletedAsync();
                await bc.Database.EnsureCreatedAsync();
                var count = bc.Brands.Count();
                if (count == 0)
                {
                    bc.Brands.Add(new Brand() { brandID = 4, brandName = "Great Western Brewery" });
                    bc.Brands.Add(new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" });
                    bc.Brands.Add(new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" });
                    bc.Brands.Add(new Brand() { brandID = 7, brandName = new string('a', 61) });
                    bc.Brands.Add(new Brand() { brandID = 3, brandName = "" });
                }
                await bc.SaveChangesAsync();
            }
            catch (SqliteException)
            {
                throw;
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
