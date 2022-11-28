using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LordOfQuotes.Models;
using LordOfQuotes.Services;
using LordOfQuotes.Services.DataServices;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

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
