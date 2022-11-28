using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using LordOfQuotes.ViewModels;
using Xamarin.Forms;

namespace LordOfQuotes.Views
{
    public partial class QuoteDetailView : ContentPage
    {
        QuoteDetailViewModel vm;

        public QuoteDetailView()
        {
            InitializeComponent();
            vm = App.ServiceProvider.GetService<QuoteDetailViewModel>();

            this.BindingContext = vm;
        }

        protected override async void OnAppearing()
        {
            await vm.OnAppearing();
        }
    }
}
