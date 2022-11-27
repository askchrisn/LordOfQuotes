using System;
using LordOfQuotes.Dtos;

namespace LordOfQuotes.Models
{
    public class Quote
    {
        public string Id { get; set; }
        public string Dialog { get; set; }
        public string Movie { get; set; }
        public string Character { get; set; }

        public Quote(QuoteDto dto)
        {
            Id = dto.id;
            Dialog = dto.dialog;
            Movie = dto.movie;
            Character = dto.character;
        }
    }
}