using System;
using System.Collections.Generic;
using prj3beer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                listViewBrand.Add(brand.brandName);             //Brands have already been validated by this point (app.xaml.cs)
            }
            listViewBrand.Sort();                               //Sort the list of brands alphabetically (default)
            brandListView.ItemsSource = listViewBrand;             //Setting the item source of our list view to the brands list
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
