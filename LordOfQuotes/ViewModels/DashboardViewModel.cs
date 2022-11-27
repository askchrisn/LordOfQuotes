using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LordOfQuotes.Models;
using LordOfQuotes.Services;
using LordOfQuotes.Services.DataServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LordOfQuotes.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private const decimal ITEMSPERPAGE = 10;

        private int PageNumber { get; set; } = 1;
        private int PageLimit => (int)Math.Ceiling(Datacache.GetTotalQuoteCount() / ITEMSPERPAGE);

        public DashboardViewModel()
        {
            InitializeData();
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

        public ICommand RemoveQuoteCommand => new Command<Quote>(async (quote) => await RemoveQuote(quote));
        private async Task RemoveQuote(Quote quote)
        {
            try
            {
                Datacache.RemoveQuote(quote);
                Datacache.AddNewQuote();

                if (!Datacache.Quotes.Any())
                {
                    Datacache.PreviousQuotes();
                }

                PaginationString = $"{PageNumber} of {PageLimit}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void InitializeData()
        {
            var paginatedQuotes = await HttpService.GetQuotes();
            Datacache = new Datacache(paginatedQuotes.Quotes, 10);
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        private IDatacache _datacache;
        public IDatacache Datacache
        {
            get => _datacache;
            set => SetProperty(ref _datacache, value);
        }

        private string _paginationString;
        public string PaginationString
        {
            get { return _paginationString; }
            set { SetProperty(ref _paginationString, value); }
        }
    }
}
