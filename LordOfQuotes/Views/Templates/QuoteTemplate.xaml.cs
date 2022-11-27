using System;
using System.Collections.Generic;
using LordOfQuotes.ViewModels;
using Xamarin.Forms;

namespace LordOfQuotes.Views.Templates
{
    public partial class QuoteTemplate : ContentView
    {
        public QuoteTemplate()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ParentContextProperty =
           BindableProperty.Create("ParentContext", typeof(object), typeof(QuoteTemplate), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as QuoteTemplate).ParentContext = newValue;
            }
        }

        void RemoveQuoteClicked(object sender, EventArgs args)
        {
            if (this.ParentContext is DashboardViewModel dvm)
            {
                dvm.RemoveQuoteCommand.Execute(this.BindingContext);
            }
        }

        void GoToQuoteClicked(System.Object sender, System.EventArgs e)
        {
            if (this.ParentContext is DashboardViewModel dvm)
            {
                dvm.GoToQuoteCommand.Execute(this.BindingContext);
            }
        }
    }
}
