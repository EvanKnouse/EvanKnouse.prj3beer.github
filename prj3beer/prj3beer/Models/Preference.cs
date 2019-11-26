using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows;

namespace prj3beer.Models
{
    public class Preference
    {
        #region Attributes
        [Key]
        [Required(ErrorMessage = "ID is required")]
        private int prefId;

        [Required(ErrorMessage = "Favourite temperature is required")]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        double faveTemp;

        [ForeignKey("bevId")]
        [Required(ErrorMessage = "Beverage object is required")]
        Beverage prefBev;
        #endregion

        #region Constructors
        public Preference()
        {

        }

        public Preference(int prefId, Beverage prefBev, double faveTemp)
        {
            this.prefId = prefId;
            this.prefBev = prefBev;
            this.faveTemp = faveTemp;
        }
        #endregion

        #region Properties
        public Beverage PrefBev { get; set; }


        public double FaveTemp { get; set; }
        #endregion
    }
}