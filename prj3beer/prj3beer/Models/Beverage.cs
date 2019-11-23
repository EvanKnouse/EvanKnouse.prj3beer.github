using System.ComponentModel.DataAnnotations;

namespace prj3beer.Models
{
    public class Beverage
    {
        private const int defaultTemp = 4;

        [Key]
        [Required]
        private int bevId; 

        [Required]
        public string name;

        [Required]
        public string brand;

        [Required]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        private double idealTemp;

        //Default Beverage Constructor
        public Beverage()
        {

        }

        public Beverage(int id, string sName, string sBrand, int nIdealTemp)
        {
            this.bevId = id;
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

        [Required]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        public double IdealTemp { get; set; }
    }
}