using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Views;
using Xamarin.Forms;
using prj3beer.Models;
using prj3beer.ViewModels;
using Android.Graphics;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class SelectFavouritesTests
    {
        //Instead of querying on any (in case its empty) just make sure it contains the correct number of beverages
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        static StatusViewModel svm;

        List<string> beverages = new List<string> { "Churchill Blonde Lager", "Great Western Pilsner", "Great Western Radler", "Original 16 Copper Ale", "Rebellion Zilla IPA" };

        const int first = 750;
        const int second = 900;
        const int third = 1050;
        const int fourth = 1200;
        const int fifth = 1350;



        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();


        }

        public void selectABeverage(String searchBeverage, int placement)
        {
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");

            app.EnterText("searchBeverage", searchBeverage.ToString());
            app.TapCoordinates(200, placement);
        }


        [Test]
        public void UserSeesAFavoritedBeverageOnTheBeverageSelectPage()
        {
            selectABeverage("chu", first);

            app.WaitForElement("FavouriteButton");

            AppResult[] favouriteButton = app.Query("FavouriteButton");

            Preference pref = svm.Context.Preference.Find(Settings.BeverageSettings);

            if (!pref.Favourite)
            {
                app.TapCoordinates(715, 2130);
            }

            app.Back();

            app.WaitForElement("Beverage Select");

            app.Tap("Beverage Select");

            app.TapCoordinates(1330, 350);

            app.Back();

            AppResult[] Carosol = app.Query("FavouritesCarousel");

            Carosol[0].

        }

        [Test]
        public void UserNoLongerSeesABeverageOnTheBeverageSelectPageAfterItIsRemovedAsAFavorited()
        {
        }

        [Test]
        public void UserSeesMultipleFavoritedBeveragesOnTheBeverageSelectPage()
        {
        }

        [Test]
        public void UserSeesAllFiveOfTheirFavoritedBeveragesOnTheBeverageSelectPage()
        {
        }

        [Test]
        public void UserSeesTheirFavoriteDrinksAppearWithASpecialSymbolAndAboveOtherDrinksInTheBeverageSelectPage()
        {
        }

        [Test]
        public void UserSeesABeverageSelectListIsSortedCorrectlyAfterRemovingADrinkFromTheirFavorites()
        {
        }

        [Test]
        public void UserDoesNotSeeAFavoritedBeverageInAnUnrelatedSearch()
        {
        }

        [Test]
        public void UsersWithNoFavoriteDrinksSeesTheStockMessageOnTheBeverageSelectPage()
        {
        }

        [Test]
        public void UserWithFavoriteDrinksDoesNotSeeOutStockMessage()
        {
        }

        [Test]
        public void UserSelectsAFavoritedDrinkFromTheBeverageSelectPageWithoutUsingTheSearchBar()
        {
        }

        [Test]
        public void UserSelectsaFavouritedDrinkFromTheBeverageSelectPageAfterSearchingForIt()
        {
        }

    }
}