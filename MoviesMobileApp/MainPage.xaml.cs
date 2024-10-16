using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace MoviesMobileApp
{
    public partial class MainPage : ContentPage
    {
        HttpClient client = new HttpClient();

        public MainPage()
        {
            InitializeComponent(); 
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string genre = genrePicker.SelectedItem?.ToString(); 

            if (genre == null)
            {
                movieLabel.Text = "Please select a genre."; 
                return;
            }

            try
            {
                string apiUrl = $"https://localhost:7285/api/Tmdb/random-movie-by-genre?genreName={genre}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var movieData = JObject.Parse(content);
                    string movieTitle = movieData["title"].ToString();
                    movieLabel.Text = $"Random Movie: {movieTitle}";
                }
                else
                {
                    movieLabel.Text = "Failed to retrieve movie.";
                }
            }
            catch (Exception ex)
            {
                movieLabel.Text = $"Error: {ex.Message}";
            }
        }
    }
}
