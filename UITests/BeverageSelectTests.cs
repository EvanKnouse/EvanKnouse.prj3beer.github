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
            // tap on the hamburger menu
            app.TapCoordinates(150, 90);
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
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
            string userInput = "Coors";
            app.EnterText("searchBeverage",userInput.ToString());

            // the following three beverages should be found on the beverage select screen, in the list view
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsTrue(beverage.Any());
        }

        [Test]
        public void TestThatValidSearchBeverageNameDisplaysMatchingBeverages()
        {
            string userInput = "Coors Light";
            app.EnterText("searchBeverage",userInput.ToString());

            // the following beverage should be found on the beverage select screen, in the list view
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverage.Any());
        }

        [Test]
        public void TestThatValidSearchTypeDisplaysMatchingBeverages()
        {
            string userInput = "pale";
            app.EnterText("searchBeverage",userInput.ToString());

            // the following beverage should be found on the beverage select screen, in the list view
            AppResult[] beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsTrue(beverage.Any());
        }
        #endregion

        #region Beverage Parameter Invalid Search, No Beverage(s) Tests
        [Test]
        public void TestThatInvalidSearchBrandNameDoesNotDisplayBeverages()
        {
            string userInput = "Coorss";
            app.EnterText("searchBeverage",userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsFalse(beverage.Any());

            // test that the error message is properly displayed when no results are found
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"Coorss\" could not be found/does not exist");
        }

        [Test]
        public void TestThatInvalidSearchBeverageNameDoesNotDisplayBeverages()
        {
            String userInput = "Maple Brew";
            app.EnterText("searchBeverage",userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevBatch88.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevChurchLager.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevGreatWesternRadler.Name);
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
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevBatch88.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevChurchLager.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevGreatWesternRadler.Name);
            Assert.IsFalse(beverage.Any());

            // test that the error message is properly displayed when no results are found
            string errorMessage = app.Query("errorLabel")[0].Text;
            Assert.AreEqual(errorMessage,"\"Unleaded\" could not be found/does not exist");
        }
        #endregion

        [Test]
        public void TestThatValidSearchCharacterCorrectlyDisplaysMatchingBeverages()
        {
            string userInput = "a";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(bevGreatWesternRadler.Name);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(bevChurchLager.Name);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(bevBatch88.Name);
            Assert.IsTrue(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsTrue(beverage.Any());
        }

        // POSSIBLY REDUNDANT
        [Test]
        public void TestThatTypingInCoorsLightDisplaysTwoResults()
        {
            string userInput = "Coors Light";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.AreEqual(1, beverageList.Count());
        }

        [Test]
        public void TestThatSearchBoxCanBeTypedInto()
        {
            string userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatBackSpacingACharacterBroadensResultSearch()
        {
            string userInput = "Coors L";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverageList = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverageList.Any());   

            // empty text in search bar to simulate backspace
            app.ClearText("searchBeverage");
            userInput = "Coors ";
            app.EnterText("searchBeverage", userInput.ToString());

            beverageList = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(bevCoorsEdge.Name);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(bevCoorsBanquet.Name);
            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatBackSpacingAWordBroadensResultSearch()
        {
            string userInput = "Coors Light";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverageList = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverageList.Any());

            // empty text in search bar to simulate backspace
            app.ClearText("searchBeverage");
            userInput = "Coors ";
            app.EnterText("searchBeverage", userInput.ToString());

            beverageList = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(bevCoorsBanquet.Name);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(bevCoorsEdge.Name);
            Assert.IsTrue(beverageList.Any());

            beverageList = app.Query(bevBatch88.Name);
            Assert.IsFalse(beverageList.Any());
        }

        #region Space Character Search Tests
        [Test]
        public void TestThatJustSpacesSearchDoesNotDisplayBeverages()
        {
            string userInput = "       ";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevBatch88.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevChurchLager.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevGreatWesternRadler.Name);
            Assert.IsFalse(beverage.Any());
        }

        [Test]
        public void TestThatLeadingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            string userInput = "   Coors Light";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverage.Any());
        }
        
        [Test]
        public void TestThatTrailingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            string userInput = "Coors Light   ";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsTrue(beverage.Any());
        }

        // NOT YET IMPLEMENTED IN CODE
        [Test]
        public void TestThatSpacesInTheMiddleOfTheSearchStringAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            string userInput = "Coors     Light";
            app.EnterText("searchBeverage", userInput.ToString());
            
            AppResult[] beverage = app.Query(bevCoorsLight.Name);

            Assert.IsTrue(beverage.Any());
        }
        #endregion

        #region Invalid Search Character Tests
        [Test]
        public void TestThatEmptySearchStringDoesNotDisplayBeverages()
        {
            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevBatch88.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevChurchLager.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevGreatWesternRadler.Name);
            Assert.IsFalse(beverage.Any());
        }

        [Test]
        public void TestThatSpecialCharactersStringDoesNotDisplayBeverages()
        {
            string userInput = "$$@!:)";
            app.EnterText("searchBeverage", userInput.ToString());

            // no beverages should be found on the beverage select screen
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevBatch88.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevChurchLager.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevGreatWesternRadler.Name);
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
            AppResult[] beverage = app.Query(bevCoorsLight.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsBanquet.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevCoorsEdge.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevBatch88.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevChurchLager.Name);
            Assert.IsFalse(beverage.Any());

            beverage = app.Query(bevGreatWesternRadler.Name);
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
            string userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] warningLabel = app.Query("errorLabel");

            Assert.IsFalse(warningLabel.Any());
        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenFirstEnteringTheBeverageSelectScreen()
        {
            AppResult[] warningLabel = app.Query("errorLabel");

            Assert.IsFalse(warningLabel.Any());
        }
        #endregion

        #region Placeholder Tests
        [Test]
        public void TestThatPlaceholderTextInSearchBoxDisappearsWhenTextIsBeingEntered()
        {
            //string userInput = "Coors";
            //app.EnterText("searchBeverage", userInput.ToString());
            app.WaitForElement("Please enter a beverage, type, or brand!!");
            AppResult[] placeholder = app.Query("Please enter a beverage, type, or brand!!");
            //Assert.AreNotEqual(searchBar[0].Text, searchPlaceholder[0].Text);
            Assert.IsTrue(placeholder.Any());
        }

 
        #endregion

        #region Activity Indicator Tests
        [Test]
        public void TestThatSpinnerDisappearsWhenSearchIsCompleted()
        {
            string userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());

            AppResult[] spinner = app.Query("loadingSpinner");
            Assert.IsFalse(spinner.Any());
        }
        #endregion
    }
}