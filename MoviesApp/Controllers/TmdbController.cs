using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services;

namespace MoviesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TmdbController : ControllerBase
    {
        private readonly TmdbService _tmdbService;

        public TmdbController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [HttpGet("authenticate")]
        public async Task<IActionResult> Authenticate()
        {
            var result = await _tmdbService.AuthenticateAsync();
            return Ok(result); // Return the TMDb API response
        }
    }
}
