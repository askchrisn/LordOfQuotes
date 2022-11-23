using System;
using System.Collections.Generic;
using LordOfQuotes.ViewModels;
using Xamarin.Forms;

namespace LordOfQuotes.Views
{
    public partial class DashboardView : ContentPage
    {
        public DashboardView()
        {
            InitializeComponent();
            this.BindingContext = App.ServiceProvider.GetService(typeof(DashboardViewModel)) as DashboardViewModel;
        }
    }
}
