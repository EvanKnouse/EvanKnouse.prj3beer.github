using System.ComponentModel.DataAnnotations;

namespace prj3beer.Models
{
    public class Beverage
    {
        private const int defaultTemp = 4;

        [Key]
        [Required (ErrorMessage = "Beverage ID is Required")]
        private int bevId; 

        [Required (ErrorMessage = "Beverage Name is Required")]
        public string name;

        [Required (ErrorMessage = "Brand is Required")]
        public string brand;

        [Required(ErrorMessage = "Ideal Temperature is Required")]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        private double idealTemp;

        //Default Beverage Constructor
        public Beverage()
        {

        }

        public Beverage(int id, string sName, string sBrand, double dIdealTemp)
        {
            this.bevId = id;
            this.name = sName;
            this.brand = sBrand;
            this.IdealTemp = dIdealTemp;

            //If the idealTemp is set outside the allowed range, set it to default value
            if (dIdealTemp > 30 || dIdealTemp < -30)
            {
                this.idealTemp = defaultTemp;
            }
            else
            {
                this.idealTemp = dIdealTemp;
            }
        }

        public double IdealTemp { get => idealTemp; set => idealTemp = value; }
    }
}