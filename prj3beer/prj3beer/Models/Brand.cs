using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace prj3beer.Models
{
 
    public class Brand
    {
        [Key]
        public int brandID { get; set; }

        [Required(ErrorMessage ="Brand Name Required")]
        [MaxLength(60,ErrorMessage ="Brand Name Too Long, 60 Characters Max")]
        public String brandName { get; set; }
    }
}
