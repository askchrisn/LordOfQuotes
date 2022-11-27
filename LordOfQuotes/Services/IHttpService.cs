using System.Collections.Generic;
using System.Threading.Tasks;
using LordOfQuotes.Dtos;
using LordOfQuotes.Models;

namespace LordOfQuotes.Services
{
    public interface IHttpService 
    {
        Task<PaginatedQuotes> GetQuotes();
        Task<Quote> GetQuote(string quoteId);
        Task<string> GetMovie(string movieId);
    }
}
