using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LordOfQuotes.Models;
using LordOfQuotes.ViewModels;

namespace LordOfQuotes.Services
{
    public class Datacache : NotifyPropertyChanged, IDatacache
    {
        private List<Quote> AllQuotes { get; set; }
        private int ItemsPerPage { get; set; }
        public int PageNumber { get; private set; } = 1;
        public int PageLimit { get; private set; }

        private ObservableCollection<Quote> _quotes = new ObservableCollection<Quote>();
        public ObservableCollection<Quote> Quotes
        {
            get => _quotes;
            private set => SetProperty(ref _quotes, value);
        }

        public Datacache(List<Quote> quotes, int itemsPerPage)
        {
            AllQuotes = quotes;
            Quotes = new ObservableCollection<Quote>(AllQuotes.Take(itemsPerPage));
            ItemsPerPage = itemsPerPage;

            CalculatePageLimit();
        }

        public bool RemoveQuote(Quote quote)
        {
            AllQuotes.Remove(quote);
            Quotes.Remove(quote);

            CalculatePageLimit();

            return true;
        }

        public void AddNewQuote()
        {
            var nextIndex = AllQuotes.IndexOf(Quotes.LastOrDefault())+1;
            if (nextIndex >= AllQuotes.Count - 1) return;

            Quotes.Add(AllQuotes[nextIndex]);

            CalculatePageLimit();
        }

        public bool NextQuotes()
        {
            PageNumber++;
            // get index of last quote in list
            var startIndex = AllQuotes.IndexOf(Quotes.LastOrDefault()) + 1;
            Quotes = new ObservableCollection<Quote>(CreateSubset(startIndex));

            return true;
        }

        public bool PreviousQuotes()
        {
            PageNumber--;
            // get index of first quote in list
            var startIndex = AllQuotes.IndexOf(Quotes.FirstOrDefault()) - ItemsPerPage;
            Quotes = new ObservableCollection<Quote>(CreateSubset(startIndex));

            return true;
        }

        public void CalculatePageLimit()
        {
            PageLimit = (int)Math.Ceiling((decimal)AllQuotes.Count() / (decimal)ItemsPerPage);
        }

        private List<Quote> CreateSubset(int index)
        {
            var startIndex = index > 0 ? index : 0;
            var subset = AllQuotes.Skip(startIndex).Take(10).ToList();

            return subset;
        }
    }
}