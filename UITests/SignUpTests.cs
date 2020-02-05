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

        }

        #region Sign In/Up Tests
        [Test]
        public void TestThatSignInSignUpButtonsAreOnScreen()
        {
            // Wait for the sign up button to appear on screen
            app.WaitForElement("SignUpButton");

            // test that the sign up button is on the screen
            AppResult[] button = app.Query("SignUpButton");
            Assert.IsTrue(button.Any());

            // test that the sign in button is on the screen
            button = app.Query("SignInButton");
            Assert.IsTrue(button.Any());
        }

        [Test]
        public void TestThatSignUpScreenElementsExistOnPage()
        {
            // navigate to the sign up page
            app.Tap("SignUpButton");

            // Wait for the Google button to appear on screen
            app.WaitForElement("GoogleButton");

            // test that the Google sign in button is on the screen
            AppResult[] result = app.Query("GoogleButton");
            Assert.IsTrue(result.Any());

            // test that the label is on the screen
            result = app.Query("MessageLabel");
            Assert.IsTrue(result.Any());

            // test that the label contains the proper text
            Assert.AreEqual(result[0].Text, "Sign Up With");
        }

        [Test]
        public void TestThatSignInScreenElementsExistOnPage()
        {
            // navigate to the sign in page
            app.Tap("SignInButton");

            // Wait for the Google Button to appear on screen
            app.WaitForElement("GoogleButton");

            // test that the Google sign in button is on the screen
            AppResult[] result = app.Query("GoogleButton");
            Assert.IsTrue(result.Any());

            // test that the label is on the screen
            result = app.Query("MessageLabel");
            Assert.IsTrue(result.Any());

            // test that the label contains the proper text
            Assert.AreEqual(result[0].Text, "Sign In With");
        }

        [Test] // Must sign in to application at least once to complete test
        public void TestThatUserIsTakenToBeverageSelectPageAfterSuccessfulSignIn()
        {
            // navigate to the sign up page
            app.Tap("SignInButton");

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // select account or enter credentials
            //app.TapCoordinates(690, 1300); // Taps behind Android Prompt Window

            app.WaitForElement("WelcomeLabel");

            // test that the welcoming label contains welcoming text
            app.WaitForElement("ContinueButton");

            // Tap the Continue Button
            app.Tap("ContinueButton");

            // Wait for the search field to be on the screen
            app.WaitForElement("searchBeverage");

            AppResult[] result = app.Query("searchBeverage");
            Assert.IsTrue(result.Any());
        }
        #endregion

        #region Tests That Cant Run
        //[Test] // This test cannot be run, Xamarin has no access to the Android UI Or Google Prompts
        //public void TestThatUserIsTakenOutsideOfAppToSelectExternalAccount()
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
        //    //app.WaitForElement("MainPage");

        //    // test that the sign up button is on the screen, as the user is sent back to the original screen
        //    //AppResult[] button = app.Query("btnSignUp");
        //    Assert.IsTrue(true);
        //}
        #endregion
    }
}