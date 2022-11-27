using System;
namespace LordOfQuotes.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int RuntimeInMinutes { get; set; }
        public int BudgetInMillions { get; set; }
        public int BoxOfficeRevenueInMillions { get; set; }
        public int AcademyAwardNominations { get; set; }
        public int AcademyAwardWins { get; set; }
        public int RottenTomatoesScore { get; set; }

        public Movie()
        {
            
        }
    }
}
