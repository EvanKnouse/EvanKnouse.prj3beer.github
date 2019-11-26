using System;
using System.Collections.Generic;
using System.Text;
using prj3beer.Services;
using System.Threading.Tasks;

namespace prj3beer.Models
{
    public class LocalStorage
    {
        APIMockManager apiConnection; 
        public List<Brand> brandList;

        public LocalStorage()
        {
            brandList = new List<Brand>();
            apiConnection = new APIMockManager();
        }

        async void getBrandsFromAPI()
        {
            
        }

        void storeBrands()
        {

        }

        List<Brand> getBrandsFromLocalStorage()
        {
            return null;
        }
    }
}
