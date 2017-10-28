using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebUI.Exception;
using WebUI.ProjectConstants;

namespace WebUI.Helpers
{
    public class WebApiService
    {
        public string BaseUri { get; }

        private WebApiService(string baseUri)
        {
            BaseUri = baseUri;
        }

        private static WebApiService _instance;

        public static WebApiService Instance => _instance ?? (_instance = new WebApiService(ConstantsUrls.WebApiUri));

        public async Task<T> AuthenticateAsync<T>(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(Flurl.Url.Combine(BaseUri, "Token"), new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("userName", userName),
                    new KeyValuePair<string, string>("password", password)
                }));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
        }

        public async Task LogOutAsync(string token)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(token))
                {
                    //Add the authorization header
                    client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + token);
                }

                var result = await client.GetAsync(Flurl.Url.Combine(BaseUri, "/api/Account/Logout"));

                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                throw new ApiException(result.StatusCode, string.Empty);
            }
        }
    }
}