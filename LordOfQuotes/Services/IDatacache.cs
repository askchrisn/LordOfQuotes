using System;
using System.Collections.ObjectModel;
using LordOfQuotes.Models;

namespace LordOfQuotes.Services
{
    public interface IDatacache
    {
        ObservableCollection<Quote> Quotes { get; }
        int GetTotalQuoteCount();
        void RemoveQuote(Quote quote);
        void AddNewQuote();
        void PreviousQuotes();
        void NextQuotes();
    }
}
