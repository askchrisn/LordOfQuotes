using System;
using System.Threading.Tasks;

namespace LordOfQuotes.Services.DataServices
{
    public interface IRequestService
    {
        Task<T> GetAsync<T>(string url, string apiKey = null);
    }
}
