using Microsoft.AspNetCore.Mvc;
using WeatherCenter.Interfaces;

namespace WeatherCenter.Controllers.Api
{

    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IWidgetService _widgetService;
        private readonly IWeatherService _weatherService;

        public WeatherController(IWidgetService widgetService, IWeatherService weatherService)
        {
            _widgetService = widgetService;
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentWeather([FromQuery(Name = "widget_id")] int WidgetId)
        {
            var coords = _widgetService.GetCoordinates(WidgetId);
            if (!coords.Exists) return BadRequest();

            var currentWeather = await _weatherService.GetCurrentWeather(coords.Location.lat, coords.Location.lng);

            if (!currentWeather.IsSuccess) return BadRequest();

            return Ok(currentWeather);
        }
    }
}
