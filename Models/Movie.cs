namespace MyMovieLibrary.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; } // Оценка от 1 до 10
    }
}