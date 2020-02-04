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

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class SignUpTests
    {
        //Instead of querying on any (in case its empty) just make sure it contains the correct number of beverages
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
            // tap on the hamburger menu
            //app.TapCoordinates(150, 90);
            // should not have to leave the sign in/up page
            //app.Tap("Page");
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

        [Test]
        public void TestThatUserIsTakenToLandingPageAfterSuccessfulSignUp()
        {
            // navigate to the sign up page
            app.Tap("SignUpButton");

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // select account or enter credentials

            // allow permissions

            // Wait for the search field to be on the screen
            app.WaitForElement("searchBeverage");

            // test that the modal is visible
            AppResult[] modal = app.Query("modal");
            Assert.IsTrue(modal.Any());
        }

        [Test]
        public void TestThatUserIsTakenOutsideOfAppToSelectExternalAccount()
        {
            // navigate to the sign up page
            app.Tap("SignUpButton");

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // test that we're on the page, somehow

            // select account or enter credentials?
            //app.TapCoordinates(x, y);
            // allow permissions?
            //app.Tap("Allow");
        }

        [Test]
        public void TestUserCanAllowAppToAccessProfileInformation()
        {
            // navigate to the sign up page
            app.Tap("SignUpButton");

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // select account or enter credentials
            //app.TapCoordinates(x, y);

            // test that the allow button is on the permissions screen
            AppResult[] allow = app.Query("Allow");
            Assert.IsTrue(allow.Any());
        }

        [Test]
        public void TestThatUsersNameIsDisplayedAfterSigningIn()
        {
            // navigate to the sign up page
            app.Tap("SignInButton");

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            //app.WaitForElement("Joel Sipes");
            //Thread.Sleep(5000);

            // select account or enter credentials
            //app.TapCoordinates(690, 1300);

            app.WaitForElement("WelcomeLabel");

            // test that the welcoming label is there
            AppResult[] results = app.Query("WelcomeLabel");
            Assert.IsTrue(results.Any());

            // test that the welcoming label contains welcoming text
            Assert.AreEqual(results[0].Text, "Welcome back Levis Media");
        }
        [Test]
        public void TestThatUsersNameIsDisplayedAfterSigningUp()
        {
            // navigate to the sign up page
            app.Tap("SignUpButton");

            // navigate to enter external credentials
            app.Tap("GoogleButton");

            // select account or enter credentials
            app.TapCoordinates(690, 1300);

            // test that the welcoming label is there
            AppResult[] results = app.Query("WelcomeLabel");
            Assert.IsTrue(results.Any());

            // test that the welcoming label contains welcoming text
            Assert.AreEqual(results[0].Text, "Welcome Levis Media");
        }

        [Test]
        public void TestThatUserIsTakenBackToSignUpPageAfterCancellingSignUp()
        {
            // navigate to the sign up page
            app.Tap("SignUpButton");

            // navigate to enter external credentials
            app.Tap("SignInButton");

            // select account or enter credentials
            //app.TapCoordinates(x, y);

            // hit the cancel button
            //app.TapCoordinates(x, y);

            // wait for the main page to be displayed
            app.WaitForElement("MainPage");

            // test that the sign up button is on the screen, as the user is sent back to the original screen
            AppResult[] button = app.Query("btnSignUp");
            Assert.IsTrue(button.Any());
        }
        #endregion
    }
}