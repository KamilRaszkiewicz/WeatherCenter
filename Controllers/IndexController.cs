using Microsoft.AspNetCore.Mvc;
using WeatherCenter.Dto.Widget;
using WeatherCenter.Interfaces;

namespace WeatherCenter.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {
        private readonly IWidgetService _widgetService;

        public IndexController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_widgetService.GetWeatherWidgets());
        }
    }
}
