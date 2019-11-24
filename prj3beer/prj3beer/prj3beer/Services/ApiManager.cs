using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using prj3beer.Models;

namespace prj3beer.Services
{
    public class ApiManager : IAPIManager
    {
    public String BaseURL = "";

        

        public Task<List<Brand>> GetBrands()
        {
            //Do request here
            throw new NotImplementedException();
        }

    }
}
