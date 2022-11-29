using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LordOfQuotes.Models;

namespace LordOfQuotes.Services
{
    public interface IPaginatedDatacache
    {
        int PageNumber { get; }
        int PageLimit { get; }
        ObservableCollection<Quote> Quotes { get; }
        void SetDatacache(List<Quote> quotes, int itemsPerPage);
        bool RemoveQuote(Quote quote);
        void PreviousQuotes();
        void NextQuotes();
    }
}
