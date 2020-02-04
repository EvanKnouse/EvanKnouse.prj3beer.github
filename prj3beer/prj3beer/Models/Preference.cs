using prj3beer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Windows;
using Xamarin.Forms;
using System.Drawing;

namespace prj3beer.Models
{
    public class Preference
    {
        BeerContext context;


        #region Attributes
        [Key]
        [Required(ErrorMessage = "ID is required")]
        public int BeverageID { get; set; }

        [Required(ErrorMessage = "Favourite temperature is required")]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        public double? Temperature { get; set; }

        //[ForeignKey("bevId")]
        //[Required(ErrorMessage = "Beverage object is required")]
        //Beverage prefBev;

        [DefaultValue("placeholder_can")]
        public string ImagePath { get; set; }

        //public Image savedImage { get; set; }

        #endregion


    

        public Preference()
        {
            this.BeverageID = BeverageID;
            context = new BeerContext();
            Beverage SelectedBeverage = (context.Beverage.Find(BeverageID));

            SaveImage(SelectedBeverage.ImageURL);
        }

        public Preference(int bevID)
        {
            this.BeverageID = bevID;
            context = new BeerContext();
            Beverage SelectedBeverage = (context.Beverage.Find(BeverageID));

            SaveImage(SelectedBeverage.ImageURL);
        }

        //public Preference(int prefId, Beverage prefBev, double faveTemp)
        //{
        //    this.prefId = prefId;
        //    this.prefBev = prefBev;
        //    this.faveTemp = faveTemp;
        //}


        /// <summary>
        /// This method will set a bool value to true when the image gets saved locally
        /// </summary>
        /// <returns></returns>
        public bool ImageSaved()
        {
            //throw new NotImplementedException();
            //if (ImagePath == null)
            if (ImagePath.Count() > 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Image SaveImage(String imageURL)
        {
            Image image = new Image();
            if (ImageSaved() == false)
            {
                if (imageURL.Length > 0)
                {

                    
                    Uri uriImage = new Uri(imageURL);

                    //Image img = new Image();

                    /*
                    img.Source = ImageSource.FromUri(uriImage);

                    byte[] imageBytes;
                    imageBytes = Convert.FromBase64String(img);

                    ImagePath = uriImage.AbsoluteUri;
                    */




                    image.Source = new UriImageSource
                    {
                        Uri = uriImage,
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(7, 0, 0, 0)
                    };
                    return image;
                }

            }
            return null;

        }
        //Things seen on the internet:
        /*  
         *  var webImage = new Image { 
            Source = ImageSource.FromUri(
            new Uri("https://xamarin.com/content/images/pages/forms/example-app.png")
            ) };
        *//*
            webImage.Source = "https://xamarin.com/content/images/pages/forms/example-app.png";
         */
    }
}
 