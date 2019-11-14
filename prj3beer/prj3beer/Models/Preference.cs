using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;

namespace prj3beer.Models
{
    public class Preference
    {
        #region Attributes
        Beverage prefBev;
        int faveTemp;
        #endregion

        #region Constructors
        public Preference()
        {

        }

        public Preference(Beverage prefBev, int faveTemp)
        {

        }
        #endregion

        [Required]
        #region Properties
        public Beverage PrefBev
        {
            get
            {
                return prefBev;
            }
            set
            {
                prefBev = value;
            }
        }

        [Required]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        public int FaveTemp
        {
            get
            {
                return faveTemp;
            }
            set
            {
                faveTemp = value;
            }
        }
        #endregion


    }
}