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
            string stringResponse = "[{\"id\": 3,\"name\":\"G\"},{\"id\":4,\"name\":\"Great Western Brewery\"},{\"id\":5,\"name\":\"Churchhill Brewing Company\"},{\"id\": 6,\"name\":\"Prarie Sun Brewery\"},{\"id\":7,\"name\":\"aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffffg\"}]";

            List<Brand> APIBrands = await Task.Run(() => JsonConvert.DeserializeObject<List<Brand>>(stringResponse));

            Brand validateBrand = new Brand() { brandID = APIBrands[0].brandID, brandName = APIBrands[0].brandName };

            List<Brand> validBrands = new List<Brand>();

            foreach (Brand currentBrand in APIBrands)
            {
                if (ValidationHelper.Validate(currentBrand).Count == 0)
                {
                    validBrands.Add(currentBrand);
                }
                //else statement to add errors to a string[]
            }

            //var validation = new ValidationHelper();

           // ValidationHelper.Validate(validateBrand);

            validBrands.Sort((a,b) => string.Compare(a.brandName,b.brandName));

            return validBrands;
        }

    }
}
