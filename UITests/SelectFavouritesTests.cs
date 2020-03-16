using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer;
using prj3beer.Models;
using prj3beer.ViewModels;
using prj3beer.Services;
using Xamarin.Forms;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class SelectFavouritesTests
    {
        //Instead of querying on any (in case its empty) just make sure it contains the correct number of beverages
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        //static StatusViewModel svm;

        //Set some drinks to begin with
        List<string> beverages = new List<string> { "Churchill Blonde Lager", "Great Western Pilsner", "Great Western Radler", "Original 16 Copper Ale", "Rebellion Zilla IPA" };

        //These are positions of items in the list of the select beverage page
        //Hard coded just for convinience
        const int first = 750;
        const int second = 900;
        const int third = 1050;
        const int fourth = 1200;
        const int fifth = 1350;

        public SelectFavouritesTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();

            app.WaitForElement("searchBeverage"); // Wait for the beverage select page to load.

            //svm = new StatusViewModel();
        }

        /// <summary>
        /// Selects a drink form the beverage select page
        /// </summary>
        /// <param name="searchBeverage"> What is inputed into the search bar </param>
        /// <param name="placement"> The position of the beverage in the list to tap </param>
        public void SelectABeverage(string searchBeverage, int placement)
        {
            // tap to navigate to the beverage select page
            // Assumes app.EnterText knows where to enter text
            //app.Tap("Beverage Select");

            app.EnterText("searchBeverage", searchBeverage.ToString()); //Entering code into the search bar
            app.TapCoordinates(200, placement); //Tapping a result from the list gotten from the search
        }

        /// <summary>
        /// Sets a preference to not be favorite
        /// </summary>
        /// <param name="sFullBevName"></param>
        public void RemoveFavourite(string sFullBevName)
        {
            //IQueryable<Preference> favorites = svm.Context.Preference.Where(c => c.Favourite == true);

            int bTemp = App.Context.Beverage.Find(sFullBevName).BeverageID; //Convert the beverage name to an ID

            RemoveFavourite(bTemp); //Remove it with code friendly version

        }
        
        /// <summary>
        /// Alternative method of removing a favorite
        /// More code friendly, but less user friendly
        /// </summary>
        /// <param name="bevID"> ID of the beverage preference to unfavorite </param>
        public void RemoveFavourite(int bevID)
        {
            Preference pref = App.Context.Preference.Find(bevID); //Find the preference

            pref.Favourite = false; //Remove the preference as a favorite
            App.Context.Preference.Update(pref); //Update it
        }

        /// <summary>
        /// Sets all preferences that were saved as a favorite to no longer be a favorite
        /// </summary>
        public void RemoveAllFavourites()
        {
            IQueryable<Preference> favorites = App.Context.Preference.Where(c => c.Favourite == true); //Get how many favorites exist
            Preference pref;
            for (int i = 0; i < favorites.Count(); i++) //For all the favorites that exist
            {
                pref = favorites.ElementAt(i); //Set it as the current
                pref.Favourite = false; //Remove it as a favorite
                App.Context.Preference.Update(pref); //Update it
                
            }
        }
        
        // Helper method for favouriting/unfavouriting a beverage through the UI.
        private void SetFavouriteStatusOfBeverageUI(string sBevName, int iListPosition, bool bLooseFocus)
        {
            app.EnterText("searchBeverage", sBevName); // Enter the search criteria into the search bar.

            app.TapCoordinates(715, iListPosition); // Tap the beverage option in the list.

            app.WaitForElement("FavouriteButton"); // Wait for the status page to load.
            app.TapCoordinates(715, 2130); // Tap the favourite button for that beverage.

            app.Back(); // Go back to the beverage select page.

            app.WaitForElement("searchBeverage"); // Wait for the beverage select page to load.
            app.TapCoordinates(1350, 360); // Tap the 'X' on the search bar to clear it.

            if(bLooseFocus)
            {
                app.TapCoordinates(715, 515); // Tap below the search bar to remove focus.
            }
        }

        // Favourite a beverage and test that it is displayed in the carousel.
        [Test]
        public void UserSeesAFavoritedBeverageOnTheBeverageSelectPage()
        {
            //FavouriteADrink("Churchill Blonde Lager");
            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and favourite it.

            AppResult[] favBev = app.Query("5"); // Query the beverage select page for the beverage's ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.
        }

        // Favourite a beverage and test that it is displayed in the carousel, then
        // unfavourite that beverage and test that it is not displayed in the carousel.
        [Test]
        public void UserNoLongerSeesABeverageOnTheBeverageSelectPageAfterItIsRemovedAsAFavorited()
        {
            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and favourite it.

            AppResult[] favBev = app.Query("5"); // Query the beverage select page for the beverage's ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.

            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and unfavourite it.

            favBev = app.Query("5"); // Query the beverage select page for the beverage's ID.
            Assert.IsFalse(favBev.Any()); // Passes if the ID is not shown on the page.
        }

        // Favourite two beverages and test that they are displayed in the carousel.
        [Test]
        public void UserSeesMultipleFavoritedBeveragesOnTheBeverageSelectPage()
        {
            SetFavouriteStatusOfBeverageUI("c", first, false); // Select "Churchill Blonde Lager" and favourite it.
            SetFavouriteStatusOfBeverageUI("c", second, true); // Select "Great Western Pilsner" and favourite it.

            AppResult[] favBev = app.Query("5"); // Query the beverage select page for the "Churchill Blonde Lager" ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.

            favBev = app.Query("2"); // Query the beverage select page for the "Great Western Pilsner" ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.
        }

        // Favourite five beverages and test that they are all displayed in the carousel.
        [Test]
        public void UserSeesAllFiveOfTheirFavoritedBeveragesOnTheBeverageSelectPage()
        {
            SetFavouriteStatusOfBeverageUI("e", first, false); // Select "Churchill Blonde Lager" and favourite it.
            SetFavouriteStatusOfBeverageUI("e", second, false); // Select "Great Western Pilsner" and favourite it.
            SetFavouriteStatusOfBeverageUI("e", third, false); // Select "Great Western Radler" and favourite it.
            SetFavouriteStatusOfBeverageUI("e", fourth, false); // Select "Original 16 Copper Ale" and favourite it.
            SetFavouriteStatusOfBeverageUI("e", fifth, true); // Select "Rebellion Pear Beer" and favourite it.

            AppResult[] favBev = app.Query("1"); // Query the beverage select page for the "Great Western Radler" ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.

            favBev = app.Query("2"); // Query the beverage select page for the "Great Western Pilsner" ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.

            app.SwipeRightToLeft(0.9, 500, true);

            favBev = app.Query("3"); // Query the beverage select page for the "Original 16 Copper Ale" ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.

            app.SwipeRightToLeft(0.9, 500, true);

            favBev = app.Query("5"); // Query the beverage select page for the "Churchill Blonde Lager" ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.

            favBev = app.Query("99"); // Query the beverage select page for the "Rebellion Pear Beer" ID.
            Assert.IsTrue(favBev.Any()); // Passes if the ID is shown on the page.
        }

        // Favourite a beverage and test that it is displayed at the first position of the list with a symbol.
        [Test]
        public void UserSeesTheirFavoriteDrinksAppearWithASpecialSymbolAndAboveOtherDrinksInTheBeverageSelectPage()
        {
            SetFavouriteStatusOfBeverageUI("c", second, false); // Select "Great Western Pilsner" and favourite it.

            app.EnterText("searchBeverage", "c"); // Search for the recently favourited beverage.

            AppResult[] favBev = app.Query(beverages[1] + "    \u2b50"); // Query the beverage select page for the "Great Western Pilsner" list option.
            float fBevPos = favBev[0].Rect.CenterY; // Get the Y position of the list option.

            Assert.IsTrue(fBevPos > 655 && fBevPos < 810); // Passes if the Y position of the list option is at the first position of the list.
        }

        // Favourite a beverage and test that it is displayed at the first position of the list, then
        // unfavourite that beverage and test that is is displayed in the second position of the list.
        [Test]
        public void UserSeesABeverageSelectListIsSortedCorrectlyAfterRemovingADrinkFromTheirFavorites()
        {
            SetFavouriteStatusOfBeverageUI("c", second, false); // Select "Great Western Pilsner" and favourite it.

            app.EnterText("searchBeverage", "c"); // Search for the recently favourited beverage.

            AppResult[] favBev = app.Query(beverages[1] + "    \u2b50"); // Query the beverage select page for the "Great Western Pilsner" list option.
            float fBevPos = favBev[0].Rect.CenterY; // Get the Y position of the list option.

            Assert.IsTrue(fBevPos > 655 && fBevPos < 810); // Passes if the Y position of the list option is at the first position of the list.

            SetFavouriteStatusOfBeverageUI("", first, false); // Select "Great Western Pilsner" and unfavourite it.

            app.EnterText("searchBeverage", "c"); // Search for the recently unfavourited beverage.

            favBev = app.Query(beverages[1]); // Query the beverage select page for the "Great Western Pilsner" list option.
            fBevPos = favBev[0].Rect.CenterY; // Get the Y position of the list option.

            Assert.IsTrue(fBevPos > 810 && fBevPos < 965); // Passes if the Y position of the list option is at the second position of the list.
        }

        // Favourite a beverage and test that it is not displayed when searching for something unrelated.
        [Test]
        public void UserDoesNotSeeAFavoritedBeverageInAnUnrelatedSearch()
        {
            SetFavouriteStatusOfBeverageUI("c", second, false); // Select "Great Western Pilsner" and favourite it.

            app.EnterText("searchBeverage", "copper"); // Search for something unrelated to the recently favourited beverage.

            AppResult[] favBev = app.Query(beverages[1]); // Query the beverage select page for the "Great Western Pilsner" list option.
            Assert.IsFalse(favBev.Any()); // Passes if "Great Western Pilsner" is not displayed in the list.
        }

        // Have no favourites and test that the no favourites label is displayed.
        [Test]
        public void UsersWithNoFavoriteDrinksSeesTheStockMessageOnTheBeverageSelectPage()
        {
            AppResult[] noFavLabel = app.Query("Select a beverage and favourite it to view it here!"); // Query the beverage select page for the no favourites message.
            Assert.IsTrue(noFavLabel.Any()); // Passes if the no favourites label is displayed on the page.
        }

        // Have no favourites and test that the no favourites label is displayed, then
        // favourite a beverage and test that the no favourites label is not displayed.
        [Test]
        public void UserWithFavoriteDrinksDoesNotSeeOutStockMessage()
        {
            AppResult[] noFavLabel = app.Query("Select a beverage and favourite it to view it here!"); // Query the beverage select page for the no favourites message.
            Assert.IsTrue(noFavLabel.Any()); // Passes if the no favourites label is displayed on the page.

            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and favourite it.

            noFavLabel = app.Query("Select a beverage and favourite it to view it here!"); // Query the beverage select page for the no favourites message.
            Assert.IsFalse(noFavLabel.Any()); // Passes if the no favourites label is not displayed on the page.
        }

        // Favourite a beverage and test that selecting it from the carousel will navigate to the status page.
        [Test]
        public void UserSelectsAFavoritedDrinkFromTheBeverageSelectPageWithoutUsingTheSearchBar()
        {
            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and favourite it.

            app.TapCoordinates(420, 1090); // Tap the recently favourited beverage in the carousel.

            app.WaitForElement("FavouriteButton"); // Wait for the status page to load.

            AppResult[] bevStatus = app.Query(beverages[0]); // Query the status page for the "Churchill Blonde Lager" beverage name.
            Assert.IsTrue(bevStatus.Any()); // Passes if "Churchill Blonde Lager" was found on its own status page.
        }

        // Favourite a beverage and test that selecting it from the list will navigate to the status page.
        [Test]
        public void UserSelectsaFavouritedDrinkFromTheBeverageSelectPageAfterSearchingForIt()
        {
            SetFavouriteStatusOfBeverageUI("c", fourth, false); // Select "Original 16 Copper Ale" and favourite it.

            app.EnterText("searchBeverage", "c"); // Search for the recently favourited beverage.

            app.TapCoordinates(715, first); // Tap the "Original 16 Copper Ale" list option.

            app.WaitForElement("FavouriteButton"); // Wait for the status page to load.

            AppResult[] bevStatus = app.Query(beverages[3]); // Query the status page for the "Original 16 Copper Ale" beverage name.
            Assert.IsTrue(bevStatus.Any()); // Again, passes if "Original 16 Copper Ale" was found on its own status page.
        }
    }
}