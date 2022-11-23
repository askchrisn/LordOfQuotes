using System;
using System.Threading.Tasks;
using LordOfQuotes.Services.DataServices;

namespace LordOfQuotes.Services
{
    public class HttpService : ApiServiceBase, IHttpService
    {
        private string baseUrl = "https://the-one-api.dev/v2";
        private string key = "ArKxecgybWqdt767qKpB";

        public HttpService(IRequestService requestProvider) : base(requestProvider)
        {

        }

        public Task<string> GetBooks()
        {
            return GetAsync<string>($"{this.baseUrl}/quote", key);
        }
    }
}
