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
            Name = "Coors Light",
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

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            app = ConfigureApp.Android.ApkFile(apkPath).StartApp();
            // tap on the hamburger menu
            app.TapCoordinates(150, 90);
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
        }

        /// <summary>
        /// This method will be used to add coors light to the status page
        /// </summary>
        public void addCoorsLightToStatusPage()
        {
            
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

        }

        [Test]
        public void TestThatMovingToStatusScreenWithNoPreviousBeverageDoesNotDisplayAnyBeverageInformation()
        {

        }

        [Test]
        public void TestThatTappingNewBeverageOverwritesOtherBeverageOnStatusScreen()
        {

        }

        [Test]
        public void TestThatBeverageTemperatureIsDisplayedOnStatusScreen()
        {

        }

        [Test]
        public void TestThatSelectedBeverageNameIsDisplayed()
        {

        }

        [Test]
        public void TestThatSelectedBrandNameIsDisplayed()
        {

        }

        [Test]
        public void TestThatSelectedTypeNameIsDisplayed()
        {

        }

        [Test]
        public void TestThatBrandEntryLableIsOnStatusScreen()
        {

        }

        [Test]
        public void TestThatTypeLabelIsOnStatusScreen()
        {

        }

        [Test]
        public void TestThatBeveragelabelIsOnStatusScreen()
        {

        }

        [Test]
        public void TestThatTestThatABeverageCanBeTapped()
        {

        }

        [Test]
        public void TestThatSelectingABeverageDisplaysAllCorrectData()
        {

        }

        [Test]
        public void TestThatBeverageLabelCanNotBeEdited()
        {

        }

        [Test]
        public void TestThatBrandLabelCanNotBeEdited()
        {

        }

        [Test]
        public void TestThatTypeLabelCanNotBeEdited()
        {

        }

        [Test]
        public void TestThatImageBoxCannotBeEdited()
        {

        }

        [Test]
        public void TestThatBeverageImageIsShownOnScren()
        {

        }

    }
    

}