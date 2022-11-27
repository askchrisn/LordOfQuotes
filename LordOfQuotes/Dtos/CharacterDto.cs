using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LordOfQuotes.Dtos
{
    [JsonObject(Title = "Doc")]
    public class CharacterDto
    {
        public string _id { get; set; }
        public string height { get; set; }
        public string race { get; set; }
        public string gender { get; set; }
        public string birth { get; set; }
        public string spouse { get; set; }
        public string death { get; set; }
        public string realm { get; set; }
        public string hair { get; set; }
        public string name { get; set; }
        public string wikiUrl { get; set; }
    }

    [JsonObject(Title = "Root")]
    public class CharacterListDto
    {
        public List<CharacterDto> docs { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
    }
}
