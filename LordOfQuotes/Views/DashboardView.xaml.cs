using System;
using System.Collections.Generic;
using LordOfQuotes.ViewModels;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace LordOfQuotes.Views
{
    public partial class DashboardView : ContentPage
    {
        DashboardViewModel vm;

        public DashboardView()
        {
            InitializeComponent();
            vm = App.ServiceProvider.GetService<DashboardViewModel>();

            this.BindingContext = vm;
        }
    }
}
