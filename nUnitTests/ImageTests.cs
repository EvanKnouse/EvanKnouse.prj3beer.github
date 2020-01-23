using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Models;
using NUnit.Framework;
using System.Collections.Generic;


namespace nUnitTests
{
    class ImageTests
    {
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
            ImageURL = "https://pngimage.net/wp-content/uploads/2018/05/coors-light-bottle-png-3.png"
        };

#endregion

        [Test]
        public void TestThatMaxBoundaryURLImageSizeIsValid()
        {

        }

        [Test]
        public void TestThatAboveMaxURLBoundaryImageSizeIsInvalid()
        {

        }

        [Test]
        public void TestThatBelowMinBoundaryImageSizeIsScaledUp()
        {

        }

        [Test]
        public void TestThatAboveMaxBoundaryImageSizeIsResizedDown()
        {

        }

        [Test]
        public void TestThatImageURLIsValid()
        {

        }

        [Test]
        public void TestThatInvalidURLIsNotSavedToAPI()
        {

        }


    }
}
