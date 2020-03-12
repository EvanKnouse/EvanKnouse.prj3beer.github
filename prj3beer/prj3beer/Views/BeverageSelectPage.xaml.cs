using System;
using System.Collections.Generic;
using prj3beer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Text.RegularExpressions;
using prj3beer.Services;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    //This page will contain a search bar and a potential list of beverages. Defaults to
    //No beverages displayed if search bar is left blank
    public partial class BeverageSelectPage : ContentPage
    {
        //A list that will contain all valid beverages that meet the search criteria
        List<string> listViewBeverages = new List<string>();

        private IQueryable<Preference> updatedFavorites;

        /// <summary>
        /// This will initialize the page and bring in the beverage objects from local storage
        /// and places in a context
        /// </summary>
        /// <param name="beerContext"></param>
        public BeverageSelectPage()
        {
            InitializeComponent();
            
            // Check to see if the welcome prompt has fired since the user has logged in
            //if (Settings.WelcomePromptSetting)
            //{   // If it fires, disable it from firing again
            //    Settings.WelcomePromptSetting = false;
            //    // If it hasn't been shown yet, then push a new modalscreen to the user.
            //    Navigation.PushModalAsync(new WelcomeModal());
            //}
        }

        #region Story 52 (The Carousel)

        private void PopulateCarousel()
        {
            //IQueryable<Preference> favPrefs = App.Context.Preference.Where(p => p.Favourite == true);

            if(updatedFavorites.Count() == 0)
            {
                NoFavouritesLabel.IsVisible = true;
                NoFavouritesLabel.Text = "Favourite, get spinny";

                FavouritesCarousel.ItemsSource = null;
                //NoFavouritesLabelHeight = NoFavouritesLabel.Height;
            }
            else
            {
                NoFavouritesLabel.IsVisible = false;

                FavouritesCarousel.ItemsSource = updatedFavorites;

                FavouritesCarousel.ItemTemplate = new DataTemplate(() =>
                {
                    var tapGesRec = new TapGestureRecognizer();

                    tapGesRec.Tapped += (s, e) =>
                    {
                        toStatusPage(int.Parse(((Image)s).AutomationId));
                    };

                    Image image = new Image { };
                    image.SetBinding(Image.SourceProperty, "ImagePath");
                    image.SetBinding(Image.AutomationIdProperty, "BeverageID");

                    image.GestureRecognizers.Add(tapGesRec);


                    image.WidthRequest = 200;
                    image.HeightRequest = 200;
                    //image.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    //image.VerticalOptions = LayoutOptions.CenterAndExpand;

                    StackLayout stackLayout = new StackLayout
                    {
                        //HeightRequest = 200,
                        //WidthRequest = 200,
                        //HorizontalOptions = LayoutOptions.CenterAndExpand,
                        //VerticalOptions = LayoutOptions.CenterAndExpand,
                        Children = { image }
                    };

                    Frame frame = new Frame
                    {
                        HasShadow = true,
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.Gray,
                        CornerRadius = 5,
                        Margin = 20,
                        Padding = 0,
                        //HeightRequest = 350,
                        //WidthRequest = 100,
                        //HorizontalOptions = LayoutOptions.Center,
                        //VerticalOptions = LayoutOptions.CenterAndExpand,
                        Content = stackLayout
                    };
                    StackLayout rootStackLayout = new StackLayout
                    {
                        Children = { frame }
                    };

                    //rootStackLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    //rootStackLayout.VerticalOptions = LayoutOptions.StartAndExpand;
                    return rootStackLayout;
                });

                //carouselHeight = FavouritesCarousel.Height;
            }
        }

        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Update the list of favorites to compare to when the search changes
            updatedFavorites = App.Context.Preference.Where(p => p.Favourite);

            PopulateCarousel();

            //Update the displayed list when the search page is returned to after a beverage is favorited
            if (searchBeverage.Text != null)SearchChanged(null, null);

            LogInOutButton();
        }

        private void LogInOutButton()
        {
            ToolbarItems.RemoveAt(1);

            bool SignInOut = (Settings.CurrentUserEmail.Length == 0 || Settings.CurrentUserName.Length == 0) ? true : false;

            if (SignInOut)
            {
                ToolbarItem SignInButton = new ToolbarItem
                {
                    AutomationId = "SignIn",
                    Text = "Sign In",
                    Order = ToolbarItemOrder.Secondary
                };

                ToolbarItems.Add(SignInButton);
            }
            else
            {
                ToolbarItem SignOutButton = new ToolbarItem
                {
                    AutomationId = "SignOut",
                    Text = "Sign Out",
                    Order = ToolbarItemOrder.Secondary
                };

                ToolbarItems.Add(SignOutButton);
            }

            ToolbarItems.ElementAt(1).Clicked += SignInOut_Clicked;
        }

        /// <summary>
        /// This method will take an a search input change event and attempt to
        /// find any beverages that list the criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            //Display the loading spinner
            loadingSpinner.IsRunning = true;

            //Make a new listview of beverages - essentially resetting it
            listViewBeverages = new List<string>();

            //Grab the text of the search string, trim it, and make it all lower case
            string searchString = searchBeverage.Text.ToString().Trim();

            searchString = Regex.Replace(searchString, @"\s+", " ");
            // grab the text of the search string without modifying it, for use in the error message label
            string potentialInvalidSearch = searchString;
            searchString = searchString.ToLower();

            //Uses the entity framework to find beverages which might the criteria
            //Checks on 3 different fields (brandName, Name of beverage, and the type) and stores it

            //Check to see if the search string finds any brands with a mtaching name
            var brands = App.Context.Brand.Where(b => b.Name.ToLower().Contains(searchString)).Distinct();

            //Initialize a nullable integer to store the brand ID
            int? brand = null;

            try
            {
                //Try to save the result from the previous search
                brand = brands.First().BrandID;
            }
            catch (Exception)
            {
                //If there is an exception, reset the brand ID to null
                brand = null;
            }

            // Search the Beverages Database for search string and brand ID that matches
            // Added AsEnumerable() for reasons not completely unbeknownst.
            var beverages = App.Context.Beverage.AsEnumerable().Where(b => b.BrandID.Value.Equals(brand) || b.Name.ToLower().Contains(searchString) || b.Type.ToString().ToLower().Contains(searchString)).Distinct();

            //If the search string is not empty
            if (!searchString.Equals(""))
            {
                //hide the error message
                errorLabel.IsVisible = false;


                #region Story 52 (Sort favorites first)

                List<string> listFavorites = new List<string>();
                List<string> listNonFav = new List<string>();

                foreach (var beverage in beverages)
                {
                    try
                    {
                        if (updatedFavorites.Contains(App.Context.Preference.Find(beverage.BeverageID)))
                            listFavorites.Add(beverage.Name + "    \u2b50");
                        else
                            listNonFav.Add(beverage.Name);
                    }
                    catch(Exception exp)
                    {
                        listNonFav.Add(beverage.Name);
                    }
                }

                //Sort the list seperatly
                listFavorites.Sort();
                listNonFav.Sort();

                //Combine the lists
                listViewBeverages.AddRange(listFavorites);
                listViewBeverages.AddRange(listNonFav);
                #endregion

                //for each beverage add it to the list
                /*
                foreach (var beverage in beverages)
                {
                    listViewBeverages.Add(beverage.Name);
                }
                */

                //Sort beverage list alphabetically for display
                //listViewBeverages.Sort();


                //If there are no beverages
                if (listViewBeverages.Count() == 0)
                {
                    //set the item source to null (make it empty)
                    beverageListView.ItemsSource = null;
                    // display the error label
                    errorLabel.IsVisible = true;
                    //append the current search into the error message
                    errorLabel.Text = "\"" + potentialInvalidSearch + "\" could not be found/does not exist";
                }
                else //there are beverages
                {
                    //Set spinner hidden to true
                    beverageListView.ItemsSource = listViewBeverages;
                }
            }
            else //search string is empty
            {
                //hide the error message
                errorLabel.IsVisible = false;
                //reset the list
                beverageListView.ItemsSource = null;
            }
            //hide the load spinner
            loadingSpinner.IsRunning = false;
        }

        /// <summary>
        /// Selecting a beverage from the list
        /// Sets the settings page prefferd ID to be used on the stauts page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeverageTapped(object sender, ItemTappedEventArgs e)
        {
            //Get the beverage tapped
            Beverage tappedBeverage = (App.Context.Beverage.Where(b => b.Name.Contains(e.Item.ToString()))).First();

            toStatusPage(tappedBeverage.BeverageID);

            /*

            //Get that beverage's ID
            Settings.BeverageSettings = tappedBeverage.BeverageID;
            //Application.Current.MainPage = new NavigationPage(new StatusPage());
            // await Navigation.PushModalAsync(new NavigationPage(new StatusPage()));

            //Set the ID to the setting page
            var id = (int)MenuItemType.Status;

            //Go to the settings page, done like this to keep the menu - May need to be changed later
            //await RootPage.NavigateFromMenu(id);
            await Navigation.PushAsync(new StatusPage());

    */
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new SettingsMenu()));
        }

        private async void SignInOut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CredentialSelectPage(false)));
        }

        #region stroy 52 (Navigate and hide)

        private async void toStatusPage(int bevId)
        {
            Settings.BeverageSettings = bevId;
            //Application.Current.MainPage = new NavigationPage(new StatusPage());
            // await Navigation.PushModalAsync(new NavigationPage(new StatusPage()));

            //Set the ID to the setting page
            //var id = (int)MenuItemType.Status; // Doesn't seem to be used.

            //Go to the settings page, done like this to keep the menu - May need to be changed later
            //await RootPage.NavigateFromMenu(id);
            await Navigation.PushAsync(new StatusPage());
        }


        private void SearchBeverageFocused(object sender, FocusEventArgs e)
        {
            FavouritesCarousel.IsVisible = false;
            NoFavouritesLabel.IsVisible = false;

            if(searchBeverage.Text == null || searchBeverage.Text.Equals(""))
            {
                /*
                List<String> bevNames = new List<String>();
                IQueryable<Beverage> iQueryBevs = App.Context.Beverage.Where(b=>b.BrandID>0);
                //bevs = iQueryBevs.ToList();

                foreach(Beverage  bev in iQueryBevs)
                {
                    bevNames.Add(bev.Name);
                }

                beverageListView.ItemsSource = bevNames;
                */
            }
        }

        private void SearchBeverageUnfocused(object sender, FocusEventArgs e)
        {
            //DependencyService.Get<IToastHandler>().LongToast("SearchBev");
            
            if(searchBeverage.Text == null || searchBeverage.Text.Equals(""))
            {
                if(updatedFavorites.Count() == 0)
                {
                    NoFavouritesLabel.IsVisible = true;
                }
                else
                {
                    FavouritesCarousel.IsVisible = true;
                    //errorLabel.IsVisi
                }

                //FavouritesCarousel.HeightRequest = carouselHeight*3;
                //NoFavouritesLabel.HeightRequest = NoFavouritesLabelHeight;
                //beverageListView.ItemsSource = null;
            }
        }

        #endregion
    }
}