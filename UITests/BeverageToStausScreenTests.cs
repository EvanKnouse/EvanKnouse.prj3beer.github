﻿using NUnit.Framework;
using prj3beer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Type = prj3beer.Models.Type;

namespace UITests
{
    [TestFixture(Platform.Android)]
    public class BeverageToStatusScreenTests
    {
        string apkPath = "D:\\virpc\\prj3beer\\prj3.beer\\prj3beer\\prj3beer.Android\\bin\\Debug\\com.companyname.prj3beer.apk";
        IApp app;
        Platform platform;

        public BeverageToStatusScreenTests(Platform platform)
        {
            this.platform = platform;
        }

        #region brand initializors
        public static string tooLong = new String('a', 200);
        public static string longBoundary = new String('a', 138);
        Beverage LargeCoorsLiteImage = new Beverage { BeverageID = 1, Name = "Great Western Radler", BrandID = 1, Type = Type.Light, Temperature = 3, ImageURL = "https://www.lcbo.com/content/dam/lcbo/products/906628.jpg/jcr:content/renditions/cq5dam.web.1280.1280.jpeg" };
        Beverage SmallCoorsLiteImage = new Beverage
        {
            BeverageID = 2,
            Name = "Great Western Brewery",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://pngimage.net/wp-content/uploads/2018/05/coors-light-bottle-png-3.png"
        };

        Beverage URLTooLarge = new Beverage
        {
            BeverageID = 2,
            Name = "Great Western Radler",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://" + tooLong + ".png"
        };

        Beverage URLLargeBoundary = new Beverage
        {
            BeverageID = 2,
            Name = "Great Western Radler",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://"  + longBoundary  + ".png"
        };

        Beverage InvalidURLBeverage = new Beverage
        {
            BeverageID = 2,
            Name = "Great Western Radler",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "HelloWorldTheEarthSaysHello"
        };
        #endregion

        //Brand --> x:automationID = brandName
        //BeverageName --> x:automationID = beverageName
        //BeverageImage --> x:automationID = beverageImage
        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();
            // tap on the hamburger menu
            app.TapCoordinates(150, 90);

        }

        public void selectABeverage(String searchBeverage)
        {
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
            app.EnterText("searchBeverage", searchBeverage.ToString());

            app.TapCoordinates(3,701);
        }

        public void notSelectingBeverage()
        {
            app.Tap("Status");
        }

        public void goToBeverageSelectScreen(String searchBeverage)
        {
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
            app.EnterText("searchBeverage", searchBeverage.ToString());
        }
        

