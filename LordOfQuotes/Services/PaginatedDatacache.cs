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
            DisplayQuotes = new ObservableCollection<Quote>(AllQuotes.Take(itemsPerPage));
            ItemsPerPage = itemsPerPage;
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        public bool RemoveQuote(Quote quote)
        {
            var identicalQuote = AllQuotes.FirstOrDefault(x => x.Id == quote.Id);
            if (identicalQuote == null) return false;

            AllQuotes.Remove(identicalQuote);
            DisplayQuotes.Remove(identicalQuote);

            AddNewQuote();
            PaginationString = $"{PageNumber} of {PageLimit}";

            if (!DisplayQuotes.Any())
            {
                PreviousQuotes();
            }

            return true;
        }

        public void NextQuotes()
        {
            PageNumber++;
            // get index of last quote in list
            var startIndex = AllQuotes.IndexOf(DisplayQuotes.LastOrDefault()) + 1;
            DisplayQuotes = new ObservableCollection<Quote>(CreateSubset(startIndex));
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        public void PreviousQuotes()
        {
            PageNumber--;
            // get index of first quote in list
            var startIndex = AllQuotes.IndexOf(DisplayQuotes.FirstOrDefault()) - ItemsPerPage;
            DisplayQuotes = new ObservableCollection<Quote>(CreateSubset(startIndex));
            PaginationString = $"{PageNumber} of {PageLimit}";
        }

        private void AddNewQuote()
        {
            var nextIndex = AllQuotes.IndexOf(DisplayQuotes.LastOrDefault()) + 1;
            // if there are no more quotes to add, return
            if (nextIndex >= AllQuotes.Count) return;

            DisplayQuotes.Add(AllQuotes[nextIndex]);
        }

        private List<Quote> CreateSubset(int index)
        {
            var startIndex = index > 0 ? index : 0;
            var subset = AllQuotes.Skip(startIndex).Take(10).ToList();

            return subset;
        }

        private ObservableCollection<Quote> _displayQuotes = new ObservableCollection<Quote>();
        public ObservableCollection<Quote> DisplayQuotes
        {
            get => _displayQuotes;
            private set => SetProperty(ref _displayQuotes, value);
        }

        private string _paginationString;
        public string PaginationString
        {
            get { return _paginationString; }
            set { SetProperty(ref _paginationString, value); }
        }
    }
}