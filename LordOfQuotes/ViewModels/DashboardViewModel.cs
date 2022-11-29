using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LordOfQuotes.Models;
using LordOfQuotes.Services;
using LordOfQuotes.Views;
using Xamarin.Forms;

namespace LordOfQuotes.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(IPaginatedDatacache paginatedDatacache)
        {
            Datacache = paginatedDatacache;
            GetAllQuotes();
        }

        private async void GetAllQuotes()
        {
            var paginatedQuotes = await HttpService.GetQuotes();
            Datacache.SetDatacache(paginatedQuotes.Quotes, 10);
        }

        public ICommand RemoveQuoteCommand => new Command<Quote>(async (quote) => await RemoveQuote(quote));
        private async Task RemoveQuote(Quote quote)
        {
            try
            {
                Datacache.RemoveQuote(quote);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ICommand PageDownCommand { get => new Command(async () => await PageDown()); }
        private async Task PageDown()
        {
            try
            {
                // if on first page return
                if (Datacache.PageNumber <= 1) return;

                Datacache.PreviousQuotes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ICommand PageUpCommand { get => new Command(async () => await PageUp()); }
        private async Task PageUp()
        {
            try
            {
                // if last page return
                if (Datacache.PageNumber >= Datacache.PageLimit) return;

                Datacache.NextQuotes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ICommand GoToQuoteCommand => new Command<Quote>(async (quote) => await GoToQuote(quote));
        private async Task GoToQuote(Quote quote)
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(QuoteDetailView)}?{nameof(QuoteDetailViewModel.QuoteId)}={quote.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private IPaginatedDatacache _datacache;
        public IPaginatedDatacache Datacache
        {
            get => _datacache;
            set => SetProperty(ref _datacache, value);
        }
    }
}
