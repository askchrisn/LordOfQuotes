using System;
using System.Collections.Generic;
using System.Linq;
using LordOfQuotes.Models;
using LordOfQuotes.Services;
using Xunit;

namespace LordOfQuotes.Tests
{
    public class PaginatedDatacacheTests
    {
        private PaginatedDatacache paginatedDatacache = new PaginatedDatacache();

        public PaginatedDatacacheTests()
        {
            List<Quote> quotes = new List<Quote>();
            quotes.Add(CreateDummyQuote("Hi!"));
            quotes.Add(CreateDummyQuote("Hey!"));
            quotes.Add(CreateDummyQuote("Hello!"));

            paginatedDatacache.SetDatacache(quotes, 1);
        }

        [Fact]
        public void RemoveFromDatacache_RemoveExistingQuote_RemovesExistingAndAddNewQuote()
        {
            // ARRANGE
            var quoteToRemove = paginatedDatacache.Quotes[0];

            // TEST
            var isSuccess = paginatedDatacache.RemoveQuote(quoteToRemove);

            // ASSERT
            Assert.True(isSuccess);
            Assert.Equal("Hey!", paginatedDatacache.Quotes.First().Dialog);
            Assert.Equal("1 of 2", paginatedDatacache.PaginationString);
        }

        [Fact]
        public void RemoveFromDatacache_AttemptToRemoveNewQuote_Fail()
        {
            // ARRANGE
            var quoteToRemove = CreateDummyQuote("Bad test!");

            // TEST
            var isSuccess = paginatedDatacache.RemoveQuote(quoteToRemove);

            // ASSERT
            Assert.False(isSuccess);
            Assert.Equal("Hi!", paginatedDatacache.Quotes.First().Dialog);
            Assert.Equal("1 of 3", paginatedDatacache.PaginationString);
        }

        private Quote CreateDummyQuote(string dialog)
        {
            var quote = new Quote();
            quote.Dialog = dialog;
            quote.Id = Guid.NewGuid().ToString();

            return quote;
        }
    }
}
