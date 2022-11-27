using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LordOfQuotes.Models;
using LordOfQuotes.ViewModels;

namespace LordOfQuotes.Services
{
    public class PaginatedDatacache : NotifyPropertyChanged, IPaginatedDatacache
    {
        private List<Quote> AllQuotes { get; set; }
        private int ItemsPerPage { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageLimit => (int)Math.Ceiling((decimal)AllQuotes.Count() / ItemsPerPage);

        public void SetDatacache(List<Quote> quotes, int itemsPerPage)
        {
            AllQuotes = quotes;
            Quotes = new ObservableCollection<Quote>(AllQuotes.Take(itemsPerPage));
            ItemsPerPage = itemsPerPage;
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        public void RemoveQuote(Quote quote)
        {
            bool s1 = AllQuotes.Remove(quote);
            bool s2 = Quotes.Remove(quote);

            AddNewQuote();
            PaginationString = $"{PageNumber} of {PageLimit}";

            if (!Quotes.Any())
            {
                PreviousQuotes();
            }
        }

        public void AddNewQuote()
        {
            var nextIndex = AllQuotes.IndexOf(Quotes.LastOrDefault())+1;
            // if there are no more quotes to add, return
            if (nextIndex >= AllQuotes.Count - 1) return;

            Quotes.Add(AllQuotes[nextIndex]);
        }

        public void NextQuotes()
        {
            PageNumber++;
            // get index of last quote in list
            var startIndex = AllQuotes.IndexOf(Quotes.LastOrDefault()) + 1;
            Quotes = new ObservableCollection<Quote>(CreateSubset(startIndex));
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        public void PreviousQuotes()
        {
            PageNumber--;
            // get index of first quote in list
            var startIndex = AllQuotes.IndexOf(Quotes.FirstOrDefault()) - ItemsPerPage;
            Quotes = new ObservableCollection<Quote>(CreateSubset(startIndex));
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        private List<Quote> CreateSubset(int index)
        {
            var startIndex = index > 0 ? index : 0;
            var subset = AllQuotes.Skip(startIndex).Take(10).ToList();

            return subset;
        }

        private ObservableCollection<Quote> _quotes = new ObservableCollection<Quote>();
        public ObservableCollection<Quote> Quotes
        {
            get => _quotes;
            private set => SetProperty(ref _quotes, value);
        }

        private string _paginationString;
        public string PaginationString
        {
            get { return _paginationString; }
            set { SetProperty(ref _paginationString, value); }
        }
    }
}