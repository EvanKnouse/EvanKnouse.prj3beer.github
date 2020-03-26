using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class SelectFavouritesTests
    {
        //Instead of querying on any (in case its empty) just make sure it contains the correct number of beverages
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        //Set some drinks to begin with
        List<string> beverages = new List<string> { "Churchill Blonde Lager", "Great Western Pilsner", "Great Western Radler", "Original 16 Copper Ale", "Rebellion Zilla IPA" };

        //These are positions of items in the list of the select beverage page
        //Hard coded just for convinience
        //const int first = 750;
        //const int second = 900;
        //const int third = 1050;
        //const int fourth = 1200;
        //const int fifth = 1350;

        //Pixel2 Pie9 positions
        //const int first = 750;
        //const int second = 900;
        //const int third = 1050;
        //const int fourth = 1200;
        //const int fifth = 1350;

        double first;
        double second;
        double third;
        double fourth;
        double fifth;
        double pageheight = 1918.9;
        double listspace;
        double favoriteButtonY;

        int SearchClearX = 1000;
        int SearchClearY = 270;

        public SelectFavouritesTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();

            app.WaitForElement("searchBeverage"); // Wait for the beverage select page to load.

            //pageheight = DeviceDisplay.MainDisplayInfo.Height;
            listspace = pageheight * 0.0609724321225702;
            first = pageheight * 0.2574391578508521;
            second = first + listspace;
            third = second + listspace;
            fourth = third + listspace;
            fifth = fourth + listspace;
            double space = pageheight * 0.134452029808744;
            favoriteButtonY = (pageheight - space) - 5;//(pageheight * 0.1146490176663714‬);
            
        }

        // Helper method for favouriting/unfavouriting a beverage through the UI.
        private void SetFavouriteStatusOfBeverageUI(string sBevName, double iListPosition, bool bLooseFocus)
        {
            app.EnterText("searchBeverage", sBevName); // Enter the search criteria into the search bar.

            app.TapCoordinates(715, (float)iListPosition+5); // Tap the beverage option in the list.

            app.WaitForElement("FavouriteButton"); // Wait for the status page to load.
            app.Tap("FavouriteButton"); // Tap the favourite button for that beverage.

            app.Back(); // Go back to the beverage select page.

            app.WaitForElement("searchBeverage"); // Wait for the beverage select page to load.
            app.TapCoordinates(SearchClearX, SearchClearY); // Tap the 'X' on the search bar to clear it.

            if (bLooseFocus)
            {
                app.TapCoordinates(715, (float)favoriteButtonY); // Tap below the search bar to remove focus.
            }
        }

        // Favourite a beverage and test that it is displayed in the carousel.
        [Test]
        public void UserSeesAFavoritedBeverageOnTheBeverageSelectPage()
        {
            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and favourite it.

            AppResult[] favBev = app.Query("Churchill Blonde Lager"); // Query the beverage select page for the beverage.
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.
        }

        // Favourite a beverage and test that it is displayed in the carousel, then
        // unfavourite that beverage and test that it is not displayed in the carousel.
        [Test]
        public void UserNoLongerSeesABeverageOnTheBeverageSelectPageAfterItIsRemovedAsAFavorited()
        {
            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and favourite it.

            AppResult[] favBev = app.Query("Churchill Blonde Lager"); // Query the beverage select page for "Churchill Blonde Lager". // ID: 5
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.

            SetFavouriteStatusOfBeverageUI("chu", first, true); // Select "Churchill Blonde Lager" and unfavourite it.

            favBev = app.Query("Churchill Blonde Lager"); // Query the beverage select page for "Churchill Blonde Lager". // ID: 5
            Assert.IsFalse(favBev.Any()); // Passes if the beverage name is not shown on the page.
        }

        // Favourite two beverages and test that they are displayed in the carousel.
        [Test]
        public void UserSeesMultipleFavoritedBeveragesOnTheBeverageSelectPage()
        {
            SetFavouriteStatusOfBeverageUI("c", first, false); // Select "Churchill Blonde Lager" and favourite it.
            SetFavouriteStatusOfBeverageUI("c", second, true); // Select "Great Western Pilsner" and favourite it.

            AppResult[] favBev = app.Query("Churchill Blonde Lager"); // Query the beverage select page for "Churchill Blonde Lager". // ID: 5
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.

            favBev = app.Query("Great Western Pilsner"); // Query the beverage select page for "Great Western Pilsner". // ID: 2
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.
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

            app.TapCoordinates(540, 805);
            app.Back();
            app.WaitForElement("FavouritesCarousel");

            AppResult[] favBev = app.Query("Great Western Radler"); // Query the beverage select page for "Great Western Radler". // ID: 1
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.

            app.SwipeRightToLeft(0.9, 1000, true);
            //app.WaitForElement("FavouritesCarousel");

            favBev = app.Query("Great Western Pilsner"); // Query the beverage select page for "Great Western Pilsner". // ID: 2
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.

            app.SwipeRightToLeft(0.9, 1000, true);

            favBev = app.Query("Original 16 Copper Ale"); // Query the beverage select page for "Original 16 Copper Ale". // ID: 3
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.

            app.SwipeRightToLeft(0.9, 1000, true);

            favBev = app.Query("Churchill Blonde Lager"); // Query the beverage select page for "Churchill Blonde Lager". // ID: 5
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.

            app.SwipeRightToLeft(0.9, 1000, true);

            favBev = app.Query("Rebellion Pear Beer"); // Query the beverage select page for "Rebellion Pear Beer". // ID: 99
            Assert.IsTrue(favBev.Any()); // Passes if the beverage name is shown on the page.
        }

        // Favourite a beverage and test that it is displayed at the first position of the list with a symbol.
        [Test]
        public void UserSeesTheirFavoriteDrinksAppearWithASpecialSymbolAndAboveOtherDrinksInTheBeverageSelectPage()
        {
            SetFavouriteStatusOfBeverageUI("c", second, false); // Select "Great Western Pilsner" and favourite it.

            app.EnterText("searchBeverage", "c"); // Search for the recently favourited beverage.

            AppResult[] favBev = app.Query(beverages[1] + "    \u2b50"); // Query the beverage select page for the "Great Western Pilsner" list option.
            float fBevPos = favBev[0].Rect.CenterY; // Get the Y position of the list option.

            Assert.IsTrue(fBevPos > first && fBevPos < second); // Passes if the Y position of the list option is at the first position of the list.
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

            Assert.IsTrue(fBevPos > first && fBevPos < second); // Passes if the Y position of the list option is at the first position of the list.

            SetFavouriteStatusOfBeverageUI("", first, false); // Select "Great Western Pilsner" and unfavourite it.

            app.EnterText("searchBeverage", "c"); // Search for the recently unfavourited beverage.

            favBev = app.Query(beverages[1]); // Query the beverage select page for the "Great Western Pilsner" list option.
            fBevPos = favBev[0].Rect.CenterY; // Get the Y position of the list option.

            Assert.IsTrue(fBevPos > second && fBevPos < third); // Passes if the Y position of the list option is at the second position of the list.
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

            app.TapCoordinates(540, 805); // Tap the recently favourited beverage in the carousel.

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

            app.TapCoordinates(715, (float)first+5); // Tap the "Original 16 Copper Ale" list option.

            app.WaitForElement("FavouriteButton"); // Wait for the status page to load.

            AppResult[] bevStatus = app.Query(beverages[3]); // Query the status page for the "Original 16 Copper Ale" beverage name.
            Assert.IsTrue(bevStatus.Any()); // Again, passes if "Original 16 Copper Ale" was found on its own status page.
        }
    }
}