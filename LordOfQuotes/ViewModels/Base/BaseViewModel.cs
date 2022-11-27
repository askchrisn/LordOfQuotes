using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LordOfQuotes.Models;
using LordOfQuotes.Services;
using LordOfQuotes.Services.DataServices;
using Xamarin.Forms;

namespace LordOfQuotes.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        public IHttpService HttpService { get; }

        public BaseViewModel()
        {
            HttpService = App.ServiceProvider.GetService(typeof(IHttpService)) as IHttpService;
        }

        public ICommand RemoveQuoteCommand => new Command<Quote>(async (quote) => await RemoveQuote(quote));
        public virtual async Task RemoveQuote(Quote quote)
        {
            try
            {
                Datacache.RemoveQuote(quote);
                Datacache.AddNewQuote();

                if (!Datacache.Quotes.Any())
                {
                    Datacache.PreviousQuotes();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private IDatacache _datacache;
        public IDatacache Datacache
        {
            get => _datacache;
            set => SetProperty(ref _datacache, value);
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}
