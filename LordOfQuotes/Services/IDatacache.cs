using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LordOfQuotes.Models;

namespace LordOfQuotes.Services
{
    public interface IDatacache
    {
        ObservableCollection<Quote> Quotes { get; }
        void SetDatacache(List<Quote> quotes, int itemsPerPage);
        int GetTotalQuoteCount();
        void RemoveQuote(Quote quote);
        void AddNewQuote();
        void PreviousQuotes();
        void NextQuotes();
    }
}
