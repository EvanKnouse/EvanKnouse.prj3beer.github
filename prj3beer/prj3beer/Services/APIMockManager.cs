using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using prj3beer.Models;
using prj3beer.Utilities;

namespace prj3beer.Services
{
    public class APIMockManager : IAPIManager
    {
        public async Task<List<Brand>> GetBrands()
        {
            // Add a delay to run the rest of the method
            await Task.Delay(1000);

            // Mock string from the "api" containing all of the Brand Objects 
            string stringResponse = "[{\"id\": 3,\"name\":\"G\"},{\"id\":4,\"name\":\"Great Western Brewery\"},{\"id\":5,\"name\":\"Churchhill Brewing Company\"},{\"id\": 6,\"name\":\"Prarie Sun Brewery\"},{\"id\":7,\"name\":\"aaaaaaaaaabbbbbbbbbbccccccccccddddddddddeeeeeeeeeeffffffffffg\"}]";

            // Create a list of Brands Deserialized from the the response string
            List<Brand> APIBrands = await Task.Run(() => JsonConvert.DeserializeObject<List<Brand>>(stringResponse));

            // Create a Brand to Validate against
            //Brand validateBrand = new Brand() { brandID = APIBrands[0].brandID, brandName = APIBrands[0].brandName };

            // Create a list of "Valid Brands" to return
            List<Brand> validBrands = new List<Brand>();

            // For each Brand in the API Brands,
            foreach (Brand currentBrand in APIBrands)
            {   // If the Validation Helper does not contain any errors for the current brand, 
                if (ValidationHelper.Validate(currentBrand).Count == 0)
                {   // Add it to the valid Brands list
                    validBrands.Add(currentBrand);
                }
                //else statement to add errors to a string[]
                // Store them and do something
            }

            // Sort them alphabetically before returning them. 
            validBrands.Sort((a,b) => string.Compare(a.brandName,b.brandName));

            // Return all valid brands
            return validBrands;
        }

    }
}
