using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LordOfQuotes.Dtos;
using LordOfQuotes.Models;
using LordOfQuotes.Services.DataServices;

namespace LordOfQuotes.Services
{
    public class HttpService : ApiServiceBase, IHttpService
    {
        // these should be in an app config in real world scenario
        protected override string authKey => "ArKxecgybWqdt767qKpB";
        protected override string baseUrl => "the-one-api.dev/";

        public async Task<PaginatedQuotes> GetQuotes()
        {
            var dto = await GetAsync<QuoteListDto>($"v2/quote?limit=20");
            return new PaginatedQuotes(dto);
        }

        public async Task<Quote> GetQuote(string quoteId)
        {
            var dto = await GetAsync<QuoteListDto>($"v2/quote/{quoteId}");
            return new Quote(dto.docs[0]);
        }

        public async Task<Movie> GetMovie(string movieId)
        {
            var dto = await GetAsync<MovieListDto>($"v2/movie/{movieId}");
            return new Movie(dto.docs[0]);
        }

        public async Task<Character> GetCharacter(string characterId)
        {
            var dto = await GetAsync<CharacterListDto>($"v2/character/{characterId}");
            return new Character(dto.docs[0]);
        }
    }
}
