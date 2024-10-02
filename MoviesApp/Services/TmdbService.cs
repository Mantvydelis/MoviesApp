using MoviesApp.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MoviesApp.Services
{
    public class TmdbService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://api.themoviedb.org/3";
        private const string ApiKey = "377f2c6b1dc000572573b6578e0497d4";

        public TmdbService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
        }

        public async Task<string> GetPopularMoviesAsync()
        {
            var requestUri = $"{BaseUrl}/movie/popular?api_key={ApiKey}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri)
            };

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> GetMoviesByGenre(int genreId)
        {
            var requestUri = $"{BaseUrl}/discover/movie?api_key={ApiKey}&with_genres={genreId}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri)
            };

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> GetRandomMovieByGenre(int genreId)
        {
            var moviesJson = await GetMoviesByGenre(genreId);

            Console.WriteLine(moviesJson);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var movies = JsonSerializer.Deserialize<TmdbMovieResponse>(moviesJson, options);

            if (movies?.Results == null || movies.Results.Count == 0)
                return "No movies found for this genre.";

            var random = new Random();
            var randomMovie = movies.Results[random.Next(movies.Results.Count)];

            return randomMovie.Title;
        }
    }


}

