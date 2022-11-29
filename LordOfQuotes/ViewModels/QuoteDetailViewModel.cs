using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LordOfQuotes.Models;
using LordOfQuotes.Services;
using Xamarin.Forms;

namespace LordOfQuotes.ViewModels
{
    [QueryProperty(nameof(QuoteId), nameof(QuoteId))]
    public class QuoteDetailViewModel : BaseViewModel
    {
        public QuoteDetailViewModel(IPaginatedDatacache paginatedDatacache)
        {
            Datacache = paginatedDatacache;
        }

        public async Task OnAppearing()
        {
            try
            {
                // shell breaking serialization because too long
                // pass quote id and get it again
                Quote = await HttpService.GetQuote(QuoteId);
                Movie = await HttpService.GetMovie(Quote.Movie);
                Character = await HttpService.GetCharacter(Quote.Character);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ICommand RemoveQuoteCommand => new Command(async () => await RemoveQuote());
        private async Task RemoveQuote()
        {
            try
            {
                Datacache.RemoveQuote(Quote);
                Shell.Current.SendBackButtonPressed();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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

        private Quote _quote;
        public Quote Quote
        {
            get => _quote;
            set => SetProperty(ref _quote, value);
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

        private IPaginatedDatacache _datacache;
        public IPaginatedDatacache Datacache
        {
            get => _datacache;
            set => SetProperty(ref _datacache, value);
        }
    }
}
