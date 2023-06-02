using Microsoft.AspNetCore.Mvc;
using WeatherCenter.Dto.Widget;
using WeatherCenter.Interfaces;

namespace WeatherCenter.Controllers.Api
{
    [ApiController]
    [Route("api/widget")]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetService _widgetService;

        public WidgetController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        [HttpGet]
        public IActionResult GetWidgets()
        {
            var widgets = _widgetService.GetWeatherWidgets();

            return Ok(widgets);
          }

        [HttpPost]
        public async Task<IActionResult> CreateWidgetAsync([FromBody] CreateWeatherWidgetDto dto)
        {
            var result = await _widgetService.CreateWeatherWidget(dto);

            return Ok(result);
        }

        [HttpPatch]
        public IActionResult UpdateWidget([FromQuery(Name = "widget_id")] int widgetId, [FromQuery(Name="name")] string newName)
        {
                return _widgetService.UpdateWeatherWidget(widgetId, newName) ? Ok() : BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteWidget([FromQuery(Name = "widget_id")] int widgetId)
        {
            var result = _widgetService.DeleteWeatherWidget(widgetId);

            return result ? Ok() : BadRequest();
        }

    }
}
