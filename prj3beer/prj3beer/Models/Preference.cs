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
        [Required]
        private int prefId;

        [Required]
        int faveTemp;

        [ForeignKey("bevId")]
        Beverage prefBev;

       
        #endregion

        #region Constructors
        public Preference()
        {

        }

        public Preference(int prefId, Beverage prefBev, int faveTemp)
        {
            this.prefId = prefId;
            this.prefBev = prefBev;
            this.faveTemp = faveTemp;
        }
        #endregion

        
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