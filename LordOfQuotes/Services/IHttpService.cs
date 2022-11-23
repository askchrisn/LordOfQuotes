using System;
using System.Threading.Tasks;

namespace LordOfQuotes.Services
{
    public interface IHttpService
    {
        Task<string> GetBooks();
    }
}
