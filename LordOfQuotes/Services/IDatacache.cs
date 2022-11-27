using System;
using System.Collections.ObjectModel;
using LordOfQuotes.Models;

namespace LordOfQuotes.Services
{
    public interface IDatacache
    {
        ObservableCollection<Quote> Quotes { get; }
        int PageNumber { get; }
        int PageLimit { get; }
        bool RemoveQuote(Quote quote);
        void AddNewQuote();
        bool PreviousQuotes();
        bool NextQuotes();
    }
}
