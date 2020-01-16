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
        List<Beverage> invalidBeverageList = new List<Beverage>();


        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        public SelectTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //Initialize the app, arrive at home page (default for now)
            app = app = ConfigureApp.Android.ApkFile(apkPath).StartApp();

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
        public void TestThatAllValidBeveragesInLocalStorageAreDisplayed()
        {
            //Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            //Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            //Look for the beveragesn on the select screen
            AppResult[] result = app.Query("beverageList");

            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.IsTrue(result.Equals(validBeverageList));
        }

        [Test]
        public void TestThatErrorMessageIsShownIfNoBeverageExists()
        {
            //Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            //Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            //Look for the beveragesn on the select screen
            AppResult[] result = app.Query(("Beverages"));

            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.AreEqual(result[0].Text, "Connection issue, please try again later");

        }

        [Test] // No Valid Beverages
        public void TestThatErrorMessageIsShownIfNoBeveragesValidate()
        {
            //Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            //Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            //Look for the beveragesn on the select screen
            AppResult[] result = app.Query(("Beverages"));

            //Will be greater than 0 if it exists, returns AppResult[]
            Assert.AreEqual(result[0].Text,"Connection issue, please try again later");
        }

        [Test]
        public void TestThatListIsSortedAlphabetically()
        {
            //Pick Select screen from the screen selection menu
            app.Tap("Beverage Select");

            //Wait for the Beverages List to appear on screen
            app.WaitForElement("beverageList");

            AppResult[] results = app.Query("Beverages");
            //List<string> listOfBrands = new List<string>();

            // Get all the beverages into a sorted list
            var ascending = results.OrderBy(a => a.Text);

            // Check to see if the results equals ascending
            Assert.IsTrue(results.SequenceEqual(ascending));
        }
    }
}
