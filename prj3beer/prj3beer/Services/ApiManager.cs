using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using prj3beer.Models;

namespace prj3beer.Services
{
    /// <summary>
    /// This class is responsible for connecting to an external API and getting the brands from it
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

    }
}
