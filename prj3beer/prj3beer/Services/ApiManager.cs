using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using prj3beer.Models;

namespace prj3beer.Services
{
    /// <summary>
    /// This class is responsible for connecting to an external API and getting the beverages from it
    /// </summary>
    public class ApiManager : IAPIManager
    {
        // This string will store the URL for the API
        public String BaseURL { get; set; }

        //Reference to a HttpClient for sending and recieving HTTP data
        HttpClient client;

        // Use methods from MOCK API MANAGER HERE
        public Task<List<Brand>> GetBrands()
        {
            //Do request here
            throw new NotImplementedException();
        }

        public async Task<List<Beverage>> GetBeveragesAsync()
        {
            var response = "";
            if(!BaseURL.Equals(""))
            {
                //Instantiate the HTTP client
                client = new HttpClient();

                //Create a response container to store the JSON string as a response
                response = await client.GetStringAsync(BaseURL);
            }
            else
            {
                response = "[{ \"id\":1,\"name\":\"Great Western Radler\",\"brand\":{ \"id\":1,\"name\":\"Great Western Brewing Company\"},\"type\":7,\"temperature\":3.0}]";
            }
            //Create a list to contain the Beverage objects that are deserialized from the JSON response
            List<Beverage> responseBeverages = JsonConvert.DeserializeObject<List<Beverage>>(response);

            //Create a list to contain the valid beverages
            List<Beverage> validBeverages = new List<Beverage>();

            //For each beverage in the list, do validation
            foreach(Beverage bev in responseBeverages)
            {
                if(ValidationHelper.Validate(bev).Count == 0)
                {
                    validBeverages.Add(bev);
                }
            }

            //Sort the valid beverages alphabetically
            validBeverages.Sort((a, b) => string.Compare(a.Name, b.Name));

            //return the valid beverages
            return validBeverages;
        }

    }
}
