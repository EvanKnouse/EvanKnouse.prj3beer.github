using Newtonsoft.Json;
using prj3beer.OAuth.Data;
using prj3beer.OAuth.Tokens;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace prj3beer.OAuth.Services
{
    public static class ProviderService
    {
        public static async Task<string> GetGoogleEmailAsync()
        {
            string googleTokenString = await SecureStorage.GetAsync("GOOGLE");

            string googleToken = JsonConvert.DeserializeObject<GoogleToken>(googleTokenString).AccessToken;

            HttpClient httpClient = new HttpClient();

            HttpResponseMessage httpResponse = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={googleToken}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                // Change to a TOAST In future
                Debug.WriteLine($"Could not get GOOGLE email. Status: {httpResponse.StatusCode}");
            }

            string data = await httpResponse.Content.ReadAsStringAsync();

            GoogleData googleData = JsonConvert.DeserializeObject<GoogleData>(data);

            return await Task.FromResult(googleData.Email);
        }
    }
}
