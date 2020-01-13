
using NUnit.Framework;
using prj3beer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Views;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class BeverageSelectTests
    {
        //Instead of querying on any (in case its empty) just make sure it contains the correct number of beverages
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        Beverage CLBeverage = new Beverage() { };

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

        [Test]
        public async Task TestThatValidSearchCharacterCorrectlyDisplaysMatchingBeveragesAsync()
        {

            String userInput = "a";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListView);

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatValidSearchBrandNameDisplaysMatchingBeveragesAsync()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            AppResult[] beverageList = app.Query(CLBeverage);
            Assert.IsTrue(beverageList.Any());


            //This will be used for multiple beverages with the name coors in it
            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());

            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());

        }

        [Test]
        public async Task TestThatInvalidSearchBrandNameDoesNotDisplayBeveragesAsync()
        {
            String userInput = "coorss";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public async Task TestThatValidSearchBeverageNameDisplaysMatchingBeveragesAsync()
        {
            String userInput = "Coors Light";
 
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(CLBeverage);
            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatInvalidSearchBeverageNameDoesNotDisplayBeveragesAsync()
        {
            String userInput = "Maple Brew";

            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListView);

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public async Task TestThatValidSearchTypeDisplaysMatchingBeveragesAsync()
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
        public async Task TestThatInvalidSearchTypeDoesNotDisplayBeveragesAsync()
        {
            String userInput = "Unleaded";
            app.EnterText("SearchBeverage", userInput.ToString().ToLower());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Count() > 0);
        }

        [Test]
        public async Task TestThatBeverageListIsSortedAlphabeticallyAsync()
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
        public void TestThatEmptySearchStringDoesNotDisplayBeverages()
        {
            AppResult[] beverageList = app.Query("beverageListView");
            Assert.IsFalse(beverageList.Any());

        }

        [Test]
        public async Task TestThatJustSpacesSearchDoesNotDisplayBeveragesAsync()
        {
            String userInput = "            ";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }


        [Test]
        public async Task TestThatLeadingSpacesAreTrimmedAndValidBeveragesAreStillDisplayedAsync()
        {
            String userInput = "            Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }


        [Test]
        public async Task TestThatTrailingSpacesAreTrimmedAndValidBeveragesAreStillDisplayedAsync()
        {
            String userInput = "Coors Lite                ";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatSpacesInTheMiddleOfTheSearchStringAreTrimmedAndValidBeveragesAreStillDisplayedAsync()
        {
            String userInput ="Coors           Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatSpecialCharactersStringDoesNotDisplayBeveragesAsync()
        {
            String userInput = "$$@!:)";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public async Task TestThatNumberStringDoesNotDisplayBeveragesAsync()
        {
            String userInput = "853971";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("beverageListView");

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public async Task TestThatTypingInCoorsLiteDisplaysTwoResultsAsync()
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
        public void TestThatbeverageListViewIsOnSelectBeverageScreen()
        {
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatSearchBoxCanBeTypedIntoAsync()
        {
            String userInput = "Coors Lite                ";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query("searchBeverage");

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public async Task TestThatBackSpacingACharacterBroadensResultSearchAsync()
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
        public async Task TestThatBackSpacingAWordBroadensResultSearchAsync()
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
        public async Task TestThatBackspacingEntireSearchStringDoesNotStartSearchProcessAsync()
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
        public async Task TestThatErrorMessageIsDisplayedWhenNoResultsAreFoundAsync()
        {
            String userInput = "$";
            app.EnterText("searchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.hidden == false);
        }

        [Test]
        public async Task TestThatErrorMessageIsNotDisplayedWhenResultsAreFoundAsync()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            //This will query the listbox which is not implemented yet.
            Assert.IsTrue(warningLabel.hidden == true);
        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenFirstEnteringTheBeverageSelectScreen()
        {
            Assert.IsTrue(warningLabel.hidden == true);
        }

        [Test]
        public async Task TestThatPlaceholderTextInSearchBoxDisappearsWhenTextIsBeginEnteredAsync()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            await Task.Delay(1500);
            Assert.AreNotEqual(searchBeverage.text, searchBeverage.placeHolder);
        }

        [Test]
        public void TestThatPlaceHolderTextIsDisplayedWhenSearchIsNullOrEmpty()
        {

        }

        [Test]
        public async Task TestThatDeviceWaitsACertainAmountOfTimeBeforeQueryingLocalStorageAsync()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            AppResult[] beverageList = app.Query(SearchBeverage.text);
            Assert.IsFalse(beverageList.Any());
            await Task.Delay(1500);
            beverageList = app.Query(SearchBeverage.text);
            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatSpinnerDisplaysWhenUserBeginsTypingInSearchBox()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());
            Assert.IsTrue(loadSpinner.hidden == false);
        }

        [Test]
        public async Task TestThatSpinnerDisappearsWhenSearchIsCompletedAsync()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());

            Assert.IsTrue(loadSpinner.hidden == false);
            await Task.Delay(1500);
            
            Assert.IsTrue(loadSpinner.hidden == true);
        }


    }
}
