using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LordOfQuotes.Dtos
{
    [JsonObject(Title = "Doc")]
    public class QuoteDto
    {
        public string _id { get; set; }
        public string dialog { get; set; }
        public string movie { get; set; }
        public string character { get; set; }
        public string id { get; set; }
    }

    [JsonObject(Title = "Root")]
    public class QuoteListDto
    {
        public List<QuoteDto> docs { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
    }
}
