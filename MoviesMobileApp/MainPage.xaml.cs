using MoviesApp.Models;
using System.Text.Json;


namespace MoviesMobileApp
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "377f2c6b1dc000572573b6578e0497d4";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGetMovieClicked(object sender, EventArgs e)
        {
            var selectedGenre = genrePicker.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedGenre))
            {
                movieLabel.Text = "Please select a genre.";
                return;
            }

            try
            {
                int genreId = GetGenreId(selectedGenre);  // Get the correct genre ID for TMDB
                string requestUrl = $"https://api.themoviedb.org/3/discover/movie?api_key={ApiKey}&with_genres={genreId}";

                var response = await _httpClient.GetStringAsync(requestUrl);

                var movies = JsonSerializer.Deserialize<TmdbMovieResponse>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (movies?.Results != null && movies.Results.Count > 0)
                {
                    var random = new Random();
                    var randomMovie = movies.Results[random.Next(movies.Results.Count)];
                    movieLabel.Text = $"Random Movie: {randomMovie.Title}";
                }
                else
                {
                    movieLabel.Text = "No movies found for this genre.";
                }
            }
            catch (Exception ex)
            {
                movieLabel.Text = $"Error fetching movie: {ex.Message}";
            }
        }

        private int GetGenreId(string genreName)
        {
            return genreName.ToLower() switch
            {
                "action" => 28,
                "adventure" => 12,
                "animation" => 16,
                "comedy" => 35,
                "crime" => 80,
                "documentary" => 99,
                "drama" => 18,
                "family" => 10751,
                "fantasy" => 14,
                "history" => 36,
                "horror" => 27,
                "music" => 10402,
                "mystery" => 9648,
                "romance" => 10749,
                "science fiction" => 878,
                "thriller" => 53,
                "war" => 10752,
                "western" => 37,
                _ => 0


            };
        }


    }
}
