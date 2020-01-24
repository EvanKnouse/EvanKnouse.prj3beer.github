using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Type = prj3beer.Models.Type;
using Xamarin.Forms;
using prj3beer;

namespace nUnitTests
{
    class ImageTests
    {
       public static string tooLong = new String('a', 200);
        public static string longBoundary = new String('a', 138);
        IList<ValidationResult> errors;
        IApp app;
        Platform platform;
        #region Initializers

        Beverage LargeCoorsLiteImage = new Beverage { BeverageID = 1, Name = "Coors Light", Brand = new Brand() { brandID = 1,brandName = "Coors"}, Type = Type.Light, Temperature = 3, ImageURL= "https://www.lcbo.com/content/dam/lcbo/products/906628.jpg/jcr:content/renditions/cq5dam.web.1280.1280.jpeg"};
        Beverage SmallCoorsLiteImage = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            Brand = new Brand() { brandID = 1, brandName = "Coors" },
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://pngimage.net/wp-content/uploads/2018/05/coors-light-bottle-png-3.png"
        };

        Beverage URLTooLarge = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            Brand = new Brand() { brandID = 1, brandName = "Coors" },
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://" + tooLong + ".png"
        };
        Beverage URLLargeBoundary = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            Brand = new Brand() { brandID = 1, brandName = "Coors" },
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "https://" + longBoundary + ".png"
        };

        Beverage InvalidURLBeverage = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            Brand = new Brand() { brandID = 1, brandName = "Coors" },
            Type = Type.Light,
            Temperature = 3,
            ImageURL = "HelloWorldTheEarthSaysHello"
        };

        Beverage noImagebeverage = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            Brand = new Brand() { brandID = 1, brandName = "Coors" },
            Type = Type.Light,
            Temperature = 3,
            ImageURL = ""
        };


        #endregion

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
            App = ConfigureApp.Android.ApkFile(apkPath).StartApp();
            // tap on the hamburger menu
            app.TapCoordinates(150, 90);
            // tap to navigate to the beverage select screen
            app.Tap("Beverage Select");
        }

        [Test]
        public void TestThatMaxBoundaryURLImageSizeIsValid()
        {
            errors = ValidationHelper.Validate(LargeCoorsLiteImage);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatAboveMaxURLBoundaryImageSizeIsInvalid()
        {
            errors = ValidationHelper.Validate(URLTooLarge);
            Assert.IsTrue(errors.Count > 0);
        }


        [Test]
        public void TestThatNoImageIsStilLValid()
        {
            errors = ValidationHelper.Validate(noImagebeverage);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatImageURLIsValid()
        {
            errors = ValidationHelper.Validate(LargeCoorsLiteImage);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatInvalidURLIsNotSavedToAPI()
        {
            errors = ValidationHelper.Validate(InvalidURLBeverage);
            Assert.IsTrue(errors.Count > 0);
        }


    }
}
