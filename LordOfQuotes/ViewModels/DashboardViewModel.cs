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
            PaginationData = paginatedDatacache;
            GetAllQuotes();
        }

        private async void GetAllQuotes()
        {
            var paginatedQuotes = await HttpService.GetQuotes();
            PaginationData.SetDatacache(paginatedQuotes.Quotes, 10);
        }

        public ICommand RemoveQuoteCommand => new Command<Quote>((quote) => RemoveQuote(quote));
        private void RemoveQuote(Quote quote)
        {
            try
            {
                PaginationData.RemoveQuote(quote);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ICommand PageDownCommand { get => new Command(() => PageDown()); }
        private void PageDown()
        {
            try
            {
                // if on first page return
                if (PaginationData.PageNumber <= 1) return;

                PaginationData.PreviousQuotes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ICommand PageUpCommand { get => new Command(() => PageUp()); }
        private void PageUp()
        {
            try
            {
                // if last page return
                if (PaginationData.PageNumber >= PaginationData.PageLimit) return;

                PaginationData.NextQuotes();
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

        private IPaginatedDatacache _paginationData;
        public IPaginatedDatacache PaginationData
        {
            get => _paginationData;
            set => SetProperty(ref _paginationData, value);
        }
    }
}
