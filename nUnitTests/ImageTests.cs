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

        [Test]
        public void TestThatMaxBoundaryURLImageSizeIsValid()
        {
            errors = ValidationHelper.Validate(URLLargeBoundary);
            Assert.IsTrue(errors.Count == 0);
        }

        [Test]
        public void TestThatAboveMaxURLBoundaryImageSizeIsInvalid()
        {
            errors = ValidationHelper.Validate(URLTooLarge);
            Assert.IsTrue(errors.Count > 0);
            Assert.AreEqual(errors[0].ToString(), "Image URL is too large");
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

        [Test]
        public void TestThatImageURLIsSavedAsTheAppropriateImageFileType()
        {
            bool found = false;
            errors = ValidationHelper.Validate(SmallCoorsLiteImage);
            Assert.IsTrue(errors.Count == 0);
            string filePath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var files = new DirectoryInfo(filePath)
                .GetFiles();

            foreach (var file in files)
            {
                if (file.Name.Equals(SmallCoorsLiteImage.Name + ".png")
                    || file.Name.Equals(SmallCoorsLiteImage.Name + ".jpg")
                    || file.Name.Equals(SmallCoorsLiteImage.Name + ".jpeg")
                    || file.Name.Equals(SmallCoorsLiteImage.Name + ".gif"))
                {
                    found = true;
                }
              
            }
            Assert.IsTrue(found);

        }

        //TestThatImagedoesNotGetRecalledWhenAccessingItAgain

    }
}
