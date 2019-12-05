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
    {
        static BeerContext context;
        static StatusViewModel svm;
        static Beverage currentBeverage;

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage(BeerContext context)
        {
            InitializeComponent();

            MainPage.context = context;

            svm = new StatusViewModel();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Status:
                        MenuPages.Add(id, new NavigationPage(new Status(svm, context, currentBeverage)));
                        break;
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
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