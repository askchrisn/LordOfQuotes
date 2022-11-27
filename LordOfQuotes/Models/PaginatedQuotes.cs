using System;
using System.Collections.Generic;
using LordOfQuotes.Dtos;

namespace LordOfQuotes.Models
{
    public class PaginatedQuotes
    {
        public List<Quote> Quotes { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public PaginatedQuotes(QuoteListDto dto)
        {
            Quotes = new List<Quote>();
            foreach (var quoteDto in dto.docs)
            {
                Quotes.Add(new Quote(quoteDto));
            }
            Page = dto.page;
            TotalPages = dto.pages;
        }
    }
}
