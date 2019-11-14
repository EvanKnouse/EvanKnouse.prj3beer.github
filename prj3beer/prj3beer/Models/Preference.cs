using System;
using System.Collections.Generic;
using System.Text;

namespace prj3beer.Models
{
    class Preference
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