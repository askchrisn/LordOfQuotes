using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LordOfQuotes.Services.DataServices;
using System.Net.Http.Headers;

namespace LordOfQuotes.Services
{
    public class RequestService : IRequestService
    {
        HttpClient client;

        public RequestService()
        {
            #if DEBUG
                var httpHandlerService = App.ServiceProvider.GetService(typeof(IHttpHandlerService)) as IHttpHandlerService;
                client = new HttpClient(httpHandlerService.GetHttpHandler());
            #else
                client = new HttpClient();
            #endif
        }

        public async Task<T> GetAsync<T>(string url, string apiKey = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(apiKey))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                }

                HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return await HandleResponse<T>(response, content);
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
