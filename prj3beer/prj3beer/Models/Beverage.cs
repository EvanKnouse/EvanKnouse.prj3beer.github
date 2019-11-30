using System.ComponentModel.DataAnnotations;

namespace prj3beer.Models
{
    public class Beverage
    {
        [Key]
        [Required (ErrorMessage = "Beverage ID is Required")]
        public int BevID { get; set; }

        [Required (ErrorMessage = "Beverage Name is Required")]
        public string Name { get; set; }

        //[Required (ErrorMessage = "Brand is Required")]
        //public string brand;

        [Required(ErrorMessage = "Ideal Temperature is Required")]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        public double IdealTemp { get; set; }
    }
}