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

        #region brand initializors
        public static string tooLong = new String('a', 200);
        public static string longBoundary = new String('a', 138);
        Beverage LargeCoorsLiteImage = new Beverage { BeverageID = 1, Name = "Coors Light", BrandID = 1, Type = Type.Light, Temperature = 3, ImageURL = "https://www.lcbo.com/content/dam/lcbo/products/906628.jpg/jcr:content/renditions/cq5dam.web.1280.1280.jpeg" };
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
            Name = "Coors Light",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://" + tooLong + ".png"
        };

        Beverage URLLargeBoundary = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://"  + longBoundary  + ".png"
        };

        Beverage InvalidURLBeverage = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "HelloWorldTheEarthSaysHello"
        };
         #endregion

        double mainDisplayInfoWidth = DeviceDisplay.MainDisplayInfo.Width;
        double mainDisplayInfoHeight = DeviceDisplay.MainDisplayInfo.Height;


        //Brand --> x:automationID = brandLabel
        //BeverageName --> x:automationID = beverageLabel
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
            app.Tap(searchBeverage);
        }

        public void notSelectingBeverage()
        {
            app.Tap("Status Screen");
        }

        public void goToBeverageSelectScreen(String searchBeverage)
        {
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
            app.EnterText("searchBeverage", searchBeverage.ToString());
        }

        /// <summary>
        /// This method will be used to add coors light to the status page
        /// </summary>
        public void addCoorsLightToStatusPage()
        {
            selectABeverage("Coors Light");

            app.WaitForElement("beverageLabel");

            AppResult[] beverageDisplay = app.Query("beverageLabel");

            Assert.AreEqual(beverageDisplay[0].Text, "Coors Light");

            beverageDisplay = app.Query("brandLabel");

            Assert.AreEqual(beverageDisplay[0].Text, "Coors");

            beverageDisplay = app.Query("beverageImage");

            Assert.AreEqual(beverageDisplay[0].Rect.Width, mainDisplayInfoWidth * 0.7);
            Assert.AreEqual(beverageDisplay[0].Rect.Height, mainDisplayInfoHeight * 0.7);


        }

        [Test]
        public void TestThatBelowMinBoundaryImageSizeIsScaledUp()
        {
            var webImage = new Image
            {
                Source = ImageSource.FromUri(new Uri(SmallCoorsLiteImage.ImageURL.ToString()))
            };
            Assert.AreEqual(webImage.Width, mainDisplayInfoWidth* .7);
        }

        [Test]
        public void TestThatAboveMaxBoundaryImageSizeIsResizedDown()
        {
            var webImage = new Image
            {
            Source = ImageSource.FromUri(new Uri(LargeCoorsLiteImage.ImageURL.ToString()))
            };
            Assert.AreEqual(webImage.Width, mainDisplayInfoWidth* .7);
        }

        [Test]
        public void TestThatTappingABeverageMovesToStatusScreen()
        {
            selectABeverage("Coors Light");

            AppResult[] beverageDisplay = app.Query("StatusPage");

            Assert.IsTrue(beverageDisplay.Any());

        }

        [Test]
        public void TestThatMovingToStatusScreenWithNoPreviousBeverageDoesNotDisplayAnyBeverageInformation()
        {
            notSelectingBeverage();

            AppResult[] beverageDisplay = app.Query("beverageLabel");

            Assert.AreEqual(beverageDisplay[0].Text, "");

            beverageDisplay = app.Query("brandLabel");

            Assert.AreEqual(beverageDisplay[0].Text, "");

            beverageDisplay = app.Query("beverageImage");

            Assert.AreEqual(beverageDisplay[0].Rect.Width, 0);
            Assert.AreEqual(beverageDisplay[0].Rect.Height, 0);

        }

        [Test]
        public void TestThatTappingNewBeverageOverwritesOtherBeverageOnStatusScreen()
        {
            selectABeverage("Coors Light");
            selectABeverage("Great Western Pilsner");

            AppResult[] beverageDisplay = app.Query("beverageLabel");

            Assert.AreEqual(beverageDisplay[0].Text, "Great Western Pilsner");

            beverageDisplay = app.Query("brandLabel");

            Assert.AreEqual(beverageDisplay[0].Text, "Great Western brewing Company");

            beverageDisplay = app.Query("beverageImage");

            Assert.AreEqual(beverageDisplay[0].Rect.Width, mainDisplayInfoWidth * 0.7);
            Assert.AreEqual(beverageDisplay[0].Rect.Height, mainDisplayInfoHeight * 0.7);
        }

        [Test]
        public void TestThatBeverageTemperatureIsDisplayedOnStatusScreen()
        {
            selectABeverage("Coors Light");
            AppResult[] beverageDisplay = app.Query("currentTarget");
            Assert.AreEqual(2, beverageDisplay[0].Text);
        }

        [Test]
        public void TestThatSelectedBeverageNameIsDisplayed()
        {
            selectABeverage("Coors Light");
            AppResult[] beverageDisplay = app.Query("currentTarget");
            Assert.AreEqual(2, beverageDisplay[0].Text);
        }

        [Test]
        public void TestThatSelectedBrandNameIsDisplayed()
        {
            selectABeverage("Coors Light");
            app.WaitForElement("brandLabel");

            AppResult[] beverageDisplay = app.Query("brandLabel");

            Assert.AreEqual(beverageDisplay[0].Text, "Coors");
        }


        [Test]
        public void TestThatBrandEntryLableIsOnStatusScreen()
        {
            selectABeverage("Coors Light");
            app.WaitForElement("brandLabel");

            AppResult[] beverageDisplay = app.Query("brandLabel");

            Assert.IsTrue(beverageDisplay.Any());
        }


        [Test]
        public void TestThatBeveragelabelIsOnStatusScreen()
        {
            selectABeverage("Coors Light");
            app.WaitForElement("beverageLabel");

            AppResult[] beverageDisplay = app.Query("beverageLabel");

            Assert.IsTrue(beverageDisplay.Any());
        }

        [Test]
        public void TestThatBeverageImageIsShownOnScreen()
        {
            selectABeverage("Coors Light");
            app.WaitForElement("beverageImage");

            AppResult[] beverageDisplay = app.Query("beverageImage");

            Assert.IsTrue(beverageDisplay.Any());
        }


        [Test]
        public void TestThatBeverageLabelCanNotBeEdited()
        {
            selectABeverage("Coors Light");

            AppResult[] appResult = app.Query("beverageLabel");
            Assert.IsFalse(appResult[0].Enabled);
        }

        [Test]
        public void TestThatBrandLabelCanNotBeEdited()
        {
            selectABeverage("Coors Light");

            AppResult[] appResult = app.Query("brandLabel");
            Assert.IsFalse(appResult[0].Enabled);
        }


        [Test]
        public void TestThatImageBoxCannotBeEdited()
        {
            selectABeverage("Coors Light");

            AppResult[] appResult = app.Query("beverageImage");
            Assert.IsFalse(appResult[0].Enabled);
        }


        [Test]
        public void TestThatImageURLIsSavedAsTheAppropriateImageFileType()
        {
            
            //On tap, set select, if saved image is false, new method to save image gotten from beverage url, Imaged save should become true

            Preference selected = new Preference { BeverageID = SmallCoorsLiteImage.BeverageID, Temperature = SmallCoorsLiteImage.Temperature };
            string filePath = selected.ImagePath;

           

            Assert.IsTrue(selected.ImageSaved());

        }

    }
    

}