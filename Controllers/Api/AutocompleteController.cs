using Microsoft.AspNetCore.Mvc;
using WeatherCenter.Interfaces;

namespace WeatherCenter.Controllers.Api
{
    [ApiController]
    [Route("api/autocomplete")]
    public class AutocompleteController : ControllerBase
    {
        private readonly ILocationsService _autocompleteService;

        public AutocompleteController(ILocationsService autocompleteService)
        {
            _autocompleteService = autocompleteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAutocompletes([FromQuery(Name = "q")] string query)
        {
            var result = await _autocompleteService.GetAutocompletes(query);

            return Ok(result);
        }
    }
}
