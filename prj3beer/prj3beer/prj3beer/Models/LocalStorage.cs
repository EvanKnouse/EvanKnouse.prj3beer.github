using System;
using System.Collections.Generic;
using System.Text;
using prj3beer.Services;
using System.Threading.Tasks;

namespace prj3beer.Models
{
    /// <summary>
    /// This will act as the local database containing methods to interact with the local storage.
    /// </summary>
    public class LocalStorage
    {
        // Mock API Connection
        APIMockManager apiConnection; 
        // List of Locally Stored Brands
        public List<Brand> brandList;

        // Default constructor
        public LocalStorage()
        {
            //Instantiate a new list of brands
            brandList = new List<Brand>();
            // Instantiate a new API Conection(mock currently)
            apiConnection = new APIMockManager();
        }

        /// <summary>
        /// This method will grab all of the brands from the API(mockapi)
        /// </summary>
        async void getBrandsFromAPI()
        {
            
        }

        // This method will store brands in the local storage
        void storeBrands()
        {

        }

        // This method will grab all of the brands from the local storage
        public void getBrandsFromLocalStorage()
        {
            this.brandList = null; 
        }
    }
}
