using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using prj3beer.Models;
using Newtonsoft.Json.Linq;

namespace prj3beer.Services
{
    /// <summary>
    /// This class is responsible for connecting to an external API and getting the beverages from it
    /// </summary>
    public class APIManager : IAPIManager
    {
        // This string will access the URL Setting
        public String BaseURL = Settings.URLSetting;

        //Reference to a HttpClient for sending and recieving HTTP data
        HttpClient client;

        public async Task<List<Brand>> GetBrandsAsync()
        {
            // Initialize variable to store response data
            string response = "";

            client = new HttpClient();

            try
            {   // Instantiate the HTTP client
                response = await client.GetStringAsync(BaseURL + "/brands");
            }
            catch (HttpRequestException)
            {
                response = "[{\"id\": 1,\"name\": \"Great Western Brewing Company\"}," +
                            "{\"id\": 2,\"name\": \"Churchill Brewing Company\"}]";
            }
 
            //Instantiate response brands list
            List<Brand> responseBrands = new List<Brand>();

            try
            {
                //Create a list to contain the Brand objects that are deserialized from the JSON response
                responseBrands = JsonConvert.DeserializeObject<List<Brand>>(response);
            }
            catch (Exception)
            {
                
            }


                //Create a list to contain the valid Brands
                List < Brand> validBrands = new List<Brand>();

            //For each beverage in the list, do validation
            foreach (Brand brand in responseBrands)
            {
                //If there are no validation errors,
                if (ValidationHelper.Validate(brand).Count == 0)
                {
                    // Add the valid beverages the valid list
                    validBrands.Add(brand);
                }
            }

            //Sort the valid beverages alphabetically
            validBrands.Sort((a, b) => string.Compare(a.Name, b.Name));

            //return the valid beverages
            return validBrands;
        }

        //public async Task<List<Beverage>> GetBeveragesAsync()
        public async Task<List<Beverage>> GetBeveragesAsync()
        {
            // Initialize variable to store response data
            string response = "";

            //Instantiate the HTTP client
            client = new HttpClient();

            try
            {
                //Create a response container to store the JSON string as a response
                response = await client.GetStringAsync(BaseURL + "/beverages");
            }
            catch (HttpRequestException)
            {
                response = "";
                /*response = "[" +
                           "{\"id\": 1,\"name\": \"Great Western Radler\",\"brand\": 1,\"type\": 7,\"temperature\": 3}," +
                           "{\"id\": 2,\"name\": \"Great Western Pilsner\",\"brand\": 1,\"type\": 4,\"temperature\": 13}," +
                           "{\"id\": 3, \"name\": \"Original 16 Copper Ale\", \"brand\": 1,\"type\": 5,\"temperature\": 2}]";
                */        
            }

          
            //Initialize response beverages list
            List<Beverage> responseBeverages = new List<Beverage>();

            //Create a list to contain the valid beverages
            List<Beverage> validBeverages = new List<Beverage>();

            //
            try
            {
                //Create a list to contain the Beverage objects that are deserialized from the JSON response
                responseBeverages = JsonConvert.DeserializeObject<List<Beverage>>(response);

                //For each beverage in the list, do validation
                foreach (Beverage bev in responseBeverages)
                {
                    //If there are no validation errors,
                    if (ValidationHelper.Validate(bev).Count == 0)
                    {
                        // Add the valid beverages the valid list
                        validBeverages.Add(bev);
                    }
                }

                //Sort the valid beverages alphabetically
                validBeverages.Sort((a, b) => string.Compare(a.Name, b.Name));
            }
            catch (Exception) { }

            //return the valid beverages
            return validBeverages;
        }

    }
}
