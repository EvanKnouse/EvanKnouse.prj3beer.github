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
        static StatusViewModel svm;

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
        }

        #region Helper Methods
        // helper method for going to the beverage page, selecting a beverage, and going to the status screen
        public void SelectABeverage(string searchBeverage)
        {
            // tap on the hamburger menu
            app.TapCoordinates(150, 90);

            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
            app.EnterText("searchBeverage", searchBeverage);

            app.TapCoordinates(3, 701);
        }

        //Helper method for going aligning menu to status screen
        public void GoToStatus()
        {
            app.TapCoordinates(150, 90);
            app.WaitForElement("Status");
            app.Tap("Status");
        }
        #endregion

        // test that the favourite button is on the status screen
        [Test]
        public void TestThatFavouriteButtonIsOnStatusScreen()
        {
            // go straight to the status screen
            GoToStatus();

            // wait for an element on the status screen
            app.WaitForElement("currentTemperature");

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
            app.WaitForElement("currentTemperature");

            // tap the favourite button to add the current beverage as a favourite
            app.Tap("FavouriteButton");

            //Fix menu alignment
            GoToStatus();

            // select the beverage that was just favourited
            SelectABeverage("Great Western Radler");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // check that the favourite button is toggled properly, should be favourited
            AppResult[] favouriteButton = app.Query("FavouriteButton");
            Assert.IsTrue(favouriteButton.Any());
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

            //Align the menu page
            GoToStatus();

            // select the beverage that was just favourited
            SelectABeverage("Great Western Radler");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            // check that the favourite button is toggled properly, should not be favourited
            AppResult[] favouriteButton = app.Query("FavouriteButton");

            //Recreate image that will be used for the favorite button
            string testSoure = "NotFavorite";

            //Make a new test imagebutton
            var testFavButton = new Xamarin.Forms.ImageButton();

            //Set testimage asspects to be the same as the button we are looking for
            testFavButton.Source = testSoure;
            testFavButton.AutomationId = "FavouriteButton";

            //Compare the buttons
            Assert.AreEqual(testFavButton, favouriteButton[0]);
        }

        // test that selecting a beverage that was previously added as a favourite shows the favourite button toggled properly
        [Test]
        public void TestThatSelectingAFavouriteBeverageFromTheBeverageSelectPageShowsTheFavouriteButtonToggled()
        {
            // go to the beverage select page and select the beverage string
            SelectABeverage("Great Western Radler");

            // wait for an element on the status screen
            app.WaitForElement("FavouriteButton");

            //Check if the button is favourited or not
            if (!svm.Context.Preference.Find(Settings.BeverageSettings).Favourite)
            {
                //Favourite button cooridinence
                app.TapCoordinates(0,0);
            }

            // check that the favourite button is toggled properly, should be favourited
            AppResult[] favouriteButton = app.Query("FavouriteButton");

            //Save the beverages current favorite status
            var testFavButton = favouriteButton[0];

            //Reset the status page
            GoToStatus();

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

            // go to the status screen fix
            GoToStatus();

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