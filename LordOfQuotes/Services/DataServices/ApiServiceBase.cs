using System;
using System.Threading.Tasks;

namespace LordOfQuotes.Services.DataServices
{
    public class ApiServiceBase
    {
        private readonly IRequestService requestProvider;

        public ApiServiceBase(IRequestService _requestProvider)
        {
            requestProvider = _requestProvider;
        }

        protected async Task<TApiResult> GetAsync<TApiResult>(string uri, string key)
        {
            var builder = new UriBuilder(uri);
            var builtUri = builder.ToString();

            return await requestProvider.GetAsync<TApiResult>(builtUri, key).ConfigureAwait(false);
        }
    }
}
