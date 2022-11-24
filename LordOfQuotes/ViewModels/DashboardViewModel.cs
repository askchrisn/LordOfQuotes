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
        public DashboardViewModel()
        {
            GetAllQuotes();
        }

        public ICommand PageDownCommand { get => new Command(async () => await PageDown()); }
        private async Task PageDown()
        {
            try
            {
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
                Quotes.Add(AllQuotes.ElementAt(LastIndex+9));
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
            LastIndex = AllQuotes.IndexOf(Quotes.LastOrDefault())+1;

            if(!forward)
            {
                // Get the index of 20 back and take the next 10 to go back 10
                LastIndex = LastIndex - 20 > 0 ? LastIndex - 20 : 0; 
            }

            var subsetOfQuotes = AllQuotes.Skip(LastIndex).Take(10);
            Quotes = new ObservableCollection<Quote>(subsetOfQuotes);
        }

        private int PageNumber = 1;
        private int LastIndex;

        private List<Quote> AllQuotes = new List<Quote>();

        private ObservableCollection<Quote> _quotes = new ObservableCollection<Quote>();
        public ObservableCollection<Quote> Quotes
        {
            get => _quotes;
            set => SetProperty(ref _quotes, value);
        }

        private string _pagination;
        public string Pagination
        {
            get { return _pagination; }
            set { SetProperty(ref _pagination, value); }
        }
    }
}
