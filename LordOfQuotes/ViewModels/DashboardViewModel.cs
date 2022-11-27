using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LordOfQuotes.Models;
using LordOfQuotes.Services;
using LordOfQuotes.Services.DataServices;
using LordOfQuotes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LordOfQuotes.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel()
        {
            GetAllQuotes();
        }

        public async Task OnAppearing()
        {
            //var paginatedQuotes = await HttpService.GetQuotes();
            //Datacache = new Datacache();
            //Datacache.SetDatacache(paginatedQuotes.Quotes, 10);
            //PaginationString = $"{PageNumber} of {PageLimit}";
        }

        private async void GetAllQuotes()
        {
            var paginatedQuotes = await HttpService.GetQuotes();
            Datacache = new Datacache();
            Datacache.SetDatacache(paginatedQuotes.Quotes, 10);
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        public ICommand RemoveQuoteCommand => new Command<Quote>(async (quote) => await RemoveQuote(quote));
        private async Task RemoveQuote(Quote quote)
        {
            try
            {
                MarkQuoteAsRead(quote);
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
                if (PageNumber <= 1) return;

                PageNumber--;
                Datacache.PreviousQuotes();
                PaginationString = $"{PageNumber} of {PageLimit}";
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
                if (PageNumber >= PageLimit) return;

                PageNumber++;
                Datacache.NextQuotes();
                PaginationString = $"{PageNumber} of {PageLimit}";
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
    }
}
