using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using prj3beer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrandSelectPage : ContentPage
    {
        public ObservableCollection<Brand> Items { get; set; }

        public BrandSelectPage()
        {
            InitializeComponent();
            LocalStorage storage = new LocalStorage();
     
            foreach (Brand brand in storage.brandList)
            {
                Items.Add(brand);
            }
         
            MyListView.ItemsSource = Items;
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
