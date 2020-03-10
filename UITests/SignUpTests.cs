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
            app.TapCoordinates(1350, 175);
        }

        #region Sign In/Up Tests

        [Test] // Test that all elements on the sign in page
        public void TestThatSignInScreenElementsExistOnPage()
        {
            // navigate to the sign up page
            //app.Tap("SignUpButton");
            app.TapCoordinates(1350, 350);

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
            app.TapCoordinates(1350, 350);

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // Wait for the search field to be on the screen
            app.WaitForElement("searchBeverage");

            AppResult[] result = app.Query("searchBeverage");
            Assert.IsTrue(result.Any());
        }
        
        [Test] // Must sign in to application at least once to complete test
        public void TestThatUserIsTakenToBeverageSelectPageAfterSuccessfulSignInWithFacebook()
        {
            // navigate to the sign up page - tap the Sign In button in the ellipsis menu
            app.TapCoordinates(1350, 350);

            // navigate to enter external credentials
            app.Tap("FacebookButton");

            // Wait for the search field to be on the screen
            app.WaitForElement("searchBeverage");

            AppResult[] result = app.Query("searchBeverage");
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void TestThatUserIsUnableToSignInOrUpMultipleTimesWithFacebook()
        {
            // navigate to the sign up page
            app.TapCoordinates(1350, 350);

            // navigate to enter external credentials
            app.Tap("FacebookButton");

            Thread.Sleep(5000);

            // Wait for the beverage select page appears again
            app.WaitForElement("searchBeverage");

            // Tap the ellipsis menu again
            app.TapCoordinates(1350, 175);

            // Look for Sign In button
            AppResult[] result = app.Query("Sign In");

            // Sign In button shouldn't appear on screen
            Assert.IsFalse(result.Any());

            // Look for Sign Out button
            result = app.Query("Sign Out");

            // Sign Out button should appear on screen
            Assert.IsTrue(result.Any());
        }
        #endregion

        #region Tests That Cant Run
        //[Test] // This test cannot be run, Xamarin has no access to the Android UI Or Google Prompts
        //public void TestThatUserIsTakenOutsideOfAppToSelectExternalAccountGoogle()
        //{
        //    // navigate to the sign up page
        //    //app.Tap("SignUpButton");

        //    // navigate to enter external credentials
        //    //app.Tap("GoogleButton");

        //    // test that we're on the page, somehow

        //    // select account or enter credentials?
        //    //app.TapCoordinates(x, y);
        //    // allow permissions?
        //    //app.Tap("Allow");
        //    Assert.IsTrue(true);
        //}

        //[Test] // This test cannot be run, Xamarin has no access to the Android UI Or Google Prompts
        //public void TestThatUserIsTakenOutsideOfAppToSelectExternalAccountFacebook()
        //{
        //    // navigate to the sign up page
        //    //app.Tap("SignUpButton");

        //    // navigate to enter external credentials
        //    //app.Tap("FacebookButton");

        //    // test that we're on the page, somehow

        //    // select account or enter credentials?
        //    //app.TapCoordinates(x, y);
        //    // allow permissions?
        //    //app.Tap("Allow");
        //    Assert.IsTrue(true);
        //}

        //[Test] // Technically you can only run this test ONCE with a brand new google account
        //public void TestUserCanAllowAppToAccessProfileInformation()
        //{
        //    // navigate to the sign up page
        //    //app.Tap("SignUpButton");

        //    // navigate to enter external credentials
        //    //app.Tap("GoogleButton");

        //    // select account or enter credentials
        //    //app.TapCoordinates(690, 1300);

        //    // You would enter your credentials (email,password)

        //    // test that the allow button is on the permissions screen
        //    //AppResult[] allow = app.Query("Allow");
        //    //Assert.IsTrue(allow.Any());
        //    Assert.IsTrue(true);
        //}

        //[Test] // Test Passes IF You Have already signed in to Application Previously
        //// AND you have to click the USER PROFILE!?!
        //public void TestThatUsersNameIsDisplayedAfterSigningIn()
        //{
        //    // navigate to the sign up page
        //    app.Tap("SignInButton");

        //    // wait for google button
        //    app.WaitForElement("GoogleButton");

        //    // navigate to enter external credentials
        //    app.Tap("GoogleButton");

        //    // select account or enter credentials
        //    //app.TapCoordinates(690, 1300); // Taps behind Android Prompt Window

        //    app.WaitForElement("WelcomeLabel");

        //    // test that the welcoming label is there
        //    AppResult[] results = app.Query("WelcomeLabel");
        //    Assert.IsTrue(results.Any());

        //    // test that the welcoming label contains welcoming text
        //    Assert.AreEqual(results[0].Text, "Welcome back Levis Media");
        //}

        //[Test] // This test cannot be run, Xamarin has no access to the Android UI Or Google Prompts
        //public void TestThatUsersNameIsDisplayedAfterSigningUp()
        //{
        //    // navigate to the sign up page
        //    //app.Tap("SignUpButton");

        //    // navigate to enter external credentials
        //    //app.Tap("GoogleButton");

        //    // select account or enter credentials
        //    //app.TapCoordinates(690, 1300);

        //    // test that the welcoming label is there
        //    //AppResult[] results = app.Query("WelcomeLabel");
        //    //Assert.IsTrue(results.Any());

        //    // test that the welcoming label contains welcoming text
        //    //Assert.AreEqual(results[0].Text, "Welcome Levis Media");
        //    Assert.IsTrue(true);
        //}

        //[Test] // This test cannot be run, Xamarin has no access to the Android UI Or Google Prompts
        //public void TestThatUserIsTakenBackToSignUpPageAfterCancellingSignUp()
        //{
        //    // navigate to the sign up page
        //    //app.Tap("SignUpButton");

        //    // navigate to enter external credentials
        //    //app.Tap("SignInButton");

        //    // select account or enter credentials
        //    //app.TapCoordinates(x, y);

        //    // hit the cancel button
        //    //app.TapCoordinates(x, y);

        //    // wait for the main page to be displayed
        //    //app.WaitForElement("searchBeverage");

        //    // test that the sign up button is on the screen, as the user is sent back to the original screen
        //    //AppResult[] button = app.Query("btnSignUp");
        //    Assert.IsTrue(true);
        //}
        #endregion

        #region Sign out Tests
        [Test]
        public void TestThatSignOutButtonAppearsIfUserIsSignedIn()
        {
            // navigate to the sign up page
            //app.Tap("SignInButton");
            app.TapCoordinates(1350, 350);

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            //Open the  menu
            //app.TapCoordinates(150, 90);
            app.TapCoordinates(1350, 175);

            app.WaitForElement("Sign Out");

            // test that the sign out button is on the screen
            AppResult[] result = app.Query("Sign Out");
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void TestThatSignOutButtonIsNotVisibleIfUserIsNotSignedIn()
        {
            //Open the hamburger menu
            //app.TapCoordinates(150, 90);
            //Open ellipsis menu
            app.TapCoordinates(1350, 175);

            //app.WaitForElement("btnSignOut");

            // test that the sign out button is on the screen
            AppResult[] result = app.Query("Sign Out");
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void TestThatUserIsNotAbleToSignInOrUpMultipleTimesWithGoogle()
        {
            // navigate to the sign up page
            //app.Tap("SignInButton");
            app.TapCoordinates(1350, 350);

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // Wait for the beverage select page to appear again
            app.WaitForElement("searchBeverage");

            // Tap the ellipsis menu again
            app.TapCoordinates(1350, 175);

            // test that the sign out button is on the screen
            AppResult[] result = app.Query("Sign In");
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void TestThatAUserCanSignInOrUpAfterLoggingOut()
        {
            // navigate to the sign up page
            //app.Tap("SignInButton");
            app.TapCoordinates(1350, 350);

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // test that the welcoming label contains welcoming text
            //app.WaitForElement("ContinueButton");

            // Tap the Continue Button
            //app.Tap("ContinueButton");
            Thread.Sleep(2000);

            //Open the hamburger menu
            //app.TapCoordinates(150, 90);
            app.TapCoordinates(1350, 175);

            //app.WaitForElement("btnSignOut");

            app.TapCoordinates(1350, 350);
            //Sign out
            app.Tap("YesButton");

            //Tap the ellipsis menu
            app.TapCoordinates(1350, 175);

            // test that the sign in button is on the screen
            AppResult[] result = app.Query("Sign In");
            Assert.IsTrue(result.Any());
        }
        #endregion
    }
}