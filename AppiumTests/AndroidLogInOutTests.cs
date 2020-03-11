using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppiumTests
{
    [TestFixture()]
    public class AndroidLogInOutTests
    {
        readonly string apkpath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";

        private AndroidDriver<AndroidElement> driver;

        [SetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("DeviceName", "Android Emulator");
            capabilities.AddAdditionalCapability("app",apkpath);
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.BrowserName, "");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
        }

        public void FacebookLogin()
        {
            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            AndroidElement androidElement = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(androidElement);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Tap Sign in 
            androidElement = driver.FindElementByXPath("//android.widget.TextView[@text='Sign In']");
            action = touchAction.Tap(androidElement);
            action.Perform();
            action.Cancel();

            // Tap Facebook


            // Complete Sign In

            // IF Continue Exists?

        }

        [Test]
        public void TestThatUserCanSignInWithFacebookFromBevSelect()
        {
            FacebookLogin();

            // Check If On Beverage Select Screen
        }

        [Test]
        public void TestThatUserCanSignInWithFacebookFromStatus()
        {
            FacebookLogin();

            // Check If On Status Screen
        }

        [Test]
        public void TestThatSignOutButtonAppearsIfUserIsSignedIn()
        {
            FacebookLogin();

            // Access Menu

            // Check If Sign Out Exists
            // Assert True

        }






    }
}
