using System;
using System.Collections.Generic;
using System.Text;

namespace nUnitTests
{
    public class MockBeverage
    {
        public String name;
        public String brand;
        private int idealTemp;
        public int faveTemp;
        private const int defaultTemp = 4;
        

        public MockBeverage(String sName, String sBrand, int nIdealTemp)
        {
            this.name = sName;
            this.brand = sBrand;

            //If the idealTemp is set outside the allowed range, set it to default value
            if(nIdealTemp > 30 || nIdealTemp < -30)
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
