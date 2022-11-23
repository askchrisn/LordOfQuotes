using System;
using System.Threading.Tasks;
using LordOfQuotes.Services.DataServices;

namespace LordOfQuotes.Services
{
    public class HttpService : IHttpService
    {
        private const string BASE_URL = "https://the-one-api.dev/";
        private const string API_KEY = "ArKxecgybWqdt767qKpB";
        private ApiServiceBase apiServiceBase;

        public HttpService()
        {
            apiServiceBase = new ApiServiceBase(BASE_URL, API_KEY);
        }

        public Task<string> GetBooks()
        {
            return apiServiceBase.GetAsync<string>($"v2/quote");
        }
    }
}
