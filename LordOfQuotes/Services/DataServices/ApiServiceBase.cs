using System;
using System.Threading.Tasks;

namespace LordOfQuotes.Services.DataServices
{
    public class ApiServiceBase : RequestService
    {
        private string authKey { get; }
        private string baseUrl { get; }
        private readonly IRequestService requestProvider;

        public ApiServiceBase(string _baseUrl, string _apiKey)
        {
            requestProvider = App.ServiceProvider.GetService(typeof(IRequestService)) as RequestService;
            baseUrl = _baseUrl;
            authKey = _apiKey;
        }

        protected async Task<TApiResult> GetAsync<TApiResult>(string uri)
        {
            var builder = new UriBuilder($"{baseUrl}{uri}");
            var builtUri = builder.ToString();

            return await requestProvider.GetAsync<TApiResult>(builtUri, authKey).ConfigureAwait(false);
        }
    }
}
