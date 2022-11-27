using System;
using System.Threading.Tasks;
using LordOfQuotes.Models;
using Xamarin.Forms;

namespace LordOfQuotes.ViewModels
{
    [QueryProperty(nameof(QuoteId), nameof(QuoteId))]
    public class QuoteDetailViewModel : BaseViewModel
    {
        public async Task OnAppearing()
        {
            var quote = await HttpService.GetQuote(QuoteId);
            Movie = await HttpService.GetMovie(quote.Movie);
            Character = await HttpService.GetCharacter(quote.Character);
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

        private Movie _movie;
        public Movie Movie
        {
            get => _movie;
            set => SetProperty(ref _movie, value);
        }

        private Character _character;
        public Character Character
        {
            get => _character;
            set => SetProperty(ref _character, value);
        }
    }
}
