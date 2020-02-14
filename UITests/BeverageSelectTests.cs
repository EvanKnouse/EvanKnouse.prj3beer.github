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
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        List<string> beverages = new List<string> { "Churchill Blonde Lager", "Great Western Pilsner", "Great Western Radler", "Original 16 Copper Ale", "Rebellion Zilla IPA" };

        #region Test Beverages
        //Beverage bevGreatWesternRadler = new Beverage { BeverageID = 1, Name = "Great Western Radler", Brand = new Brand() { brandID = 4, brandName = "Great Western Brewery" }, Type = prj3beer.Models.Type.Radler, Temperature = 2 };
        //Beverage bevChurchLager = new Beverage { BeverageID = 2, Name = "Churchill Blonde Lager", Brand = new Brand() { brandID = 4, brandName = "Great Western Brewery" }, Type = prj3beer.Models.Type.Lager, Temperature = 3 };
        //Beverage bevBatch88 = new Beverage { BeverageID = 3, Name = "Batch 88", Brand = new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" }, Type = prj3beer.Models.Type.Stout, Temperature = 4 };
        //Beverage bevCoorsLight = new Beverage { BeverageID = 4, Name = "Coors Light", Brand = new Brand() { brandID = 25, brandName = "Coors" }, Type = prj3beer.Models.Type.Light, Temperature = 3 };
        //Beverage bevCoorsBanquet = new Beverage { BeverageID = 5, Name = "Coors Banquet", Brand = new Brand() { brandID = 25, brandName = "Coors" }, Type = prj3beer.Models.Type.Pale, Temperature = 2 };
        //Beverage bevCoorsEdge = new Beverage { BeverageID = 6, Name = "Coors Edge", Brand = new Brand() { brandID = 25, brandName = "Coors" }, Type = prj3beer.Models.Type.Radler, Temperature = 5 };
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

            /* Story 9 Navigation Change.
             // tap on the hamburger menu
            //app.TapCoordinates(150, 90);
            // tap to navigate to the beverage select screen
            //app.Tap("Beverage Select");
             */

        }

        #region Element on Screen Tests
        [Test]
        public void TestThatSearchBoxIsOnSelectBeverageScreen()
        {
            app.WaitForElement("searchBeverage");

            AppResult[] searchBar = app.Query("searchBeverage");

            Assert.IsTrue(searchBar.Any());
        }

        [Test]
        public void TestThatBeverageListViewIsOnSelectBeverageScreen()
        {
            app.WaitForElement("beverageListView");

            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }
        #endregion
        
        #region Search Result Tests
        #region Beverage Parameter Valid Search, Displays Beverage(s) Tests
        [Test]
        public void TestThatValidSearchBrandNameDisplaysMatchingBeverages()
        {
            string userInput = "Great Western";
            app.EnterText("searchBeverage",userInput.ToString());

            // the following three beverages should be found on the beverage select screen, in the list view
            AppResult[] beverage = app.Query(beverages[1]);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsTrue(beverage.Any());
        }

        [Test]
        public void TestThatValidSearchBeverageNameDisplaysMatchingBeverages()
        {
            string userInput = "Great Western Radler";
            app.EnterText("searchBeverage",userInput.ToString());

            // the following beverage should be found on the beverage select screen, in the list view
            AppResult[] beverage = app.Query(beverages[2]);
            Assert.IsTrue(beverage.Any());
        }

        [Test]
        public void TestThatValidSearchTypeDisplaysMatchingBeverages()
        {
            string userInput = "radler";
            app.EnterText("searchBeverage",userInput.ToString());

            // the following beverage should be found on the beverage select screen, in the list view
            AppResult[] beverage = app.Query(beverages[2]);
            Assert.IsTrue(beverage.Any());
        }
        #endregion

        #region Beverage Parameter Invalid Search, No Beverage(s) Tests
        [Test]
        public void TestThatInvalidSearchBrandNameDoesNotDisplayBeverages()
        {
            string userInput = "GWB";
            app.EnterText("searchBeverage",userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(beverages[1]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsFalse(beverage.Any());

            // test that the error message is properly displayed when no results are found
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"GWB\" could not be found/does not exist");
        }

        [Test]
        public void TestThatInvalidSearchBeverageNameDoesNotDisplayBeverages()
        {
            String userInput = "Maple Brew";
            app.EnterText("searchBeverage",userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(beverages[0]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[1]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsFalse(beverage.Any());

            // test that the error message is properly displayed when no results are found
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"Maple Brew\" could not be found/does not exist");
        }

        [Test]
        public void TestThatInvalidSearchTypeDoesNotDisplayBeverages()
        {
            string userInput = "Unleaded";
            app.EnterText("searchBeverage",userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(beverages[0]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[1]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsFalse(beverage.Any());

            // test that the error message is properly displayed when no results are found
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"Unleaded\" could not be found/does not exist");
        }
        #endregion

        #region SearchDisplay
        [Test]
        public void TestThatValidSearchCharacterCorrectlyDisplaysMatchingBeverages()
        {
            string userInput = "a";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverage = app.Query(beverages[0]);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(beverages[1]);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsTrue(beverage.Any());
        }

        [Test]
        public void TestThatSearchBoxCanBeTypedInto()
        {
            string userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }
        #endregion

        #region backSpacing
        [Test]
        public void TestThatBackSpacingACharacterBroadensResultSearch()
        {
            string userInput = "Great Western R";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverageList = app.Query(beverages[2]);
            Assert.IsTrue(beverageList.Any());   

            // empty text in search bar to simulate backspace
            app.ClearText("searchBeverage");
            userInput = "Great Western ";
            app.EnterText("searchBeverage", userInput.ToString());

            beverageList = app.Query(beverages[1]);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(beverages[2]);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(beverages[3]);
            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatBackSpacingAWordBroadensResultSearch()
        {
            string userInput = "Great Western Radler";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverageList = app.Query(beverages[2]);
            Assert.IsTrue(beverageList.Any());

            // empty text in search bar to simulate backspace
            app.ClearText("searchBeverage");
            userInput = "Great Western ";
            app.EnterText("searchBeverage", userInput.ToString());

            beverageList = app.Query(beverages[1]);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(beverages[2]);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(beverages[3]);
            Assert.IsTrue(beverageList.Any());
        }
        #endregion

        #region Space Character Search Tests
        [Test]
        public void TestThatJustSpacesSearchDoesNotDisplayBeverages()
        {
            string userInput = "       ";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] beverage = app.Query(beverages[0]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[1]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsFalse(beverage.Any());
        }

        [Test]
        public void TestThatLeadingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            string userInput = "   Great Western Radler";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverage = app.Query(beverages[2]);
            Assert.IsTrue(beverage.Any());
        }
        
        [Test]
        public void TestThatTrailingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            string userInput = "Great Western Radler   ";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] beverage = app.Query(beverages[2]);
            Assert.IsTrue(beverage.Any());
        }

        [Test]
        public void TestThatSpacesInTheMiddleOfTheSearchStringAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            string userInput = "Great Western     Radler";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverage = app.Query(beverages[2]);

            Assert.IsTrue(beverage.Any());
        }
        #endregion

        #region Invalid Search Character Tests
        [Test]
        public void TestThatEmptySearchStringDoesNotDisplayBeverages()
        {
            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(beverages[0]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[1]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsFalse(beverage.Any());
        }

        [Test]
        public void TestThatSpecialCharactersStringDoesNotDisplayBeverages()
        {
            string userInput = "$$@!:)";
            app.EnterText("searchBeverage", userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(beverages[0]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[1]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsFalse(beverage.Any());

            // test that the error message is properly displayed when no results are found
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"$$@!:)\" could not be found/does not exist");
        }

        [Test]
        public void TestThatNumberStringDoesNotDisplayBeverages()
        {
            string userInput = "853971";
            app.EnterText("searchBeverage", userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(beverages[0]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[1]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[2]);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(beverages[3]);
            Assert.IsFalse(beverage.Any());

            // test that the error message is properly displayed when no results are found
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"853971\" could not be found/does not exist");
        }
        #endregion
        #endregion

        #region Error Label Tests
        [Test]
        public void TestThatErrorMessageIsDisplayedWhenNoResultsAreFound()
        {
            string userInput = "Meepo";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] warningLabel = app.Query("errorLabel");

            // test that the error label is displayed on the screen
            Assert.IsTrue(warningLabel[0].Enabled);
            // test that the error label contains the proper error message
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"Meepo\" could not be found/does not exist");
        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenResultsAreFound()
        {
            string userInput = "Great";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] warningLabel = app.Query("errorLabel");

            Assert.IsFalse(warningLabel.Any());
        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenFirstEnteringTheBeverageSelectScreen()
        {
            AppResult[] warningLabel = app.Query("errorLabel");

            Assert.IsFalse(warningLabel[0].Text.Length < 0);
        }
        #endregion

        #region Placeholder Tests
        [Test]
        public void TestThatPlaceholderTextInSearchBoxDisappearsWhenTextIsBeingEntered()
        {
            app.WaitForElement("Please enter a beverage, type, or brand!!");

            AppResult[] placeholder = app.Query("Please enter a beverage, type, or brand!!");
            Assert.IsTrue(placeholder.Any());

            string userInput = "Great";
            app.EnterText("searchBeverage", userInput.ToString());

            placeholder = app.Query("Please enter a beverage, type, or brand!!");
            //Assert.AreNotEqual(placeholder[0].Text, "Please enter a beverage, type, or brand!!"); // if the placeholder text was overwritten
            Assert.AreNotEqual(placeholder[0].Text, "Please enter a beverage, type, or brand!!Coors"); // if the placeholder text was appended to
        }
        #endregion

        #region Activity Indicator Tests
        [Test]
        public void TestThatSpinnerDisappearsWhenSearchIsCompleted()
        {
            string userInput = "Great";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] spinner = app.Query("loadingSpinner");
            Assert.IsFalse(spinner.Any());
        }
        #endregion

        #region Story26
        [Test]
        public void TestThatListIsSortedAlphabetically()
        {
            //Wait for the Beverages List to appear on screen
            //app.WaitForElement("beverageList");
                       
            
            //List<string> listOfBrands = new List<string>();

            string userInput = "a";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] results = app.Query("beverageListView");

            // Get all the beverages into a sorted list
            var ascending = results.OrderBy(a => a.Text);

            // Check to see if the results equals ascending
            Assert.IsTrue(results.SequenceEqual(ascending));
        }

        [Test]
        public void TestThatAllValidBeveragesInLocalStorageAreDisplayed()
        {
            //Wait for the Beverages List to appear on screen
            //app.WaitForElement("beverageList");

            string userInput = "a";
            app.EnterText("searchBeverage", userInput.ToString());

            //Initialize App Result
            AppResult[] result = null;

            // Loop through all beverages in the valid beverage list,
            foreach (string beverageName in beverages)
            {
                // Query the app for the current beverage list
                result = app.Query(beverageName);
                // Check to see if the beverage exists on the page.
                Assert.IsTrue(result.Any());
            }
        }

        //[Test]
        //public void TestThatErrorMessageIsShownIfUnableToConnectToAPI()
        //{
        //    //Pick Select screen from the screen selection menu
        //    app.Tap("Beverage Select");

        //    //Wait for the Beverages List to appear on screen
        //    app.WaitForElement("beverageList");

        //    // Tap the Refresh Button
        //    app.Tap("Refresh");

        //    //Look for the expected error message on screen
        //    AppResult[] result = app.Query(("Connection issue, please try again later"));

        //    //Will return true if the app result contains the error message
        //    Assert.IsTrue(result.Any());
        //}

        #endregion

    }
}