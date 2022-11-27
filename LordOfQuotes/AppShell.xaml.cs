using System;
using System.Collections.Generic;
using LordOfQuotes.Views;
using Xamarin.Forms;

namespace LordOfQuotes
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.BindingContext = this;

            // REGISTER SCREENS HERE
            Routing.RegisterRoute(nameof(QuoteDetailView), typeof(QuoteDetailView));
        }
    }
}
