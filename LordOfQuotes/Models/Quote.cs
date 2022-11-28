using System;
using LordOfQuotes.Dtos;
using Newtonsoft.Json;

namespace LordOfQuotes.Models
{
    public class Quote
    {
        public string Id { get; set; }
        public string Dialog { get; set; }
        public string Movie { get; set; }
        public string Character { get; set; }

        public Quote()
        {

        }

        public Quote(QuoteDto dto)
        {
            Id = dto.id;
            Dialog = $"'{dto.dialog}'";
            Movie = dto.movie;
            Character = dto.character;
        }
    }
}