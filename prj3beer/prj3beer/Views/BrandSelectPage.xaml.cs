using System;
using System.Collections.Generic;
using prj3beer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prj3beer.Services;
using System.Linq;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrandSelectPage : ContentPage
    {
        BeerContext context = new BeerContext();
        List<String> listViewBeverages = new List<String>();
        /// <summary>
        /// Constructor for the Brand Select Page
        /// </summary>
        public BrandSelectPage(List<Beverage> beverageList, BeerContext beerContext)
        {
            InitializeComponent();
            context = beerContext;




            /*List<String> listViewBrand = new List<String>();
            foreach (Brand brand in brands)
            {
                listViewBrand.Add(brand.brandName);             //Brands have already been validated by this point (app.xaml.cs)
            }
            listViewBrand.Sort();                               //Sort the list of brands alphabetically (default)
            brandListView.ItemsSource = listViewBrand;*/           //Setting the item source of our list view to the brands list
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            loadingSpinner.IsRunning = true;

            //Make spinner hidden = false
            listViewBeverages = new List<string>();
            String searchString = searchBeverage.Text;
            var beverages = context.Beverage.Where(b => b.Brand.brandName.ToLower().Contains(searchString.ToLower()) || b.Name.ToLower().Contains(searchString.ToLower()) || b.Type.ToString().ToLower().Contains(searchString.ToLower())).Distinct();

            if (!searchString.Equals(""))
            {
                errorLabel.IsVisible = false;

                foreach (var beverage in beverages)
                {
                    listViewBeverages.Add(beverage.Name);
                }

                if (listViewBeverages.Count() == 0)
                {
                    beverageListView.ItemsSource = null;
                    // shows up instantly at the moment, may want to add some sort of debounce
                    errorLabel.IsVisible = true;
                    errorLabel.Text = "\"" + searchString + "\" could not be found/does not exist";
                }
                else
                {
                    //Set spinner hidden to true
                    beverageListView.ItemsSource = listViewBeverages;
                }
            }
            else
            {
                errorLabel.IsVisible = false;

                beverageListView.ItemsSource = null;
            }

            loadingSpinner.IsRunning = false;
        }
    }
}