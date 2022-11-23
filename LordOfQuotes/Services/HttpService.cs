using System.Collections.Generic;
using System.Threading.Tasks;
using LordOfQuotes.Dtos;
using LordOfQuotes.Models;
using LordOfQuotes.Services.DataServices;

namespace LordOfQuotes.Services
{
    public class HttpService : ApiServiceBase, IHttpService
    {
        public async Task<List<Quote>> GetQuotes()
        {
            var dto = await GetAsync<Root>($"v2/quote");

            var listOfQuotes = new List<Quote>();
            foreach(var quoteDto in dto.docs)
            {
                listOfQuotes.Add(new Quote(quoteDto));
            }

            return listOfQuotes;
        }
    }
}
