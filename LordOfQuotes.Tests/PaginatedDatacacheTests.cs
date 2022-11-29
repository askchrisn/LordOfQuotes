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
            quotes.Add(CreateDummyQuote("Quote1!"));
            quotes.Add(CreateDummyQuote("Quote2!"));
            quotes.Add(CreateDummyQuote("Quote3!"));
            quotes.Add(CreateDummyQuote("Quote4!"));
            paginatedDatacache.SetDatacache(quotes, 2);
        }

        [Fact]
        public void RemoveFromDatacache_RemoveAndAddsNextQuote()
        {
            // ARRANGE
            var quoteToRemove = paginatedDatacache.DisplayQuotes[0];

            // TEST
            var isSuccess = paginatedDatacache.RemoveQuote(quoteToRemove);

            // ASSERT
            Assert.True(isSuccess);
            Assert.Equal("Quote2!", paginatedDatacache.DisplayQuotes[0].Dialog);
            Assert.Equal("Quote3!", paginatedDatacache.DisplayQuotes[1].Dialog);
            Assert.Equal("1 of 2", paginatedDatacache.PaginationString);
        }

        [Fact]
        public void RemoveFromDatacache_RemoveAndAddsNextQuoteReducePage()
        {
            // ARRANGE
            var quoteToRemove1 = paginatedDatacache.DisplayQuotes[0];
            var quoteToRemove2 = paginatedDatacache.DisplayQuotes[1];

            // TEST
            var isSuccess1 = paginatedDatacache.RemoveQuote(quoteToRemove1);
            var isSuccess2 = paginatedDatacache.RemoveQuote(quoteToRemove2);

            // ASSERT
            Assert.True(isSuccess1);
            Assert.True(isSuccess2);
            Assert.Equal("Quote3!", paginatedDatacache.DisplayQuotes[0].Dialog);
            Assert.Equal("Quote4!", paginatedDatacache.DisplayQuotes[1].Dialog);
            Assert.Equal("1 of 1", paginatedDatacache.PaginationString);
        }

        [Fact]
        public void RemoveFromDatacache_RemoveLastQuoteNoAdd()
        {
            // ARRANGE
            paginatedDatacache.NextQuotes();
            var quoteToRemove = paginatedDatacache.DisplayQuotes[0];

            // TEST
            var isSuccess = paginatedDatacache.RemoveQuote(quoteToRemove);

            // ASSERT
            Assert.True(isSuccess);
            Assert.Single(paginatedDatacache.DisplayQuotes);
            Assert.Equal("Quote4!", paginatedDatacache.DisplayQuotes[0].Dialog);
            Assert.Equal("2 of 2", paginatedDatacache.PaginationString);
        }

        [Fact]
        public void RemoveFromDatacache_Fails()
        {
            // ARRANGE
            var quoteToRemove = CreateDummyQuote("Bad test!");

            // TEST
            var isSuccess = paginatedDatacache.RemoveQuote(quoteToRemove);

            // ASSERT
            Assert.False(isSuccess);
            Assert.Equal("Quote1!", paginatedDatacache.DisplayQuotes[0].Dialog);
            Assert.Equal("1 of 2", paginatedDatacache.PaginationString);
        }

        [Fact]
        public void NextQuotes_GoesToNextPage()
        {
            // ARRANGE

            // TEST
            paginatedDatacache.NextQuotes();

            // ASSERT
            Assert.Equal("Quote3!", paginatedDatacache.DisplayQuotes[0].Dialog);
            Assert.Equal("Quote4!", paginatedDatacache.DisplayQuotes[1].Dialog);
            Assert.Equal("2 of 2", paginatedDatacache.PaginationString);
        }

        [Fact]
        public void PrevQuotes_GoesToPreviousPage()
        {
            // ARRANGE

            // TEST
            paginatedDatacache.NextQuotes();
            paginatedDatacache.PreviousQuotes();

            // ASSERT
            Assert.Equal("Quote1!", paginatedDatacache.DisplayQuotes[0].Dialog);
            Assert.Equal("Quote2!", paginatedDatacache.DisplayQuotes[1].Dialog);
            Assert.Equal("1 of 2", paginatedDatacache.PaginationString);
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
