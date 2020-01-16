using prj3beer.Models;
using prj3beer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectBeveragePage : ContentPage
    {
        public SelectBeveragePage()
        {
            InitializeComponent();

            // Initialize a list to store the beverages from
            List<Beverage> beverageList = new List<Beverage>();
            List<string> validBeverages = new List<string>();

            // Setup the context to access our stored beverages
            BeerContext context = new BeerContext();

            // Store the local beverages in the list
            beverageList = context.Beverage.ToList();

            // Perform validation on the list, removing invalid beverages
            beverageList.ForEach(e => { if (ValidationHelper.Validate(e).Count == 0) { validBeverages.Add(e.Name); } });

            validBeverages.Sort((a, b) => { return string.Compare(a, b); }) ;
            beverageListView.ItemsSource = validBeverages;

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