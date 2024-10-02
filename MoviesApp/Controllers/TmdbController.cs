using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services;

namespace MoviesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TmdbController : ControllerBase
    {
        private readonly TmdbService _movieService;

        public TmdbController(TmdbService tmdbService)
        {
            _movieService = tmdbService;
        }


        [HttpGet("popular movies")]
        public async Task<IActionResult> GetPopularMoviesAsync()
        {
            var result = await _movieService.GetPopularMoviesAsync();
            return Ok(result);
        }


        [HttpGet("movie by genre")]
        public async Task<IActionResult> GetMovieByGenre(int genreId)
        {
            var result = await _movieService.GetMoviesByGenre(genreId);
            return Ok(result);
        }

        [HttpGet("random movie by genre")]
        public async Task<IActionResult> GetRandomMovieByGenre(int genreId)
        {
            var result = await _movieService.GetRandomMovieByGenre(genreId);
            return Ok(result);
        }


    }
}
