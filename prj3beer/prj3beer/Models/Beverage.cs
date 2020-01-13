using System.ComponentModel.DataAnnotations;
using Xamarin.Forms;
using Newtonsoft.Json;


namespace prj3beer.Models
{
    public class Beverage
    {
        [Key]
        [Required (ErrorMessage = "Beverage ID is Required")]
        [JsonProperty("id")]
        [Range(1,999,ErrorMessage = "ID Range must be between 1 and 999")]
        public int BeverageID { get; set; }

        [Required (ErrorMessage = "Beverage Name is Required")]
        [JsonProperty("name")]
        [MinLength (3,ErrorMessage ="Must be at least 3 characters long")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Brand is Required")]
        [JsonProperty("brand")]
        //[MinLength(3, ErrorMessage = "Must be at least 3 characters long")]
        public Brand Brand { get; set; }

        [Required(ErrorMessage = "Type is Required")]
        [JsonProperty("type")]
        public Type Type { get; set; }

        [Required(ErrorMessage = "Temperature is Required")]
        [Range(-30, 30, ErrorMessage = "Target Temperature cannot be below -30C or above 30C")]
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
    }
}