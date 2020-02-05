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
        //[DefaultValue("")]
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
            if (ImagePath == "" || ImagePath == "placeholder_can" || ImagePath==null)
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
            image.Source = "placeholder_can";
            if (ImageSaved() == false)
            {
                if (imageURL.Length > 0)
                {


                    //Uri uriImage = new Uri(imageURL);
                    Uri uriImage = new Uri("https://uc8f123a0359d0bd1edfb681c74a.previews.dropboxusercontent.com/p/thumb/AAvlTVy8L0Fx5hKDKYFZLSu0nPVVO8AQTOa2d5RC7ZE-lnaTkeEbQIyuQmuE0Ax88CTtxaYx2_3SHQhuOe69WjC0UB5sgwoPeR2c04-YIHDN044qdZhkaAsBO1R0dLoNBda7Ey2FzC5bXCRMt4zW7x23cpyczcVjYim4-d923-JxkL8DxriJ5aGYkEJPVOzjwUOw_ktjRVtBfmnEqD8Zo0J0jZsh-se6QlirWl3JNnbp6dgje236hJd-DUItDNgCT4C3l7xOLwPOn9lHIYNnD25Pq5dQiMaxigKcnjteSAILFjRoRbV0Il23U0o9xXgD3QYSHqzqUXyHDOPTlI-V7jy6z6aiFX78h3ZZnmg-WsXOBUJ99SHvKojll3KXNiVXpZj3XrPBX3HN3tI6nPzd_BnV/p.png?fv_content=true&size_mode=5");

                    image.Source = new UriImageSource
                    {
                        Uri = uriImage,
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(7, 0, 0, 0)
                    };
                }

            }
            return image;

        }
    }
}
 