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
using System.Threading;

/*  IMPORTANT!!! MUST SIGN IN ON APP BEFORE RUNNING ALL UI TESTS!
    ACCOUNT: prj3beer
    PASSWORD: Prj3beer##
*/

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class SignUpTests
    {
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        IApp app;
        Platform platform;

        public SignUpTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();

            // Tap the Ellipsis menu button
            //app.TapCoordinates(1350, 175);
            app.TapCoordinates(1020, 135);
        }

        #region Sign In/Up Tests

        [Test] // Test that all elements on the sign in page
        public void TestThatSignInScreenElementsExistOnPage()
        {
            // navigate to the sign up page
            //app.Tap("SignUpButton");
            //app.TapCoordinates(1350, 350);
            app.TapCoordinates(1020, 265);

            // Wait for the Google button to appear on screen
            app.WaitForElement("GoogleButton");

            // test that the Google sign in button is on the screen
            AppResult[] result = app.Query("GoogleButton");
            Assert.IsTrue(result.Any());

            // test that the Facebook sign in button is on screen
            result = app.Query("FacebookButton");
            Assert.IsTrue(result.Any());

            // test that the Cancel button is on screen
            result = app.Query("CancelButton");
            Assert.IsTrue(result.Any());

            // test that the label is on the screen
            result = app.Query("MessageLabel");
            Assert.IsTrue(result.Any());

            // test that the label contains the proper text
            Assert.AreEqual(result[0].Text, "Sign In With");
        }

        [Test] // Must sign in to application at least once to complete test
        public void TestThatUserIsTakenToBeverageSelectPageAfterSuccessfulSignInWithGoogle()
        {
            // navigate to the sign up page
            //app.Tap("SignInButton");
            //app.TapCoordinates(1350, 350);
            app.TapCoordinates(1020, 265);

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // Wait for the search field to be on the screen
            app.WaitForElement("searchBeverage");

            AppResult[] result = app.Query("searchBeverage");
            Assert.IsTrue(result.Any());
        }

        #endregion

        

        #region Sign out Tests
        
        [Test]
        public void TestThatSignOutButtonIsNotVisibleIfUserIsNotSignedIn()
        {
            //Open the hamburger menu
            //app.TapCoordinates(150, 90);
            //Open ellipsis menu
            //app.TapCoordinates(1350, 175);
            //app.TapCoordinates(1020, 135);

            //app.WaitForElement("btnSignOut");

            // test that the sign out button is on the screen
            AppResult[] result = app.Query("Sign Out");
            Assert.IsFalse(result.Any());
        }

        

        [Test]
        public void TestThatAUserCanSignInOrUpAfterLoggingOut()
        {
            // navigate to the sign up page
            //app.Tap("SignInButton");
            //app.TapCoordinates(1350, 350);
            app.TapCoordinates(1020, 265);
            
            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // test that the welcoming label contains welcoming text
            //app.WaitForElement("ContinueButton");

            // Tap the Continue Button
            //app.Tap("ContinueButton");
            Thread.Sleep(2000);

            //Open the hamburger menu
            //app.TapCoordinates(150, 90);
            //app.TapCoordinates(1350, 175);
            app.TapCoordinates(1020, 135);

            //app.WaitForElement("btnSignOut");

            //app.TapCoordinates(1350, 350);
            app.TapCoordinates(1020, 265);
            
            //Sign out
            app.Tap("YesButton");

            //Tap the ellipsis menu
            //app.TapCoordinates(1350, 175);
            app.TapCoordinates(1020, 135);

            // test that the sign in button is on the screen
            AppResult[] result = app.Query("Sign In");
            Assert.IsTrue(result.Any());
        }
        #endregion

        #region Pit o doooooooooooooooooooooom
        //[Test] Converted To Appium
        //public void TestThatUserIsNotAbleToSignInOrUpMultipleTimesWithGoogle()
        //{
        //    // navigate to the sign up page
        //    //app.Tap("SignInButton");
        //    app.TapCoordinates(1350, 350);

        //    // navigate to enter external credentials
        //    app.Tap("GoogleButton");

        //    // Wait for the beverage select page to appear again
        //    app.WaitForElement("searchBeverage");

        //    // Tap the ellipsis menu again
        //    app.TapCoordinates(1350, 175);

        //    // test that the sign out button is on the screen
        //    AppResult[] result = app.Query("Sign In");
        //    Assert.IsFalse(result.Any());
        //}

        //[Test] // Converted To Appium
        //public void TestThatSignOutButtonAppearsIfUserIsSignedIn()
        //{
        //    // navigate to the sign up page
        //    //app.Tap("SignInButton");
        //    app.TapCoordinates(1350, 350);

        //    // navigate to enter external credentials
        //    app.Tap("GoogleButton");

        //    //Open the  menu
        //    //app.TapCoordinates(150, 90);
        //    app.TapCoordinates(1350, 175);

        //    app.WaitForElement("Sign Out");

        //    // test that the sign out button is on the screen
        //    AppResult[] result = app.Query("Sign Out");
        //    Assert.IsTrue(result.Any());
        //}
        #endregion
    }
}