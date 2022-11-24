using System.Collections.Generic;
using System.Threading.Tasks;
using LordOfQuotes.Dtos;
using LordOfQuotes.Models;
using LordOfQuotes.Services.DataServices;

namespace LordOfQuotes.Services
{
    public class HttpService : ApiServiceBase, IHttpService
    {
        public async Task<PaginatedQuotes> GetQuotes(int pageNumber)
        {
            var dto = await GetAsync<Root>($"v2/quote").ConfigureAwait(false);

            return new PaginatedQuotes(dto);
        }
    }
}
