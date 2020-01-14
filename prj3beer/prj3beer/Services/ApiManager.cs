using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using prj3beer.Models;

namespace prj3beer.Services
{
    /// <summary>
    /// This class is responsible for connecting to an external API and getting the beverages from it
    /// </summary>
    public class ApiManager : IAPIManager
    {
        // This string will store the URL for the API
        public String BaseURL = "";
        
          
        // Use methods from MOCK API MANAGER HERE

        public Task<List<Brand>> GetBrands()
        {
            //Do request here
            throw new NotImplementedException();
        }

        public Task<List<Brand>> GetBeverages()
        {
            //Do request here
            throw new NotImplementedException();
        }

    }
}
