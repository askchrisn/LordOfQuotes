using System.Threading.Tasks;
using LordOfQuotes.Services.DataServices;

namespace LordOfQuotes.Services
{
    public class HttpService : ApiServiceBase, IHttpService
    {
        public Task<string> GetQuotes()
        {
            return GetAsync<string>($"v2/quote");
        }
    }
}
