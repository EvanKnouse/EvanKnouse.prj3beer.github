using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prj3beer.Services;
using prj3beer.Views;
using System.IO;

namespace prj3beer
{
    public partial class App : Application
    {

        public App(string dbPath)
        {
            InitializeComponent();

         
            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
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
