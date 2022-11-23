using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LordOfQuotes.Services;
using LordOfQuotes.Services.DataServices;
using Xamarin.Forms;

namespace LordOfQuotes.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    { 
        public DashboardViewModel()
        {
            InitializeAsync().Wait();
            Console.WriteLine("Initialized");
        }

        public async override Task InitializeAsync(object navigationData = null)
        {
            //var books = await HttpService.GetSample();
            //Console.WriteLine(books);
        }

        public ICommand ButtonClickedCommand { get => new Command(async () => await ButtonClicked()); }
        private async Task ButtonClicked()
        {
            try
            {
                var books = await HttpService.GetBooks();
                Console.WriteLine(books);
                TextString = books;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private string textString = "inited";
        public string TextString
        {
            get { return textString; }
            set { SetProperty(ref textString, value); }
        }
    }
}
