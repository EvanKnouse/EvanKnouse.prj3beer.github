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

        //const int[] iaPositions
        const int first = 750;
        const int second = 900;
        const int third = 1050;
        const int fourth = 1200;
        const int fifth = 1350;

        int iFavCount = svm.Context.Preference.Where(c => c.Favourite == true).Count();

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();


        }

        public void selectABeverage(String searchBeverage, int placement)
        {
            // tap to navigate to the beverage select screen
            // Assumes app.EnterText knows where to enter text
            //app.Tap("Beverage Select");

            app.EnterText("searchBeverage", searchBeverage.ToString());
            app.TapCoordinates(200, placement);
        }

        private void FavouriteADrink(string sFullBevName, string sSearch, int iPosition)
        {
            iFavCount = svm.Context.Preference.Where(c => c.Favourite == true).Count();

            Beverage bev = svm.Context.Beverage.Find(sFullBevName);
            Preference pref = svm.Context.Preference.Find(bev.BeverageID);

            pref.Favourite = true;

            svm.Context.Preference.Update(pref);

            /*if (!pref.Favourite)
            {
                if (iFavCount >= 5)
                {
                    // Change this depending on how the CarouselView displays
                    app.TapCoordinates(200, 775);

                    app.WaitForElement("FavouriteButton");

                    app.TapCoordinates(715, 2130);

                    app.Back();

                    app.WaitForElement("Beverage Select");
                }

                selectABeverage(sSearch, iPosition);

                app.WaitForElement("FavouriteButton");

                app.TapCoordinates(715, 2130);

                app.Back();

                app.WaitForElement("Beverage Select");

                //app.Tap("Beverage Select");

                app.TapCoordinates(1330, 350);

                app.Back();
            }*/
        }


        [Test]
        public void UserSeesAFavoritedBeverageOnTheBeverageSelectPage()
        {
            FavouriteADrink("Churchill Blonde Lager", "chu", first);

            AppResult[] favouritedBeverage = app.Query("Churchill Blonde Lager");

            Assert.IsTrue(favouritedBeverage.Any());

            // Querying the CarouselView may be better in the future
            //AppResult[] Carosol = app.Query("FavouritesCarousel");
        }

        [Test]
        public void UserNoLongerSeesABeverageOnTheBeverageSelectPageAfterItIsRemovedAsAFavorited()
        {
            FavouriteADrink("Churchill Blonde Lager", "chu", first);

            // Change this depending on how the CarouselView displays
            app.TapCoordinates(200, 775);

            app.WaitForElement("FavouriteButton");

            app.TapCoordinates(715, 2130);

            app.Back();

            app.WaitForElement("Beverage Select");

            AppResult[] favouritedBeverage = app.Query("Churchill Blonde Lager");

            Assert.IsFalse(favouritedBeverage.Any());
        }

        [Test]
        public void UserSeesMultipleFavoritedBeveragesOnTheBeverageSelectPage()
        {
            for(int i = iFavCount; i <= 5; i++)
            {

            }

            FavouriteADrink("c", first);
            FavouriteADrink("c", first);
            FavouriteADrink("c", first);
            FavouriteADrink("c", first);
            FavouriteADrink("chu", first);

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