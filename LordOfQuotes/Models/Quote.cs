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

        public Quote(Doc doc)
        {
            Id = doc.id;
            Dialog = doc.dialog;
            Movie = doc.movie;
            Character = doc.character;
        }
    }
}