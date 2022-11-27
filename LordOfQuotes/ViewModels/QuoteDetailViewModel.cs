using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LordOfQuotes.ViewModels
{
    [QueryProperty(nameof(QuoteId), nameof(QuoteId))]
    public class QuoteDetailViewModel : BaseViewModel
    {
        public async Task OnAppearing()
        {
            var quote = await HttpService.GetQuote(QuoteId);
            var movie = await HttpService.GetMovie(quote.Movie);
        }

        private string quoteId;
        public string QuoteId
        {
            get
            {
                return quoteId;
            }
            set
            {
                quoteId = value;
            }
        }
    }
}
