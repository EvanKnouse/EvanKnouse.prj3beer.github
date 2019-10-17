using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace UITests
{
    public class MockScreen
    {
        public static Label lblTargetTemp;

        public static void Main(String[] args)
        {
            //lblTargetTemp = new Label();
        }

        public static void displayTargetTemp(MockBeverage mockBev)
        {
            //lblTargetTemp = new Label { Text = mockBev.idealTemp.ToString() };
        }
    }
}
