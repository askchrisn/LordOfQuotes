using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
            authKey = "ArKxecgybWqdt767qKpB";
            baseUrl = "the-one-api.dev/";

            #if DEBUG
                var httpHandlerService = App.ServiceProvider.GetService(typeof(IHttpHandlerService)) as IHttpHandlerService;
                client = new HttpClient(httpHandlerService.GetHttpHandler());
            #else
                client = new HttpClient();
            #endif
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

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
