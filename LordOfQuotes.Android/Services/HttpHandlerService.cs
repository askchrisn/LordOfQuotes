using System;
using Xamarin.Forms;
using LordOfQuotes.Droid.Services;
using LordOfQuotes.Services.DataServices;
using System.Net.Http;

[assembly: Dependency(typeof(HttpHandlerService))]
namespace LordOfQuotes.Droid.Services
{
    public class HttpHandlerService : IHttpHandlerService
    {
        public HttpClientHandler GetHttpHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}
