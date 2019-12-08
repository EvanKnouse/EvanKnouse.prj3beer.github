using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using prj3beer.Models;
using prj3beer.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrandSelectPage : ContentPage
    {
        /// <summary>
        /// Constructor for the Brand Select Page
        /// </summary>
        public BrandSelectPage(List<Brand> brands)
        {
            InitializeComponent();
            List<String> listViewBrand = new List<String>();
            foreach (Brand brand in brands)
            {
                listViewBrand.Add(brand.brandName);
            }
            
            MyListView.ItemsSource = listViewBrand;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
