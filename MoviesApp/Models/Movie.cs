namespace MoviesApp.Models
{
    public class Movie
    {
        public string Title { get; set; }

    }

    public class TmdbMovieResponse
    {
        public List<Movie> Results { get; set; }
    }


}
