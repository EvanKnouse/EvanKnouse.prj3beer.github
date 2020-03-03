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

        public void SelectABeverage(String searchBeverage, int placement)
        {
            // tap to navigate to the beverage select screen
            // Assumes app.EnterText knows where to enter text
            //app.Tap("Beverage Select");

            app.EnterText("searchBeverage", searchBeverage.ToString());
            app.TapCoordinates(200, placement);
        }


        private void FavouriteADrink(string sFullBevName)
        {
            iFavCount = svm.Context.Preference.Where(c => c.Favourite == true).Count();

            Beverage bev = svm.Context.Beverage.Find(sFullBevName);
            Preference pref = svm.Context.Preference.Find(bev.BeverageID);


            if(iFavCount >= 5 && !pref.Favourite)
            {
                RemoveFavourite(svm.Context.Preference.Where(c => c.Favourite == true).First().BeverageID);
            }


            pref.Favourite = true;
            svm.Context.Preference.Update(pref);

            /*
            if (!pref.Favourite)
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
            }
            */
        }

        public void RemoveFavourite(string sFullBevName)
        {
            IQueryable<Preference> favorites = svm.Context.Preference.Where(c => c.Favourite == true);

            int bTemp = svm.Context.Beverage.Find(sFullBevName).BeverageID;

            for (int i=0; i< favorites.Count(); i++)
            {
                Preference pTemp = favorites.ElementAt(i);
                if (pTemp.BeverageID == bTemp)
                {
                    pTemp.Favourite = false;
                    svm.Context.Preference.Update(pTemp);
                }
            }
        }

        public void RemoveFavourite(int bevID)
        {
            IQueryable<Preference> favorites = svm.Context.Preference.Where(c => c.Favourite == true);

            for (int i = 0; i < favorites.Count(); i++)
            {
                Preference pTemp = favorites.ElementAt(i);
                if (pTemp.BeverageID == bevID)
                {
                    pTemp.Favourite = false;
                    svm.Context.Preference.Update(pTemp);
                }
            }
        }


        public void RemoveAllFavourites()
        {
            IQueryable<Preference> favorites = svm.Context.Preference.Where(c => c.Favourite == true);
            for (int i = 0; i < favorites.Count(); i++)
            {
                Preference pTemp = favorites.ElementAt(i);
                pTemp.Favourite = false;
                svm.Context.Preference.Update(pTemp);
                
            }
        }

        [Test]
        public void UserSeesAFavoritedBeverageOnTheBeverageSelectPage()
        {
            FavouriteADrink("Churchill Blonde Lager");

            app.WaitForElement("Churchill Blonde Lager");

            AppResult[] favouritedBeverage = app.Query("Churchill Blonde Lager");

            Assert.IsTrue(favouritedBeverage.Any());

            // Querying the CarouselView may be better in the future
            //AppResult[] Carosol = app.Query("FavouritesCarousel");
        }

        [Test]
        public void UserNoLongerSeesABeverageOnTheBeverageSelectPageAfterItIsRemovedAsAFavorited()
        {
            FavouriteADrink("Churchill Blonde Lager");

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
           
            FavouriteADrink("Churchill Blonde Lager");
            FavouriteADrink("Great Western Pilsner");
            FavouriteADrink("Rebellion Pear Beer");


            IQueryable<Preference> favorites = svm.Context.Preference.Where(c => c.Favourite == true);
            RemoveFavourite(favorites.First().BeverageID);

            favorites = svm.Context.Preference.Where(c => c.Favourite == true);

            for (int i=0; i<favorites.Count(); i++)
            {
                Beverage temp = svm.Context.Beverage.Find(favorites.ElementAt(i).BeverageID);

                AppResult[] favouritedBeverage = app.Query(temp.Name);

                Console.WriteLine(temp.Name + "Exists");
                Assert.IsTrue(favouritedBeverage.Any());
            }

        }

        [Test]
        public void UserSeesAllFiveOfTheirFavoritedBeveragesOnTheBeverageSelectPage()
        {
            RemoveAllFavourites();

            FavouriteADrink(beverages[0]);
            FavouriteADrink(beverages[1]);
            FavouriteADrink(beverages[2]);
            FavouriteADrink(beverages[3]);
            FavouriteADrink(beverages[4]);

            for (int i = 0; i <5; i++)
            {
                AppResult[] favouritedBeverage = app.Query(beverages[i]);

                Console.WriteLine(beverages[i] + "Exists");
                Assert.IsTrue(favouritedBeverage.Any());
            }
        }

        [Test]
        public void UserSeesTheirFavoriteDrinksAppearWithASpecialSymbolAndAboveOtherDrinksInTheBeverageSelectPage()
        {
            RemoveAllFavourites();

            FavouriteADrink(beverages[1]);

            app.EnterText("searchBeverage", "c");

            AppResult[] favBev = app.Query(beverages[1]);
            float fBevPos = favBev[0].Rect.CenterY;

            AppResult[] favSymbol = app.Query("FavouriteSymbol");
            float fSymbolPos = favSymbol[0].Rect.CenterY;

            Assert.AreEqual(fBevPos, fSymbolPos);
            // Greater than 750 and less than 900
            Assert.IsTrue(fBevPos > 2625f && fBevPos < 3150f);
        }

        [Test]
        public void UserSeesABeverageSelectListIsSortedCorrectlyAfterRemovingADrinkFromTheirFavorites()
        {
            /*RemoveAllFavourites();
            FavouriteADrink(beverages[1]);
            app.EnterText("searchBeverage", "c");*/

            UserSeesTheirFavoriteDrinksAppearWithASpecialSymbolAndAboveOtherDrinksInTheBeverageSelectPage();

            RemoveFavourite(beverages[1]);

            app.EnterText("searchBeverage", "c");

            AppResult[] favBev = app.Query(beverages[1]);
            float fBevPos = favBev[0].Rect.CenterY;

            Assert.IsTrue(fBevPos > 3150f && fBevPos < 3675f);
        }

        [Test]
        public void UserDoesNotSeeAFavoritedBeverageInAnUnrelatedSearch()
        {
            FavouriteADrink(beverages[1]);

            app.EnterText("searchBeverage", "copper");

            AppResult[] bev = app.Query(beverages[1]);
            Assert.IsFalse(bev.Any());
        }

        [Test]
        public void UsersWithNoFavoriteDrinksSeesTheStockMessageOnTheBeverageSelectPage()
        {
            RemoveAllFavourites();

            AppResult[] stockMessage = app.Query("NoFavouritesLabel");
            // Might have to test actual text
            Assert.IsTrue(stockMessage.Any());
        }

        [Test]
        public void UserWithFavoriteDrinksDoesNotSeeOutStockMessage()
        {
            FavouriteADrink(beverages[0]);

            AppResult[] stockMessage = app.Query("NoFavouritesLabel");
            // Might have to test actual text
            Assert.IsFalse(stockMessage.Any());
        }

        [Test]
        public void UserSelectsAFavoritedDrinkFromTheBeverageSelectPageWithoutUsingTheSearchBar()
        {
            RemoveAllFavourites();

            FavouriteADrink(beverages[0]);

            app.TapCoordinates(200, 775);

            app.WaitForElement("FavouriteButton");

            AppResult[] bevStatus = app.Query(beverages[0]);

            Assert.IsTrue(bevStatus.Any());
        }

        [Test]
        public void UserSelectsaFavouritedDrinkFromTheBeverageSelectPageAfterSearchingForIt()
        {
            RemoveAllFavourites();

            FavouriteADrink(beverages[3]);

            SelectABeverage("c", first);

            // Don't process and drink

            app.WaitForElement("FavouriteButton");

            AppResult[] bevStatus = app.Query(beverages[3]);

            Assert.IsTrue(bevStatus.Any());
        }
    }
}