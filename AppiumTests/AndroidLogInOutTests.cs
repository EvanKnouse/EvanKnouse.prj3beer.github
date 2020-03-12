using NUnit.Framework;
using OpenQA.Selenium;
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
        private RemoteWebDriver webDriver;

        AppiumOptions capabilities = new AppiumOptions();
        
        [SetUp]
        public void BeforeAll()
        {
            capabilities.AddAdditionalCapability("DeviceName", "Android Emulator");
            capabilities.AddAdditionalCapability("app", apkpath);
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.BrowserName, "");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            webDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
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
            Thread.Sleep(1000);

            // Tap Facebook
            androidElement = driver.FindElementByXPath("//android.widget.Button[@text='FACEBOOK']");
            action = touchAction.Tap(androidElement);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Complete Sign In
            //Container for the web element inside Facebook's webview
            IWebElement webElement;
            Thread.Sleep(4000);

            // Find Email field
            try
            {
                //androidElement = driver.FindElementByXPath("//android.widget.EditText[@resource-id='m_login_email']");
                webElement = driver.FindElementByXPath("//android.widget.EditText[@resource-id='m_login_email']");

                // If email field exists,
                if (webElement.Displayed)
                {
                    //Enter text into email field
                    webElement.SendKeys("dick_bojwzxn_richards@tfbnw.net");

                    //Find the password field
                    webElement = driver.FindElementByXPath("//android.widget.EditText[@resource-id='m_login_password']");

                    //Enter password into password field
                    webElement.SendKeys("Prj3beer##");
                    Thread.Sleep(2000);

                    //Find and tap the Log In button
                    webElement = driver.FindElementByClassName("android.widget.Button");
                    action = touchAction.Tap(webElement);
                    action.Perform();
                    action.Cancel();
                    Thread.Sleep(3000);
                }
            }
            catch(Exception){}

            // If Continue Exists?
            webElement = driver.FindElementByClassName("android.widget.Button");
            action = touchAction.Tap(webElement);
            action.Perform();
            action.Cancel();
        }

        [Test]
        public void TestThatUserCanSignInWithFacebookFromBevSelect()
        {
            FacebookLogin();

            Thread.Sleep(2000);
            // Check If On Beverage Select screen
            // The search bar only Beverage Select screen
            AndroidElement element = driver.FindElementByClassName("android.widget.SearchView");
            // If the element is displayed, we are on the Beverage Select screen
            Assert.IsTrue(element.Displayed);
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
