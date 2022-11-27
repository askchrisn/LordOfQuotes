using System.Collections.Generic;
using System.Threading.Tasks;
using LordOfQuotes.Dtos;
using LordOfQuotes.Models;
using LordOfQuotes.Services.DataServices;

namespace LordOfQuotes.Services
{
    public class HttpService : ApiServiceBase, IHttpService
    {
        public async Task<PaginatedQuotes> GetQuotes()
        {
            var dto = await GetAsync<QuoteListDto>($"v2/quote?limit=20").ConfigureAwait(false);
            return new PaginatedQuotes(dto);
        }

        public async Task<Quote> GetQuote(string quoteId)
        {
            var dto = await GetAsync<QuoteListDto>($"v2/quote/{quoteId}").ConfigureAwait(false);
            return new Quote(dto.docs[0]);
        }

        public async Task<Movie> GetMovie(string movieId)
        {
            var dto = await GetAsync<MovieListDto>($"v2/movie/{movieId}").ConfigureAwait(false);
            return new Movie(dto.docs[0]);
        }

        public async Task<Character> GetCharacter(string characterId)
        {
            var dto = await GetAsync<CharacterListDto>($"v2/character/{characterId}").ConfigureAwait(false);
            return new Character(dto.docs[0]);
        }
    }
}
