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
    }
}
