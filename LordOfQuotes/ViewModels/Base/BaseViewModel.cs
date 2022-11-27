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
        private const decimal ITEMSPERPAGE = 10;

        public IHttpService HttpService { get; }
        public int PageNumber { get; set; } = 1;
        public int PageLimit => (int)Math.Ceiling(Datacache.GetTotalQuoteCount() / ITEMSPERPAGE);

        public BaseViewModel()
        {
            Datacache = App.ServiceProvider.GetService(typeof(IDatacache)) as IDatacache;
            HttpService = App.ServiceProvider.GetService(typeof(IHttpService)) as IHttpService;
        }

        public void MarkQuoteAsRead(Quote quote)
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

        private IDatacache _datacache;
        public IDatacache Datacache
        {
            get => _datacache;
            set => SetProperty(ref _datacache, value);
        }

        private string _paginationString;
        public string PaginationString
        {
            get => _paginationString;
            set => SetProperty(ref _paginationString, value);
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
