namespace prj3beerAndroid
{
    public class Beverage
    {
        public string name;
        public string brand;
        private int idealTemp;
        public int faveTemp;
        private const int defaultTemp = 4;

        //Default Beverage Constructor
        public Beverage()
        {

        }

        //Beverage constructor - takes in and sets the beverage's name, brand name, and ideal temperature (from database)
        public Beverage(string sName, string sBrand, int nIdealTemp)
        {
            this.name = sName;
            this.brand = sBrand;
            this.idealTemp = nIdealTemp;

            //If the idealTemp is set outside the allowed range, set it to default value
            if (nIdealTemp > 30 || nIdealTemp < -30)
            {
                this.idealTemp = defaultTemp;
            }
            else
            {
                this.idealTemp = nIdealTemp;
            }
        }

        public int getIdealTemp()
        {
            return this.idealTemp;
        }
    }
}