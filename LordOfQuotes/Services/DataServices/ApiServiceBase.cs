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
        protected virtual string authKey => throw new NotImplementedException();
        protected virtual string baseUrl => throw new NotImplementedException();

        public ApiServiceBase()
        {
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
