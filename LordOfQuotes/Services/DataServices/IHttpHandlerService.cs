using System;
using System.Net.Http;

namespace LordOfQuotes.Services.DataServices
{
    public interface IHttpHandlerService
    {
        HttpClientHandler GetHttpHandler();
    }
}
