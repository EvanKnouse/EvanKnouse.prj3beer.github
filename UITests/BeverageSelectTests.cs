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

       // Beverage CLBeverage = new Beverage() { BeverageID=1,Name="Coors Light",brand=new Brand { brandID = 1,brandName="Coors" }, type = prj3beer.Models.Type.ale, Temperature = 5 };

        IApp app;
        Platform platform;

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
            app.Tap("SearchBeverage");
        }

        #region List View Tests
        [Test]
        public async Task TestThatBeverageListIsSortedAlphabetically()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString().ToLower());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            var ascending = beverageList.OrderBy(a => a.Text);
            Assert.IsFalse(beverageList.SequenceEqual(ascending));
        }

        [Test]
        public void TestThatbeverageListViewIsOnSelectBeverageScreen()
        {
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }
        #endregion

        #region Search Bar Tests
        [Test]
        public async Task TestThatValidSearchCharacterCorrectlyDisplaysMatchingBeverages()
        {
            String userInput = "a";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatValidSearchBrandNameDisplaysMatchingBeverages()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
          //  AppResult[] beverageList = app.Query(CLBeverage.Name);
           // Assert.IsTrue(beverageList.Any());

            //This will be used for multiple beverages with the name coors in it
            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());

            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatInvalidSearchBrandNameDoesNotDisplayBeverages()
        {
            String userInput = "Coorss";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public async Task TestThatValidSearchBeverageNameDisplaysMatchingBeverages()
        {
            String userInput = "Coors Light";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
          //  AppResult[] beverageList = app.Query(CLBeverage.Name);
           // Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatInvalidSearchBeverageNameDoesNotDisplayBeverages()
        {
            String userInput = "Maple Brew";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public async Task TestThatValidSearchTypeDisplaysMatchingBeverages()
        {
            String userInput = "ale";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
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
        public async Task TestThatInvalidSearchTypeDoesNotDisplayBeverages()
        {
            String userInput = "Unleaded";
            app.EnterText("SearchBeverage", userInput.ToString().ToLower());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Count() > 0);
        }

        [Test]
        public async Task TestThatTypingInCoorsLiteDisplaysTwoResults()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.AreEqual(1, beverageList.Count());
        }

        [Test]
        public void TestThatSearchBoxIsOnSelectBeverageScreen()
        {
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatSearchBoxCanBeTypedInto()
        {
            String userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatBackSpacingACharacterBroadensResultSearch()
        {
            String userInput = "Coors L";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Count() == 1);

            userInput = "Coors ";
            app.EnterText("searchBeverage", userInput.ToString());
            Assert.IsTrue(beverageList.Count() == 4);

        }

        [Test]
        public async Task TestThatBackSpacingAWordBroadensResultSearch()
        {
            String userInput = "Coors Lite";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Count() == 1);

            userInput = "Coors ";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);
            Assert.IsTrue(beverageList.Count() == 4);
        }

        [Test]
        public async Task TestThatBackspacingEntireSearchStringDoesNotStartSearchProcess()
        {
            String userInput = "Coors Lite";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Count() == 1);

            userInput = "";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);
            Assert.IsTrue(beverageList.Count() == 0);
        }

        [Test]
        public async Task TestThatDeviceWaitsACertainAmountOfTimeBeforeQueryingLocalStorage()
        {
            String userInput = "Coors";
            app.EnterText("searchBeverage", userInput.ToString());
            AppResult[] beverageList = app.Query("beverageListView");
            Assert.IsFalse(beverageList.Count() >= 1);
            await Task.Delay(1500);
            beverageList = app.Query("beverageListView");
            Assert.IsTrue(beverageList.Count() >= 1);
        }

        #region Space Character Tests
        [Test]
        public async Task TestThatJustSpacesSearchDoesNotDisplayBeverages()
        {
            String userInput = "       ";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }


        [Test]
        public async Task TestThatLeadingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "   Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }


        [Test]
        public async Task TestThatTrailingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "Coors Lite   ";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatSpacesInTheMiddleOfTheSearchStringAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "Coors     Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
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
        public async Task TestThatSpecialCharactersStringDoesNotDisplayBeverages()
        {
            String userInput = "$$@!:)";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public async Task TestThatNumberStringDoesNotDisplayBeverages()
        {
            String userInput = "853971";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }
        #endregion
        #endregion

        #region Error Label Tests
        [Test]
        public async Task TestThatErrorMessageIsDisplayedWhenNoResultsAreFound()
        {
            String userInput = "Meepo";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);

            AppResult[] warningLabel = app.Query(c => c.All("hiddenLabel").Property("Hidden", false)); // app.WaitForElement
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.Any());
        }

        [Test]
        public async Task TestThatErrorMessageIsNotDisplayedWhenResultsAreFound()
        {
            String userInput = "Coors";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);

            AppResult[] warningLabel = app.Query(c => c.All("hiddenLabel").Property("Hidden", true)); // app.WaitForElement
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.Any());
        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenFirstEnteringTheBeverageSelectScreen()
        {
            AppResult[] warningLabel = app.Query(c => c.All("hiddenLabel").Property("Hidden", true)); // app.WaitForElement
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.Any());
        }
        #endregion

        #region Placeholder Tests
        [Test]
        public async Task TestThatPlaceholderTextInSearchBoxDisappearsWhenTextIsBeingEntered()
        {
            String userInput = "Coors";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
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
        public async Task TestThatSpinnerDisappearsWhenSearchIsCompleted()
        {
            String userInput = "Coors";
            app.EnterText("SearchBeverage", userInput.ToString());

            AppResult[] spinner = app.Query(c => c.All("loadingSpinner").Property("IsRunning", true));
            Assert.IsTrue(spinner.Any());
            await Task.Delay(1500);
            spinner = app.Query(c => c.All("loadingSpinner").Property("IsRunning", false));
            Assert.IsTrue(spinner.Any());
        }
        #endregion
    }
}