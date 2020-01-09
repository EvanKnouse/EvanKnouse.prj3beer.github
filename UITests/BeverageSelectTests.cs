
using NUnit.Framework;
using prj3beer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

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
        public void TestThatValidSearchCharacterCorrectlyDisplaysMatchingBeverages()
        {

            String userInput = "a";

            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatValidSearchBrandNameDisplaysMatchingBeverages()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString());

            AppResult[] beverageList = app.Query(CLBeverage);
            Assert.IsTrue(beverageList.Any());


            //This will be used for multiple beverages with the name coors in it
            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());

            //AppResult[] beverageList = app.Query(CLBeverage);
            //Assert.IsTrue(beverageList.Any());

        }

        [Test]
        public void TestThatInvalidSearchBrandNameDoesNotDisplayBeverages()
        {
            String userInput = "coorss";

            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatValidSearchBeverageNameDisplaysMatchingBeverages()
        {
            String userInput = "Coors Light";
 
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(CLBeverage);
            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatInvalidSearchBeverageNameDoesNotDisplayBeverages()
        {
            String userInput = "Maple Brew";

            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

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
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatBeverageListIsSortedAlphabetically()
        {
            String userInput = "Coors";

            app.EnterText("SearchBeverage", userInput.ToString().ToLower());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            var ascending = beverageList.OrderBy(a => a.Text);
            Assert.IsFalse(beverageList.SequenceEqual(ascending));
        }

        [Test]
        public void TestThatEmptySearchStringDoesNotDisplayBeverages()
        {
            AppResult[] beverageList = app.Query(beverageListBox);
            Assert.IsFalse(beverageList.Any());

        }

        [Test]
        public void TestThatJustSpacesSearchDoesNotDisplayBeverages()
        {
            String userInput = "            ";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsFalse(beverageList.Any());
        }


        [Test]
        public void TestThatLeadingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "            Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsTrue(beverageList.Any());
        }


        [Test]
        public void TestThatTrailingSpacesAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput = "Coors Lite                ";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatSpacesInTheMiddleOfTheSearchStringAreTrimmedAndValidBeveragesAreStillDisplayed()
        {
            String userInput ="Coors           Lite";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatSpecialCharactersStringDoesNotDisplayBeverages()
        {
            String userInput = "$$@!:)";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatNumberStringDoesNotDisplayBeverages()
        {
            String userInput = "853971";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.IsFalse(beverageList.Any());
        }

        [Test]
        public void TestThatTypingInCoorsLiteDisplaysTwoResults()
        {
            String userInput = "Coors Lite";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(beverageListBox);

            Assert.AreEqual(1, beverageList.Count());
        }

        [Test]
        public void TestThatSearchBoxIsOnSelectBeverageScreen()
        {
            AppResult[] beverageList = app.Query(SearchBeverage);

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatBeverageListBoxIsOnSelectBeverageScreen()
        {
            AppResult[] beverageList = app.Query(SearchBeverage);

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatSearchBoxCanBeTypedInto()
        {
            String userInput = "Coors Lite                ";
            app.EnterText("SearchBeverage", userInput.ToString());

            //This will query the listbox which is not implemented yet.
            AppResult[] beverageList = app.Query(SearchBeverage.text);

            Assert.IsTrue(beverageList.Any());
        }

        [Test]
        public void TestThatBackSpacingACharacterBroadensResultSearch()
        {

        }

        [Test]
        public void TestThatBackSpacingAWordBroadensResultSearch()
        {

        }

        [Test]
        public void TestThatBackspacingEntireSearchStringDoesNotStartSearchProcess()
        {

        }

        [Test]
        public void TestThatErrorMessageIsDisplayedWhenNoResultsAreFound()
        {

        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenResultsAreFound()
        {

        }

        [Test]
        public void TestThatErrorMessageIsNotDisplayedWhenFirstEnteringTheBeverageSelectScreen()
        {

        }

        [Test]
        public void TestThatPlaceholderTextInSearchBoxDisappearsWhenTextIsBeginEntered()
        {

        }

        [Test]
        public void TestThatPlaceHolderTextIsDisplayedWhenSearchIsNullOrEmpty()
        {

        }

        [Test]
        public void TestThatDeviceWaitsACertainAmountOfTimeBeforeQueryingLocalStorage()
        {

        }

        [Test]
        public void TestThatSpinnerDisplaysWhenUserBeginsTypingInSearchBox()
        {

        }

        [Test]
        public void TestThatSpinnerDisappearsWhenSearchIsCompleted()
        {

        }


    }
}
