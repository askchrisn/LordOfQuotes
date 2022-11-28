using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LordOfQuotes.Dtos;
using Newtonsoft.Json;

namespace LordOfQuotes.Services.DataServices
{
    public class ApiServiceBase
    {
        HttpClient client;
        internal string authKey { private get; set; }
        internal string baseUrl { private get; set; }

        public ApiServiceBase()
        {
            // should have been read in from DB connection of sorts
            authKey = "ArKxecgybWqdt767qKpB";
            baseUrl = "the-one-api.dev/";

            client = new HttpClient();
        }

        public async Task<TApiResult> GetAsync<TApiResult>(string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(authKey))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authKey);
                }

                HttpResponseMessage response = await client.GetAsync($"https://{baseUrl}{url}").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return await HandleResponse<TApiResult>(response, content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response, string content)
        {
            if (response == null || response.Content == null)
            {
                throw new NullReferenceException();
            }

            content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var convertedContent = JsonConvert.DeserializeObject<T>(content);
            return convertedContent;
        }
    }
}
