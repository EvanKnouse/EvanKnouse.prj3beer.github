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
        public BrandSelectPage()
        {   
            InitializeComponent();

            Load();  
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        private async void Load()
        {
            try
            {
                BeerContext bc = new BeerContext();
              
                await bc.Database.EnsureCreatedAsync();
                int count = bc.Brands.Count();
                if (count == 0)
                {
                    List<Brand> unvalidatedBrands = new List<Brand>();
                    List<Brand> validatedBrands = new List<Brand>();

                    unvalidatedBrands.Add(new Brand() { brandID = 4, brandName = "Great Western Brewery" });
                    unvalidatedBrands.Add(new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" });
                    unvalidatedBrands.Add(new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" });
                    unvalidatedBrands.Add(new Brand() { brandID = 7, brandName = new string('a', 61) });
                    unvalidatedBrands.Add(new Brand() { brandID = 3, brandName = "" });

                    foreach(Brand brand in unvalidatedBrands)
                    {
                        if (ValidationHelper.Validate(brand).Count == 0)
                        {
                            validatedBrands.Add(brand);
                        }
                    }

                    foreach(Brand brand in validatedBrands)
                    {
                        bc.Brands.Add(brand);
                    }


                    //Brand GWBbrand = new Brand() { brandID = 4, brandName = "Great Western Brewery" };
                    // Brand CBCbrand = new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" };
                    // Brand PSBbrand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" };

                    //bc.Brands.Add(GWBbrand);
                    //bc.Brands.Add(CBCbrand);
                    //bc.Brands.Add(PSBbrand);

                    await bc.SaveChangesAsync();
                }
                List<Brand> brandList = bc.Brands.ToList();
                MyListView.ItemsSource = brandList;

            }
            catch (SqliteException)
            {
                throw new Exception("Could not connect to database");
            }
           
        }
    }
}
