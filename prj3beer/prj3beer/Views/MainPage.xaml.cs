using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using prj3beer.Models;
using prj3beer.Services;
using Microsoft.Data.Sqlite;
using prj3beer.ViewModels;

namespace prj3beer.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {   // Static Database
        static BeerContext context;
        // Static ViewModel
        static StatusViewModel svm;
        // Static Beverage (the one currently selected)
        static Beverage currentBeverage;

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        /// <summary>
        /// Mainpage - Takes in the database that was passed in to it. 
        /// </summary>
        /// <param name="context">Database</param>
        public MainPage(BeerContext context)
        {   // Initialize the page
            InitializeComponent();

            // Set this page's context to the one passed in
            MainPage.context = context;

            // Set this page's View Model to a new Status View Model
            svm = new StatusViewModel();

            // Set values for the current Beverage (Find the 2nd beverage for mocking)
            currentBeverage = context.Beverage.Find(2);



            MasterBehavior = MasterBehavior.Popover;
            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {   // Add the Status Screen to the Navigation Menu
                    case (int)MenuItemType.Status:
                        // Pass in the Status View Model, Context (Database) and the current beverage object
                        MenuPages.Add(id, new NavigationPage(new Status(svm, context, currentBeverage)));
                        break;
                    //case (int)MenuItemType.Browse:
                    //    MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                    //    break;
                    //case (int)MenuItemType.About:
                    //    MenuPages.Add(id, new NavigationPage(new AboutPage()));
                    //    break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}