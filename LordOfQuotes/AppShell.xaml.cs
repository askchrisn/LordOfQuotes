using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LordOfQuotes
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
    }
}
