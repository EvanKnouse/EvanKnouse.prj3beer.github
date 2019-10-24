namespace prj3beer.Services
{
    public class Beverage
    {
        public String name;
        public String brand;
        private int idealTemp;
        public int faveTemp;
        private const int defaultTemp = 4;

        //Default Beverage Constructor
        public Beverage()
        {

        }

        public Beverage(String sName, String sBrand, int nIdealTemp)
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
