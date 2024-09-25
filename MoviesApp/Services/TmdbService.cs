using System.Net.Http.Headers;

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

        public async Task<string> AuthenticateAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{BaseUrl}/authentication/token/new")
            };

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
