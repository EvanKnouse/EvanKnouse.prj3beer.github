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
using System.IO;
using System.Linq;

namespace nUnitTests
{
    class ImageTests
    {
       public static string tooLong = new String('a', 200);
        public static string longBoundary = new String('a', 138);
        IList<ValidationResult> errors;
       
        #region Initializers

        Beverage LargeCoorsLiteImage = new Beverage { BeverageID = 1, Name = "Coors Light", BrandID = 1, Type = Type.Light, Temperature = 3, ImageURL= "https://www.lcbo.com/content/dam/lcbo/products/906628.jpg/jcr:content/renditions/cq5dam.web.1280.1280.jpeg"};
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
            ImageURL = "https://" + longBoundary + ".png"
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

        Beverage noImagebeverage = new Beverage
        {
            BeverageID = 2,
            Name = "Coors Light",
            BrandID = 1,
            Type = Type.Light,
            Temperature = 3,
            ImageURL = ""
        };


        #endregion

        /* No longer a boundry that needs to be tested
        [Test]
        public void TestThatMaxBoundaryURLImageSizeIsValid()
        {
            errors = ValidationHelper.Validate(URLLargeBoundary);
            Assert.IsTrue(errors.Count == 0);
        }*/

        /* Image URLs can be any size
        [Test]
        public void TestThatAboveMaxURLBoundaryImageSizeIsInvalid()
        {
            errors = ValidationHelper.Validate(URLTooLarge);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(errors[0].ToString(), "Image URL is too large");
        }*/


        [Test]
        public void TestThatNoImageIsStilLValid()
        {
            errors = ValidationHelper.Validate(noImagebeverage);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatImageURLIsValid()
        {
            errors = ValidationHelper.Validate(SmallCoorsLiteImage);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatInvalidURLIsCaught()
        {
            errors = ValidationHelper.Validate(InvalidURLBeverage);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(errors[0].ToString(), "Image URL is not actually an image URL");
        }


        /* Was changed to a UI Test
        [Test]
        public void TestThatDefaultImageIsUsedWhenNoCustomImageIsSavedAndURLIsInvalid()
        {
            Preference selected = new Preference { BeverageID = noImagebeverage.BeverageID, Temperature = noImagebeverage.Temperature };
            Assert.IsFalse(selected.ImageSaved());
            Image testImage = new Image();
            testImage.Source = "placeholder_can";
            Assert.AreEqual(selected.SaveImage(noImagebeverage.ImageURL), testImage);
            //Assert.AreEqual(selected.ImagePath, "../Images/placeholder_can.png");
        }
        */

    }
}
