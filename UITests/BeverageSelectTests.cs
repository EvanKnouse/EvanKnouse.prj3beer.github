using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Views;
using Xamarin.Forms;
using prj3beer.Models;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class BeverageSelectTests
    {
        //Instead of querying on any (in case its empty) just make sure it contains the correct number of beverages
        string apkPath = "D:\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";
        // D:\prj3beer\prj3.beer\prj3beer\prj3beer.Android\bin\Debug

        IApp app;
        Platform platform;

        #region Test Beverages
        Beverage bevGreatWesternRadler = new Beverage { BeverageID = 1, Name = "Great Western Radler", Brand = new Brand() { brandID = 4, brandName = "Great Western Brewery" }, Type = prj3beer.Models.Type.Radler, Temperature = 2 };
        Beverage bevChurchLager = new Beverage { BeverageID = 2, Name = "Churchill Blonde Lager", Brand = new Brand() { brandID = 4, brandName = "Great Western Brewery" }, Type = prj3beer.Models.Type.Lager, Temperature = 3 };
        Beverage bevBatch88 = new Beverage { BeverageID = 3, Name = "Batch 88", Brand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" }, Type = prj3beer.Models.Type.Stout, Temperature = 4 };
        Beverage bevCoorsLight = new Beverage { BeverageID = 4, Name = "Coors Light", Brand = new Brand() { brandID = 25, brandName = "Coors" }, Type = prj3beer.Models.Type.Light, Temperature = 3 };
        Beverage bevCoorsBanquet = new Beverage { BeverageID = 5, Name = "Coors Banquet", Brand = new Brand() { brandID = 25, brandName = "Coors" }, Type = prj3beer.Models.Type.Pale, Temperature = 2 };
        Beverage bevCoorsEdge = new Beverage { BeverageID = 6, Name = "Coors Edge", Brand = new Brand() { brandID = 25, brandName = "Coors" }, Type = prj3beer.Models.Type.Radler, Temperature = 5 };
        #endregion

        public BeverageSelectTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();
            app.TapCoordinates(150, 90);
            app.Tap("Beverage Select");
            //app.Tap("SearchBeverage");
        }

        #region List View Tests
        [Test]
        public void TestThatBeverageListViewContainsProperBeverages()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            //AppResult[] beverageList = app.Query("beverageListView");

            AppResult[] beverageList = app.Query(bevCoorsEdge.Name);
            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatBeverageListIsSortedAlphabetically()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            var ascending = beverageList.OrderBy(a => a.Text);
            Assert.IsFalse(beverageList.SequenceEqual(ascending));
        }

        [Test]
        public void TestThatBeverageListViewIsOnSelectBeverageScreen()
        {
            app.WaitForElement("beverageListView");

            AppResult[] beverageList = app.Query("beverageListView");

            beverageList.

            Assert.IsTrue(beverageList.Any());
        }
        #endregion

        #region Search Bar Tests
        [Test]
        public void TestThatValidSearchCharacterCorrectlyDisplaysMatchingBeverages()
        {
            String userInput = "a";

            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatValidSearchBrandNameDisplaysMatchingBeverages()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString());
            
          //  AppResult[] beverageList = app.Query(CLBeverage.Name);
           // Assert.IsTrue(beverageList.Any());

            //This will be used for multiple beverages with the name coors in it
            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());

            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatInvalidSearchBrandNameDoesNotDisplayBeverages()
        {
            String userInput = "Coorss";

            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatValidSearchBeverageNameDisplaysMatchingBeverages()
        {
            String userInput = "Coors Light";

            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
          //  AppResult[] beverageList = app.Query(CLBeverage.Name);
           // Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatInvalidSearchBeverageNameDoesNotDisplayBeverages()
        {
            String userInput = "Maple Brew";

            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatValidSearchTypeDisplaysMatchingBeverages()
        {
            String userInput = "ale";

            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the ales which is not implemented yet.

            //    AppResult[] beverageList = app.Query(aleBeverage1);
            //    Assert.IsTrue(beverageList.Any());

            //    AppResult[] beverageList = app.Query(aleBeverage1);
            //    Assert.IsTrue(beverageList.Any());

            //    AppResult[] beverageList = app.Query(aleBeverage1);
            //    Assert.IsTrue(beverageList.Any());

            //    AppResult[] beverageList = app.Query(aleBeverage1);
            //    Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatInvalidSearchTypeDoesNotDisplayBeverages()
        {
            String userInput = "Unleaded";
            app.EnterText("SearchBeverage", userInput.ToString().ToLower());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Count() > 0);
        }

        [Test]
        public void TestThatTypingInCoorsLiteDisplaysTwoResults()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.AreEqual(1, beverageList.Count());
        }

        [Test]
        public void TestThatSearchBoxIsOnSelectBeverageScreen()
        {
            app.WaitForElement("searchBeverage");

            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatSearchBoxCanBeTypedInto()
        {
            String userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatBackSpacingACharacterBroadensResultSearch()
        {
            String userInput = "Coors L";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Count() == 1);

            userInput = "Coors ";
            app.EnterText("searchBeverage", userInput.ToString());
            Assert.IsTrue(beverageList.Count() == 4);

        }

        [Test]
        public void TestThatBackSpacingAWordBroadensResultSearch()
        {
            String userInput = "Coors Lite";
            app.EnterText("searchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Count() == 1);

            userInput = "Coors ";
            app.EnterText("searchBeverage", userInput.ToString());
            
            Assert.IsTrue(beverageList.Count() == 4);
        }

        [Test]
        public void TestThatBackspacingEntireSearchStringDoesNotStartSearchProcess()
        {
            String userInput = "Coors Lite";
            app.EnterText("searchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Count() == 1);

            userInput = "";
            app.EnterText("searchBeverage", userInput.ToString());
            
            Assert.IsTrue(beverageList.Count() == 0);
        }

        [Test]
        public void TestThatDeviceWaitsACertainAmountOfTimeBeforeQueryingLocalStorage()
        {
            String userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());
            AppResult[] beverageList = app.Query("beverageListView");
            Assert.IsFalse(beverageList.Count() >= 1);
            
            beverageList = app.Query("beverageListView");
            Assert.IsTrue(beverageList.Count() >= 1);
        }

        #region Space Character Tests
        [Test]
        public void TestThatJustSpacesSearchDoesNotDisplayBeverages()
        {
            String userInput = "       ";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }


        [Test]
        public void TestThatLeadingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "   Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }


        [Test]
        public void TestThatTrailingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "Coors Lite   ";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatSpacesInTheMiddleOfTheSearchStringAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "Coors     Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }
        #endregion

        #region Invalid Character Tests
        [Test]
        public void TestThatEmptySearchStringDoesNotDisplayBeverages()
        {
            AppResult[] beverageList = app.Query("beverageListView");
            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatSpecialCharactersStringDoesNotDisplayBeverages()
        {
            String userInput = "$$@!:)";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatNumberStringDoesNotDisplayBeverages()
        {
            String userInput = "853971";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }
        #endregion
        #endregion

        #region Error Label Tests
        [Test]
        public void TestThatErrorMessageIsDisplayedWhenNoResultsAreFound()
        {
            String userInput = "Meepo";
            app.EnterText("searchBeverage", userInput.ToString());
            

            AppResult[] warningLabel = app.Query(c => c.All("errorLabel").Property("Hidden", false)); // app.WaitForElement
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.Any());
        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenResultsAreFound()
        {
            String userInput = "Coors";
            app.EnterText("SearchBeverage", userInput.ToString());
            

            AppResult[] warningLabel = app.Query(c => c.All("errorLabel").Property("Hidden", true)); // app.WaitForElement
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.Any());
        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenFirstEnteringTheBeverageSelectScreen()
        {
            AppResult[] warningLabel = app.Query(c => c.All("errorLabel").Property("Hidden", true)); // app.WaitForElement
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.Any());
        }
        #endregion

        #region Placeholder Tests
        [Test]
        public void TestThatPlaceholderTextInSearchBoxDisappearsWhenTextIsBeingEntered()
        {
            String userInput = "Coors";
            app.EnterText("SearchBeverage", userInput.ToString());
            
            AppResult[] searchBar = app.Query("searchBeverage");
            AppResult[] searchPlaceholder = app.Query(c => c.All("searchBeverage").Property("Placeholder", true));
            Assert.AreNotEqual(searchBar[0].Text, searchPlaceholder[0].Text);
        }

        [Test]
        public void TestThatPlaceHolderTextIsDisplayedWhenSearchIsNullOrEmpty() { }
        #endregion

        #region Activity Indicator Tests
        [Test]
        public void TestThatSpinnerDisplaysWhenUserBeginsTypingInSearchBox()
        {
            String userInput = "Coors";
            app.EnterText("SearchBeverage", userInput.ToString());
            AppResult[] spinner = app.Query(c => c.All("loadingSpinner").Property("IsRunning", true));
            Assert.IsTrue(spinner.Any());
        }

        [Test]
        public void TestThatSpinnerDisappearsWhenSearchIsCompleted()
        {
            String userInput = "Coors";
            app.EnterText("SearchBeverage", userInput.ToString());

            AppResult[] spinner = app.Query(c => c.All("loadingSpinner").Property("IsRunning", true));
            Assert.IsTrue(spinner.Any());
            
            spinner = app.Query(c => c.All("loadingSpinner").Property("IsRunning", false));
            Assert.IsTrue(spinner.Any());
        }
        #endregion
    }
}