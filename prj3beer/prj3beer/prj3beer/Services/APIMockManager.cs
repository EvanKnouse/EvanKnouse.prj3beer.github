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
        public async Task<List<Brand>> GetBrands()
        {
            await Task.Delay(1000);
            var stringResponse = "[{\"id\": 3,\"name\":\"G\"},{\"id\":4,\"name\":\"Great Western Brewery\"},{\"id\":5,\"name\":\"Churchhill Brewing Company\"},{\"id\": 6,\"name\":\"Prarie Sun Brewery\"},{\"id\":7,\"name\":\"aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffffg\"}]";

            var brands = await Task.Run(() => JsonConvert.DeserializeObject<List<Brand>>(stringResponse));

            Brand validateBrand = new Brand() { brandID = brands[0].brandID, brandName = brands[0].brandName };

            var validation = new ValidationHelper();

            ValidationHelper.Validate(validateBrand);
           
            brands.Sort((a,b) => string.Compare(a.brandName,b.brandName));

            return brands;
        }

    }
}
