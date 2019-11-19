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
        public String brandName { get; set; }

        public Brand()
        {

        }


    }

   

}
