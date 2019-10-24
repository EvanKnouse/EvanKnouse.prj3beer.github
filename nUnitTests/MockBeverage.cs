using System;
using System.Collections.Generic;
using System.Text;

namespace nUnitTests
{
    public class MockBeverage
    {
        public String name;
        public String brand;
        public int idealTemp;
        public int faveTemp;

        public MockBeverage(String sName, String sBrand, int nIdealTemp)
        {
            this.name = sName;
            this.brand = sBrand;
            this.idealTemp = nIdealTemp;
        }
    }
}
