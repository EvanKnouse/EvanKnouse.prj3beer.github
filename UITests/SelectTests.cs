using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using prj3beer.Models;
using System.Collections.Generic;

namespace UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class SelectTest
    {
        IApp app;
        Platform platform;

        // TODO: Populate Two Lists Here, Both the Valid And Invalid Beverages
        List<string> validBeverageList = new List<string>();

        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public SelectTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            Settings.URLSetting = "";

            //Initialize the app, arrive at home page (default for now)
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();

            Settings.URLSetting = "";


            validBeverageList.Clear();
            validBeverageList.Add("Churchill Blonde Lager");
            validBeverageList.Add("Great Western Pilsner");
            validBeverageList.Add("Great Western Radler");
            validBeverageList.Add("Original 16 Copper Ale");
            validBeverageList.Add("Rebellion Zilla IPA");
            
            //Tap into the screen navigation menu
            app.TapCoordinates(150, 90);    //TODO: Coordinates will have to be set up to exact location(default for now)
            //app.Tap(c => c.Marked("hamburglar"));

        }

        [Test]
        public void TestThatListViewExistsOnPage()
        {
            // Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            // Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            // Query the app for the beverageList
            AppResult[] brandList = app.Query("beverageList");

            // If there is any result, the list exists
            Assert.IsTrue(brandList.Any());
        }


        [Test]
        public void TestThatAllValidBeveragesInLocalStorageAreDisplayed()
        {
            //Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            //Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            //Initialize App Result
            AppResult[] result = null;

            // Loop through all beverages in the valid beverage list,
            foreach (string beverageName in validBeverageList)
            {
                // Query the app for the current beverage list
                result = app.Query(beverageName);
                // Check to see if the beverage exists on the page.
                Assert.IsTrue(result.Any());
            }
        }

        [Test]
        public void TestThatErrorMessageIsShownIfUnableToConnectToAPI()
        {
            //Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            //Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            // Tap the Refresh Button
            app.Tap("Refresh");

            //Look for the expected error message on screen
            AppResult[] result = app.Query(("Connection issue, please try again later"));

            //Will return true if the app result contains the error message
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void TestThatListIsSortedAlphabetically()
        {
            //Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            //Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            AppResult[] results = app.Query("beverageList");
            //List<string> listOfBrands = new List<string>();

            // Get all the beverages into a sorted list
            var ascending = results.OrderBy(a => a.Text);

            // Check to see if the results equals ascending
            Assert.IsTrue(results.SequenceEqual(ascending));
        }
    }
}
