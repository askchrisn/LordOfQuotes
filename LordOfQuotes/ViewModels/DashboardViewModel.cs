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
        private int IndexOfTopOfPage;
        private List<Quote> AllQuotes = new List<Quote>();

        public DashboardViewModel()
        {
            GetAllQuotes();
        }

        public ICommand PageDownCommand { get => new Command(async () => await PageDown()); }
        private async Task PageDown()
        {
            try
            {
                if (PageNumber == 1) return;

                PageNumber--;
                SetPagination(false);
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
                if (PageNumber >= CalculateLastPage()) return;

                PageNumber++;
                SetPagination(true);
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
                AllQuotes.Remove(quote);
                Quotes.Remove(quote);

                if (!Quotes.Any())
                {
                    PageDown();
                }

                Console.WriteLine(Quotes.Count());

                if(IndexOfTopOfPage + 9 < AllQuotes.Count)
                {
                    Quotes.Add(AllQuotes.ElementAt(IndexOfTopOfPage + 9));
                }

                var maxPageNumber = CalculateLastPage();
                PaginationString = $"{PageNumber} of {maxPageNumber}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void GetAllQuotes()
        {
            var paginatedQuotes = await HttpService.GetQuotes(PageNumber);
            AllQuotes = paginatedQuotes.Quotes;
            SetPagination(true);
        }

        private void SetPagination(bool forward)
        {
            // Get the next index that is needed
            IndexOfTopOfPage = AllQuotes.IndexOf(Quotes.LastOrDefault())+1;

            if(!forward)
            {
                // Get the index of 20 back and take the next 10 to go back 10
                IndexOfTopOfPage = IndexOfTopOfPage - 20 > 0 ? IndexOfTopOfPage - 20 : 0; 
            }

            var subsetOfQuotes = AllQuotes.Skip(IndexOfTopOfPage).Take(10);
            Quotes = new ObservableCollection<Quote>(subsetOfQuotes);

            var maxPageNumber = CalculateLastPage();
            PaginationString = $"{PageNumber} of {maxPageNumber}";
        }

        private decimal CalculateLastPage()
        {
            decimal amountCount = (decimal)AllQuotes.Count() / (decimal)10;
            var maxPageNumber = Math.Ceiling(amountCount);

            return maxPageNumber;
        }

        private ObservableCollection<Quote> _quotes = new ObservableCollection<Quote>();
        public ObservableCollection<Quote> Quotes
        {
            get => _quotes;
            set => SetProperty(ref _quotes, value);
        }

        private int _pageNumber = 1;
        public int PageNumber
        {
            get { return _pageNumber; }
            set { SetProperty(ref _pageNumber, value); }
        }

        private string _paginationString;
        public string PaginationString
        {
            get { return _paginationString; }
            set { SetProperty(ref _paginationString, value); }
        }
    }
}
