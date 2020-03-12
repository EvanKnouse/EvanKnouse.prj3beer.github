using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer;
using prj3beer.Models;
using prj3beer.ViewModels;

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

        //These are positions of items in the list of the select beverage screen
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

            //svm = new StatusViewModel();
        }

        /// <summary>
        /// Selects a drink form the beverage select page
        /// </summary>
        /// <param name="searchBeverage"> What is inputed into the search bar </param>
        /// <param name="placement"> The position of the beverage in the list to tap </param>
        public void SelectABeverage(string searchBeverage, int placement)
        {
            // tap to navigate to the beverage select screen
            // Assumes app.EnterText knows where to enter text
            //app.Tap("Beverage Select");

            app.EnterText("searchBeverage", searchBeverage.ToString()); //Entering code into the search bar
            app.TapCoordinates(200, placement); //Tapping a result from the list gotten from the search
        }

        /// <summary>
        /// Sets a beverage preference to be favorited
        /// Will remove a favorite at random if the limit has already been reached
        /// </summary>
        /// <param name="sFullBevName"> The breverage to favorite </param>
        private void FavouriteADrink(string sFullBevName)
        {
            int iFavCount = App.Context.Preference.Where(c => c.Favourite == true).Count();//Gets a count of how many beverages are already set as favorites

            Beverage bev = App.Context.Beverage.Find(sFullBevName);//Gets the beverages beased on what's passed in
            Preference pref = App.Context.Preference.Find(bev.BeverageID);//Get the preference of the beverage inputed

            if(iFavCount >= 5 && !pref.Favourite) //Ensure there are not more favorites then the limit. If trying to favorite an already favorited drink, doesn't add to the count so don't remove any
            {
                RemoveFavourite(App.Context.Preference.Where(c => c.Favourite == true).First().BeverageID); //Removes a favorite at random
            }


            pref.Favourite = true; //Set the current to be a favorite
            App.Context.Preference.Update(pref); //Save the preference now that it is favorited

            #region shrinking comments
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
            #endregion
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

            //for (int i=0; i< favorites.Count(); i++)
            //{
            //    Preference pTemp = favorites.ElementAt(i);
            //    if (pTemp.BeverageID == bTemp)
            //    {
            //        pTemp.Favourite = false;
            //        svm.Context.Preference.Update(pTemp);
            //    }
            //}

        }


        /// <summary>
        /// Alternative method of removing a favorite
        /// More code friendly, but less user friendly
        /// </summary>
        /// <param name="bevID"> ID of the beverage preference to unfavorite </param>
        public void RemoveFavourite(int bevID)
        {
            ///IQueryable<Preference> favorites = svm.Context.Preference.Where(c => c.Favourite == true);

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

        [Test]
        public void UserSeesAFavoritedBeverageOnTheBeverageSelectPage()
        {
            FavouriteADrink("Churchill Blonde Lager");

            app.WaitForElement("Churchill Blonde Lager");//Give a chance for the carousel to be updated

            AppResult[] favouritedBeverage = app.Query("Churchill Blonde Lager"); //Query to see if the drink is now displayed

            Assert.IsTrue(favouritedBeverage.Any()); //Passes if the favorited beverage is returned

            // Querying the CarouselView may be better in the future
            //AppResult[] Carosol = app.Query("FavouritesCarousel");
        }

        [Test]
        public void UserNoLongerSeesABeverageOnTheBeverageSelectPageAfterItIsRemovedAsAFavorited()
        {
            RemoveAllFavourites();

            FavouriteADrink("Churchill Blonde Lager");

            // Change this depending on how the CarouselView displays
            app.TapCoordinates(200, 775); //Taps the carousel to access the beverage status page

            app.WaitForElement("FavouriteButton"); //Give the page a chance to load what we need

            app.TapCoordinates(715, 2130); //Tap the favorite button to unfavorite 

            app.Back(); //Go back to the beverage select page

            app.WaitForElement("Beverage Select"); //Wait for the page to load

            AppResult[] favouritedBeverage = app.Query("Churchill Blonde Lager"); //Query to see if the drink is displayed

            Assert.IsFalse(favouritedBeverage.Any()); //Passes if the drink is now not seen
        }

        [Test]
        public void UserSeesMultipleFavoritedBeveragesOnTheBeverageSelectPage()
        {
           
            //Favorite some drinks
            FavouriteADrink("Churchill Blonde Lager");
            FavouriteADrink("Great Western Pilsner");
            FavouriteADrink("Rebellion Pear Beer");


            IQueryable<Preference> favorites = App.Context.Preference.Where(c => c.Favourite == true); //Get all the favorited drinks

            RemoveFavourite(favorites.First().BeverageID); //Remove a favorite to prevent an edge case

            favorites = App.Context.Preference.Where(c => c.Favourite == true); //Update the favorited beverage list now that one has been removed

            for (int i=0; i<favorites.Count(); i++) //For every favorite
            {
                Beverage temp = App.Context.Beverage.Find(favorites.ElementAt(i).BeverageID); //Single out a single beverage from it's prefence

                AppResult[] favouritedBeverage = app.Query(temp.Name); //Queryt for that singled out beverage

                Console.WriteLine(temp.Name + "Exists"); //Leave a line so it can see what beverages were seen

                Assert.IsTrue(favouritedBeverage.Any()); //Passes if the favorited beverage is returned
            }

        }

        [Test]
        public void UserSeesAllFiveOfTheirFavoritedBeveragesOnTheBeverageSelectPage()
        {
            //Clear then fill the favorite list
            RemoveAllFavourites();

            FavouriteADrink(beverages[0]);
            FavouriteADrink(beverages[1]);
            FavouriteADrink(beverages[2]);
            FavouriteADrink(beverages[3]);
            FavouriteADrink(beverages[4]);

            for (int i = 0; i <5; i++) //For the full favorite list
            {
                AppResult[] favouritedBeverage = app.Query(beverages[i]);//Query the i'th favorite

                Console.WriteLine(beverages[i] + "Exists"); //Leave a note to see what name was gotten

                Assert.IsTrue(favouritedBeverage.Any()); //Passes if the favorited beverage is returned
            }
        }

        [Test]
        public void UserSeesTheirFavoriteDrinksAppearWithASpecialSymbolAndAboveOtherDrinksInTheBeverageSelectPage()
        {
            RemoveAllFavourites();

            FavouriteADrink(beverages[1]); //Favorite the great western pillsner

            app.EnterText("searchBeverage", "c"); //Search for it

            AppResult[] favBev = app.Query(beverages[1]); //Gets the app result of the queried beverage name
            float fBevPos = favBev[0].Rect.CenterY; //Gets the position of the element

            AppResult[] favSymbol = app.Query("FavouriteSymbol"); //Gets the appResult of our special symbol
            float fSymbolPos = favSymbol[0].Rect.CenterY; //Gets the position of the special symbol

            Assert.AreEqual(fBevPos, fSymbolPos); //Ensure the beverage and it's favorite symbol are aligned
            // Greater than 750 and less than 900
            Assert.IsTrue(fBevPos > 2625f && fBevPos < 3150f); //Ensure they are on the top
        }

        [Test]
        public void UserSeesABeverageSelectListIsSortedCorrectlyAfterRemovingADrinkFromTheirFavorites()
        {
            //RemoveAllFavourites();
            //FavouriteADrink(beverages[1]);
            //app.EnterText("searchBeverage", "c");

            UserSeesTheirFavoriteDrinksAppearWithASpecialSymbolAndAboveOtherDrinksInTheBeverageSelectPage(); //Makes sure a beverage is favorited right to begin with

            app.TapCoordinates(1330, 350); //Clear the search bar

            RemoveFavourite(beverages[1]); //Remove the beverage as a favorite

            app.EnterText("searchBeverage", "c"); //Search for the beverage

            AppResult[] favBev = app.Query(beverages[1]); //Gets the appResult of the queried beverage

            float fBevPos = favBev[0].Rect.CenterY; //Get the postion of the app result

            Assert.IsTrue(fBevPos > 3150f && fBevPos < 3675f); // Ensure the beverage is in the second position where it normally defaults to
        }

        [Test]
        public void UserDoesNotSeeAFavoritedBeverageInAnUnrelatedSearch()
        {
            FavouriteADrink(beverages[1]); // Favorite a beverage

            app.EnterText("searchBeverage", "copper"); //Search for something unrelated

            AppResult[] bev = app.Query(beverages[1]); //Query the beverage

            Assert.IsFalse(bev.Any()); //Ensure the beverage is not seen
        }

        [Test]
        public void UsersWithNoFavoriteDrinksSeesTheStockMessageOnTheBeverageSelectPage()
        {
            RemoveAllFavourites();

            AppResult[] stockMessage = app.Query("NoFavouritesLabel"); //Query for the stock message
            
            // Might have to test actual text
            Assert.IsTrue(stockMessage.Any()); //Passes if the message is seen
        }

        [Test]
        public void UserWithFavoriteDrinksDoesNotSeeOutStockMessage()
        {
            FavouriteADrink(beverages[0]); //Set a favorite

            AppResult[] stockMessage = app.Query("NoFavouritesLabel"); //Query the stock message
            // Might have to test actual text
            Assert.IsFalse(stockMessage.Any()); //Passes if the message is not seen
        }

        [Test]
        public void UserSelectsAFavoritedDrinkFromTheBeverageSelectPageWithoutUsingTheSearchBar()
        {
            RemoveAllFavourites(); //Clear the favorites (and therefore the carousel)

            FavouriteADrink(beverages[0]); //Favorite a drink

            app.TapCoordinates(200, 775); //Tap the drink on the carosel

            app.WaitForElement("FavouriteButton"); //Give the status page a chance to load

            AppResult[] bevStatus = app.Query(beverages[0]); //Query that the beverage name is found somewhere

            Assert.IsTrue(bevStatus.Any()); //Passes if the beverage was found on it's own status page
        }

        [Test]
        public void UserSelectsaFavouritedDrinkFromTheBeverageSelectPageAfterSearchingForIt()
        {
            RemoveAllFavourites();

            FavouriteADrink(beverages[3]); //Favorite copper ale

            SelectABeverage("c", first); //Select it, it should be on the top of the list after being favorited

            // Don't process and drink

            app.WaitForElement("FavouriteButton"); //Give the page a chance to load

            AppResult[] bevStatus = app.Query(beverages[3]); //Query that the beverage name is found somewhere

            Assert.IsTrue(bevStatus.Any()); //Again: passes if the beverage was found on it's own status page
        }
}
}