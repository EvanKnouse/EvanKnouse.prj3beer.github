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
        //private RemoteWebDriver webDriver;

        AppiumOptions capabilities = new AppiumOptions();

        [SetUp]
        public void BeforeAll()
        {
            capabilities.AddAdditionalCapability("DeviceName", "Android Emulator");
            capabilities.AddAdditionalCapability("app", apkpath);
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.BrowserName, "");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            //webDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
        }

        /// <summary>
        /// This method will select "Great Western Pilsner" from the beverage select,
        /// and send you to the status screen.
        /// </summary>
        private void SelectBeverage()
        {
            TouchAction touchAction = new TouchAction(driver);

            Thread.Sleep(500);

            //AndroidElement element = driver.FindElementByAndroidUIAutomator("new UiSelector().textContains(\"Please enter a beverage, type or brand!!\");");
            AndroidElement element = driver.FindElementByClassName("android.widget.EditText");

            element.SendKeys("Great");

            Thread.Sleep(500);

            //element = driver.FindElementByAndroidUIAutomator("new UiSelector().textContains(\"Great Western Pilsner\");");
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Great Western Pilsner']");

            Thread.Sleep(500);

            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();

            Thread.Sleep(500);
        }

        /// <summary>
        /// This method will sign a user IN to google
        /// </summary>
        public void GoogleLogin()
        {
            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            AndroidElement element = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(2000);

            // Tap Sign in 
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign In']");
            action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(2000);

            // Tap Google
            element = driver.FindElementByXPath("//android.widget.Button[@text='GOOGLE']");
            action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(6000);

            // Find all the ListViews On The Page
            var elements = driver.FindElementsByClassName("//android.widget.LinearLayout[@resource-id='com.google.android.gms:id/container']");
            //var elements = driver.FindElementsByClassName("android.widget.ListView");

            // The first List View Element is The Button We Want To Tap
            element = elements.ElementAt(0);
            // So I'm Gonna Tap It. 
            action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// This method will sign a user IN to facebook
        /// </summary>
        public void FacebookLogin()
        {
            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            AndroidElement element = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Tap Sign in 
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign In']");
            action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Tap Facebook
            element = driver.FindElementByXPath("//android.widget.Button[@text='FACEBOOK']");
            action = touchAction.Tap(element);
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
            catch (Exception) { }

            Thread.Sleep(2000);
            // If Continue Exists?
            webElement = driver.FindElementByClassName("android.widget.Button");
            action = touchAction.Tap(webElement);
            action.Perform();
            action.Cancel();

            Thread.Sleep(6000);
           
        }

        /// <summary>
        /// This method will Sign a User OUT of Facebook
        /// </summary>
        public void FacebookLogout()
        {
            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            AndroidElement element = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Tap Sign out 
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign Out']");
            action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Tap Yes
            element = driver.FindElementByXPath("//android.widget.Button[@text='YES']");
            action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestThatUserCanSignInWithFacebookFromBevSelect()
        {
            FacebookLogin();

            Thread.Sleep(2000);
            // Check If On Beverage Select screen
            // The search bar only Beverage Select screen
            AndroidElement element = driver.FindElementByClassName("android.widget.SearchView");
            //AndroidElement element = driver.FindElementByAccessibilityId("BeverageSelectPage");

            // If the element is displayed, we are on the Beverage Select screen
            Assert.IsTrue(element.Displayed);
            
        }

        [Test]
        public void TestThatUserCanSignInWithFacebookFromStatus()
        {
            SelectBeverage();

            FacebookLogin();

            Thread.Sleep(2000);
            // Check If On Beverage Select screen
            // The search bar only Beverage Select screen
            AndroidElement element = driver.FindElementByAccessibilityId("StatusPage");
            // If the element is displayed, we are on the Beverage Select screen
            Assert.IsTrue(element.Displayed);
        }

        [Test]
        public void TestThatSignInButtonAppearsIfUserIsSignedOut()
        {
            Thread.Sleep(2000);
            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            AndroidElement element = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();

            Thread.Sleep(1000);

            // Find Sign in 
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign In']");

            Thread.Sleep(1000);
            // Check If Sign Out Exists
            // Assert True
            Assert.IsTrue(element.Displayed);


            Thread.Sleep(1000);

            Assert.Throws<OpenQA.Selenium.NoSuchElementException>(()=>element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign Out']"));
        }


        [Test]
        public void TestThatSignOutButtonAppearsIfUserIsSignedIn()
        {
            FacebookLogin();

            Thread.Sleep(2000);
            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            AndroidElement element = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();

            Thread.Sleep(1000);

            // Find Sign in 
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign Out']");

            Thread.Sleep(1000);
            // Check If Sign Out Exists
            // Assert True
            Assert.IsTrue(element.Displayed);

            Thread.Sleep(1000);

            Assert.Throws<OpenQA.Selenium.NoSuchElementException>(() => element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign In']"));
        }

        [Test]
        public void TestThatUserCanSignInOnSelectScreenAndOutOnStatusScreen()
        {
            FacebookLogin();

            Thread.Sleep(4000);

            SelectBeverage();

            FacebookLogout();

            // The search bar only Beverage Select screen
            AndroidElement element = driver.FindElementByAccessibilityId("StatusPage");

            // If the element is displayed, we are on the Beverage Select screen
            Assert.IsTrue(element.Displayed);

            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            element = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Find Sign in 
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign In']");
        }

        [Test]
        public void TestUserCanSignInWithGoogle()
        {
            GoogleLogin();

            // Access Menu
            TouchAction touchAction = new TouchAction(driver);
            AndroidElement element = driver.FindElementByXPath("//android.widget.ImageView[@content-desc='More options']");
            var action = touchAction.Tap(element);
            action.Perform();
            action.Cancel();
            Thread.Sleep(1000);

            // Find Sign Out
            element = driver.FindElementByXPath("//android.widget.TextView[@text='Sign Out']");

            Assert.IsTrue(element.Displayed);
        }
    }
}
