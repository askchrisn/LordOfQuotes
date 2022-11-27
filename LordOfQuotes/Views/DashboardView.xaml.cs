using System;
using System.Collections.Generic;
using LordOfQuotes.ViewModels;
using Xamarin.Forms;

namespace LordOfQuotes.Views
{
    public partial class DashboardView : ContentPage
    {
        DashboardViewModel vm;

        public DashboardView()
        {
            InitializeComponent();
            vm = App.ServiceProvider.GetService(typeof(DashboardViewModel)) as DashboardViewModel;

            this.BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            //await vm.OnAppearing();
        }
    }
}
