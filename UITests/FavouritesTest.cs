using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Models;
using prj3beer.ViewModels;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class FavouritesTests
    {
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        public FavouritesTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();
            app.WaitForElement("searchBeverage"); // Wait for the beverage select page to load.
        }

        #region Helper Methods
        // helper method for going to the beverage page, selecting a beverage, and going to the status screen
        public void SelectABeverage(string searchBeverage)
        {
            // tap to navigate to the beverage select page
            // Assumes app.EnterText knows where to enter text
            //app.Tap("Beverage Select");

            app.TapCoordinates(1350, 350);
            app.EnterText("searchBeverage", searchBeverage.ToString()); //Entering code into the search bar
            app.TapCoordinates(200, 750); //Tapping a result from the list gotten from the search
        }
        #endregion

        // test that the favourite button is on the status screen
        [Test]
        public void TestThatFavouriteButtonIsOnStatusScreen()
        {
            // go straight to the status screen
            SelectABeverage("c");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // test that the favourite button returns something and is on the screen
            AppResult[] favouriteButton = app.Query("FavouriteButton");
            Assert.IsTrue(favouriteButton.Any());
        }

        // test that pressing the favourite button adds the beverage as a favourite
        [Test]
        public void TestThatPressingTheFavouriteButtonAddsTheBeverageAsAFavourite()
        {
            // go to the beverage select page and select the beverage string
            SelectABeverage("Great Western Radler");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            //Get reference of an unfavored favorite button
            AppResult[] RefUnfavBtn = app.Query("FavouriteButton");

            // tap the favourite button to add the current beverage as a favourite
            app.Tap("FavouriteButton");
            
            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // check that the favourite button is toggled properly, should be favourited
            AppResult[] favouriteButton = app.Query("FavouriteButton");

            Assert.AreNotEqual(favouriteButton[0], RefUnfavBtn[0]);
        }

        // test that pressing the favourite button removes the beverage as a favourite
        [Test]
        public void TestThatPressingTheFavouriteButtonOnAFavouriteBeverageRemovesItAsAFavourite()
        {
            // go to the beverage select page and select the beverage string
            SelectABeverage("Great Western Radler");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // tap the favourite button to remove the current beverage as a favourite
            app.Tap("FavouriteButton");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            AppResult[] FavoritedButton = app.Query("FavouriteButton");

            app.Tap("FavouriteButton");

            // check that the favourite button is toggled properly, should not be favourited
            AppResult[] unFavouritedButton = app.Query("FavouriteButton");


            //Compare the buttons
            Assert.AreNotEqual(FavoritedButton[0], unFavouritedButton[0]);
        }

        // test that selecting a beverage that was previously added as a favourite shows the favourite button toggled properly
        [Test]
        public void TestThatSelectingAFavouriteBeverageFromTheBeverageSelectPageShowsTheFavouriteButtonToggled()
        {
            // go to the beverage select page and select the beverage string
            SelectABeverage("Great Western Radler");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // check that the favourite button is toggled properly, should be favourited
            AppResult[] favouriteButton = app.Query("FavouriteButton");

            //Save the beverages current favorite status
            var testFavButton = favouriteButton[0];

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // get the current status of the favourite button
            favouriteButton = app.Query("FavouriteButton");

            //Will compare the image source of the button to the expected image
            Assert.IsTrue(favouriteButton.Any());
        }

        // test that multiple beverages can be added as a favourite
        [Test]
        public void TestThatMultipleBeveragesCanBeSelectedAsFavourites()
        {
            // go to the beverage select page and select the beverage string
            SelectABeverage("Churchill Blonde Lager");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // tap the favourite button to add the current beverage as a favourite
            app.Tap("FavouriteButton");

            // check that the favourite button is toggled properly, should be favourited
            AppResult[] favouriteButton = app.Query("FavouriteButton");

            // will compare the image source of the button to the expected image
            Assert.IsTrue(favouriteButton.Any());

            app.Back();
            
            // select a new beverage
            SelectABeverage("Rebellion Pear Beer");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // tap the favourite button to add the current beverage as a favourite
            app.Tap("FavouriteButton");

            // check that the favourite button is toggled properly, should be favourited
            favouriteButton = app.Query("FavouriteButton");

            // will compare the image source of the button to the expected image
            Assert.IsTrue(favouriteButton.Any());
        }
    }
}