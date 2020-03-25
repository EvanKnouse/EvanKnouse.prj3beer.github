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
        /// <param></param>
        public BeverageSelectPage()
        {
            InitializeComponent();
        }

        public void ReAppearing()
        {
            LogInOutButton();
        }

        #region Story 52 (The Carousel)

        private void PopulateCarousel()
        {
            // If there are no beverages favourited.
            if(updatedFavorites.Count() == 0)
            {
                // If the user is not searching for anything.
                if(searchBeverage.Text == null || searchBeverage.Text.Equals(""))
                {
                    // Display the no favourites label, telling the user to select a beverage and favourite it.
                    NoFavouritesLabel.IsVisible = true;
                }

                // Reset the FavouritesCarousel items source to the now empty list of favourited beverages.
                FavouritesCarousel.ItemsSource = null;
            }
            // There are favourited beverages.
            else
            {
                // Hide the no favourites label.
                NoFavouritesLabel.IsVisible = false;

                // Set the FavouritesCarousel items source to the list of favourited beverages.
                FavouritesCarousel.ItemsSource = updatedFavorites;

                // Set the FavouritesCarousel item template. This could be done in a different spot, since it only needs
                // to be done when the page is created initially, instead of every OnAppearance.
                FavouritesCarousel.ItemTemplate = new DataTemplate(() =>
                {
                    // TapGestureRecognizer for tapping the images in the carousel and going to the Status Page.
                    var tapGesRec = new TapGestureRecognizer();

                    // Event handler for the above TapGestureRecognizer.
                    tapGesRec.Tapped += (s, e) =>
                    {
                        toStatusPage(int.Parse(((Image)s).AutomationId));
                    };

                    // Image to be displayed in the carousel. SourceProperty is for displaying the image, AutomationIdProperty is
                    // for navigation to the Status Page.
                    Image image = new Image { };
                    image.SetBinding(Image.SourceProperty, "ImagePath");
                    image.SetBinding(Image.AutomationIdProperty, "BeverageID");

                    // Add the TapGestureRecognizer to the image.
                    image.GestureRecognizers.Add(tapGesRec);

                    image.WidthRequest = 200;
                    image.HeightRequest = 200;
                    
                    // Label for displaying the preference's BeverageID, "necessary" for UI testing. Should be removed in final product.
                    #if DEBUG
                        Label lblIDForTesting = new Label();
                        lblIDForTesting.SetBinding(Label.TextProperty, "BeverageID");
                        StackLayout stackLayout = new StackLayout
                        {
                            Children = { image, lblIDForTesting }
                        };
                    #else
                        StackLayout stackLayout = new StackLayout
                        {
                            Children = {image}
                        };
                    #endif
                    Frame frame = new Frame
                    {
                        HasShadow = true,
                        BackgroundColor = Color.Transparent,
                        BorderColor = Color.Gray,
                        CornerRadius = 5,
                        Margin = 20,
                        Padding = 0,
                        Content = stackLayout
                    };

                    StackLayout rootStackLayout = new StackLayout
                    {
                        Children = { frame }
                    };

                    return rootStackLayout;
                });
            }
        }
#endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Update the list of favorites to compare to when the search changes
            updatedFavorites = App.Context.Preference.Where(p => p.Favourite);

            // Show the carousel or no favourites label depending on if the user has favourites.
            PopulateCarousel();

            //Update the displayed list when the search page is returned to after a beverage is favorited
            if (searchBeverage.Text != null)SearchChanged(null, null);

            //  Setup The Menu Button
            LogInOutButton();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        /// <summary>
        /// This method will replace the Log In / Log Out button 
        /// </summary>
        private void LogInOutButton()
        {
            // Remove the Log in/out button
            ToolbarItems.RemoveAt(1);

            //bool loggedOut = (Settings.CurrentUserEmail.Length == 0) ? true : false;

            // If there is no current user signed in
            if (Settings.CurrentUserEmail.Length == 0)
            {
                // Create a new ToolBar Button
                ToolbarItem SignInButton = new ToolbarItem
                {   // Assign it the properties below
                    AutomationId = "SignIn",
                    Text = "Sign In",
                    // Set the menu button to the sub-menu
                    Order = ToolbarItemOrder.Secondary
                };
                // Add the button to the menu
                ToolbarItems.Add(SignInButton);
            }
            else // A user is signed in
            {   // Create a new toolbar button caled Sign Out
                ToolbarItem SignOutButton = new ToolbarItem
                {   // Assign it the properties below
                    AutomationId = "SignOut",
                    Text = "Sign Out",
                    // Set the menu button to the sub-menu
                    Order = ToolbarItemOrder.Secondary
                };
                // Add the button to the menu
                ToolbarItems.Add(SignOutButton);
            }
            // Add the click event handler to the button
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
                brand = brands.FirstOrDefault().BrandID;
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
                //Create seperate lists to sort by favorite
                List<string> listFavorites = new List<string>();
                List<string> listNonFav = new List<string>();

                foreach (var beverage in beverages)
                {
                    try
                    {
                        //It is favorited
                        if (updatedFavorites.Contains(App.Context.Preference.Find(beverage.BeverageID)))
                            //Add to the favorite list with a star
                            listFavorites.Add(beverage.Name + "    \u2b50");
                        //Not favorited
                        else
                            //Add to not favorite list
                            listNonFav.Add(beverage.Name);
                    }

                    //Will be called if a preference for a beverage does not exist
                    catch(Exception exp)
                    {
                        //Assume it's not a favorite if it has never gone to that page
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
            //Seperate the beverage name
            string name = e.Item.ToString();

            //Check if there is a star
            int starIndex = name.IndexOf("    \u2b50");

            // -1 means no star, otherwise cut out the star
            if (starIndex != -1) name = name.Substring(0, starIndex);

            //Get the beverage tapped
            Beverage tappedBeverage = (App.Context.Beverage.Where(b => b.Name.Contains(name))).First();

            toStatusPage(tappedBeverage.BeverageID);

            /*
            Beverage tappedBeverage = (App.Context.Beverage.Where(b => b.Name.Contains(e.Item.ToString()))).First();
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

        /// <summary>
        /// This method is called when the Settings Menu Button is Clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Clicked(object sender, EventArgs e)
        {
            // Push a new settings modal
            Navigation.PushModalAsync(new NavigationPage(new SettingsMenu()));
        }

        /// <summary>
        /// This method is called when the Sign In or Out button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SignInOut_Clicked(object sender, EventArgs e)
        {   
            // Push a new login page modal
            await Navigation.PushModalAsync(new CredentialSelectPage());
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