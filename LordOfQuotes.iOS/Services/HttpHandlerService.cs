using System.Net.Http;
using LordOfQuotes.Services.DataServices;

[assembly: Xamarin.Forms.Dependency(typeof(IHttpHandlerService))]
namespace LordOfQuotes.iOS.Services
{
    public class HttpHandlerService : IHttpHandlerService
    {
        HttpClientHandler handler = new HttpClientHandler();
        public HttpClientHandler GetHttpHandler()
        {
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
