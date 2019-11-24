using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using prj3beer.Models;

namespace prj3beer.Services
{
    public class APIMockManager : IAPIManager
    {
        public async Task<List<Brand>> getBrandsAsync()
        {
            await Task.Delay(1000);
            var stringResponse = "[  {\"id\": 3, \"name\": \"G\" },  { \"id\": 4,    \"name\": \"Great Western Brewery\" },{ \"id\": 5,    \"name\": \"Churchhill Brewing Company\"  },  {                \"id\": 6,    \"name\": \"Prarie Sun Brewery\"  },  {                \"id\": 7,    \"name\": \"aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffffg\"}]";

            var brands = await Task.Run(() => JsonConvert.DeserializeObject<List<Brand>>(stringResponse));
    

            return brands;
        }

    }
}
