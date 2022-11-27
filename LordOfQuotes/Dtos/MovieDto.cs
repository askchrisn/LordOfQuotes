using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LordOfQuotes.Dtos
{
    [JsonObject(Title = "Doc")]
    public class MovieDto
    {
        public string _id { get; set; }
        public string name { get; set; }
        public int runtimeInMinutes { get; set; }
        public int budgetInMillions { get; set; }
        public int boxOfficeRevenueInMillions { get; set; }
        public int academyAwardNominations { get; set; }
        public int academyAwardWins { get; set; }
        public int rottenTomatoesScore { get; set; }
    }

    [JsonObject(Title = "Root")]
    public class MovieListDto
    {
        public List<MovieDto> docs { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
    }

}
