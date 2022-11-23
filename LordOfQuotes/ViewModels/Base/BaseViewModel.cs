using System;
using System.Threading.Tasks;
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
            HttpService = App.ServiceProvider.GetService(typeof(IHttpService)) as HttpService;
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

        public virtual Task InitializeAsync(object parameter = null)
        {
            return Task.CompletedTask;
        }
    }
}