        [Test]
        public void TestThatBeverageTemperatureIsDisplayedOnStatusScreen()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("currentTemperature");
            AppResult[] beverageDisplay = app.Query("currentTemperature");
            Assert.IsTrue(beverageDisplay.Any());
        }


        [Test]//700f is 200 pixels
        public void TestThatBelowMinBoundaryImageSizeIsScaledUp()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("beverageImage");
            AppResult[] queryImage = app.Query("beverageImage");
            if (queryImage[0].Rect.Width == 700f || queryImage[0].Rect.Height == 700f)
                Assert.IsTrue(true);
            else {
                Assert.AreEqual(700f, queryImage[0].Rect.Height);
                Assert.AreEqual(700f, queryImage[0].Rect.Width);
            }
        }

        [Test]
        public void TestThatAboveMaxBoundaryImageSizeIsResizedDown()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("beverageImage");
            AppResult[] queryImage = app.Query("beverageImage");
            if (queryImage[0].Rect.Width == 700f || queryImage[0].Rect.Height == 700f)
                Assert.IsTrue(true);
            else
                Assert.IsTrue(false);
        }

        /* Image is now always force set to 200 pixels
        [Test]
        public void TestThatImageProportioinIsCorrect()
        {
            var webImage = new Image
            {
                Source = ImageSource.FromUri(new Uri(SmallCoorsLiteImage.ImageURL.ToString()))
            };
            Assert.AreEqual(webImage.Width, mainDisplayInfoWidth * .8);
        }*/

        [Test]
        public void TestThatTappingABeverageMovesToStatusScreen()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("StatusPage");
            AppResult[] beverageDisplay = app.Query("StatusPage");

            Assert.IsTrue(beverageDisplay.Any());

        }

        [Test]
        public void TestThatSelectedBeverageNameIsDisplayed()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("beverageName");
            AppResult[] beverageDisplay = app.Query("beverageName");
            Assert.AreEqual("Great Western Radler", beverageDisplay[0].Text);
        }

        [Test]
        public void TestThatSelectedBrandNameIsDisplayed()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("brandName");

            AppResult[] beverageDisplay = app.Query("brandName");

            Assert.AreEqual("Great Western Brewing Company",beverageDisplay[0].Text);
        }


        [Test]
        public void TestThatBrandEntryLableIsOnStatusScreen()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("brandName");

            AppResult[] beverageDisplay = app.Query("brandName");

            Assert.IsTrue(beverageDisplay.Any());
        }


        [Test]
        public void TestThatbeverageNameIsOnStatusScreen()
        {
            selectABeverage("Great Western Radler");
            
            app.WaitForElement("beverageName");

            AppResult[] beverageDisplay = app.Query("beverageName");

            Assert.IsTrue(beverageDisplay.Any());
        }

        [Test]
        public void TestThatBeverageImageIsShownOnScreen()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("beverageImage");

            AppResult[] beverageDisplay = app.Query("beverageImage");

            Assert.IsTrue(beverageDisplay.Any());
        }


        [Test]
        public void TestThatbeverageNameCanNotBeEdited()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("beverageName");
            AppResult[] appResult = app.Query("beverageName");
            Assert.IsFalse(appResult[0].Enabled);
        }

        [Test]
        public void TestThatbrandNameCanNotBeEdited()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("brandName");
            AppResult[] appResult = app.Query("brandName");
            Assert.IsFalse(appResult[0].Enabled);
        }


        [Test]
        public void TestThatImageBoxCannotBeEdited()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("beverageImage");
            AppResult[] appResult = app.Query("beverageImage");
            Assert.IsFalse(appResult[0].Enabled);
        }



        #region Untestable tests

        /*
        [Test]
        public void TestThatDefaultImageIsDisplayedWhenNoImageIsSelected()
        {
            //notSelectingBeverage();
            selectABeverage("Rebellion Pear Beer");
            app.WaitForElement("beverageImage");
            AppResult[] imageDisplay = app.Query("beverageImage");

            

            Image testImage = new Image();
            testImage.Source = "placeholder_can";

            

            Assert.AreEqual(testImage.ToString(), imageDisplay[0]);//Do not know how to get the source...
        }

        */

        /*
        [Test]
        public void TestThatMovingToStatusScreenWithNoPreviousBeverageDispalysTheDefaultBeverageInformation()
        {
            notSelectingBeverage();

            app.WaitForElement("beverageName");

            AppResult[] beverageDisplay = app.Query("beverageName");

            Assert.AreEqual(beverageDisplay[0].Text, "No Beverage");

            beverageDisplay = app.Query("brandName");

            Assert.AreEqual(beverageDisplay[0].Text, "No Brand");

            //beverageDisplay = app.Query("beverageImage");
            //How to do the default imeage?
            //Assert.AreEqual(beverageDisplay[0].Rect.Width, 0);
            //Assert.AreEqual(beverageDisplay[0].Rect.Height, 0);

        }*/

        /*
        [Test]
        public void TestThatTheAppWillUsedTheImagePathForTheSelectedBeverageIfImagePathIsNotNull()
        {
            
            //notSelectingBeverage();
            //AppResult[] imageDisplay = app.Query("Image");

            //app.TapCoordinates(150, 90);
            //selectABeverage("Great Western Radler");

            selectABeverage("Great Western Radler");

            AppResult[] imageDisplayed = app.Query("Image");

            

            //Assert.AreNotEqual(imageDisplayed[0],);//Want to cocompate to image source?
        }
        */

        /*
        [Test]
        public void TestThatTappingNewBeverageOverwritesOtherBeverageOnStatusScreen()
        {
            selectABeverage("Great Western Radler");
            app.WaitForElement("beverageName");
            app.TapCoordinates(150, 90);
            notSelectingBeverage();
            app.WaitForElement("beverageName");
            app.TapCoordinates(150, 90);
            //selectABeverage("Great Western Pilsner"); //Cannot just select a beverage.. The search does not auto-clear. We overlap text
            app.WaitForElement("beverageName");
            AppResult[] beverageDisplay = app.Query("beverageName");

            Assert.AreEqual(beverageDisplay[0].Text, "Great Western Pilsner");

            beverageDisplay = app.Query("brandName");

            Assert.AreEqual(beverageDisplay[0].Text, "Great Western brewing Company");

        }
        */

        /* Image is now saved to local device storage, so if it is valid, it will be automatic through built-in functions
        [Test]
        public void TestThatImageURLIsSavedAsTheAppropriateImageFileType()
        {
            
            //On tap, set select, if saved image is false, new method to save image gotten from beverage url, Imaged save should become true

            Preference selected = new Preference { BeverageID = SmallCoorsLiteImage.BeverageID, Temperature = SmallCoorsLiteImage.Temperature };
            //string filePath = selected.ImagePath;
           // Image img = selected.savedImage;
           

            Assert.IsTrue(selected.ImageSaved());
        }*/

        /* Moving to the status page with no image selected does display default information now
        [Test]
        public void TestThatMovingToStatusScreenWithNoPreviousBeverageDoesNotDisplayAnyBeverageInformation()
        {
            notSelectingBeverage();

            AppResult[] beverageDisplay = app.Query("beverageName");

            Assert.AreEqual(beverageDisplay[0].Text, "");

            beverageDisplay = app.Query("brandName");

            Assert.AreEqual(beverageDisplay[0].Text, "");

            beverageDisplay = app.Query("beverageImage");

            Assert.AreEqual(beverageDisplay[0].Rect.Width, 0);
            Assert.AreEqual(beverageDisplay[0].Rect.Height, 0);

        }*/

        #endregion
    }


}