using LordOfQuotes.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LordOfQuotes.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        public IHttpService HttpService { get; }

        public BaseViewModel()
        {
            HttpService = App.ServiceProvider.GetService<IHttpService>();
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
