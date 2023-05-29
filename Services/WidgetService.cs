using Microsoft.EntityFrameworkCore;
using WeatherCenter.Dto.Widget;
using WeatherCenter.Interfaces;
using WeatherCenter.Models.Entities;

namespace WeatherCenter.Services
{
    public class WidgetService : IWidgetService
    {
        private readonly IRepository<Widget> _widgetsRepository;
        private readonly IAutocompleteService _autocompleteService;

        public WidgetService(IRepository<Widget> widgetsRepository, IAutocompleteService autocompleteService)
        {
            _widgetsRepository = widgetsRepository;
            _autocompleteService = autocompleteService;
        }

        public async Task<bool> CreateWeatherWidget(CreateWeatherWidgetDto dto)
        {
            var coords = await _autocompleteService.GetCoordinates(dto.PlaceId);

            var secondaryInfo = dto.SecondPart.Split(", ");

            var country = secondaryInfo.Last();
            var region = secondaryInfo.SkipLast(1).FirstOrDefault();



            var entity = new Widget()
            {
                Name = dto.WidgetName,
                City = dto.City,
                Region = region,
                Country = country,

                Latitude = coords.lat,
                Longtitude = coords.lng
            };

            _widgetsRepository.Add(entity);

            return _widgetsRepository.SaveChanges() == 1;
        }

        public bool DeleteWeatherWidget(int widgetId)
        {
            _widgetsRepository.Remove(new Widget() { Id = widgetId });

            return _widgetsRepository.SaveChanges() == 1;
        }

        public IEnumerable<GetWeatherWidgetDto> GetWeatherWidgets()
        {
            return _widgetsRepository.GetAll().Select(x => new GetWeatherWidgetDto()
            {
                WidgetId = x.Id,
                WidgetName = x.Name,

                Latitude = x.Latitude,
                Longtitude = x.Longtitude,

                FullLocation = GetFullLocation(x),
            });

        }

        private string GetFullLocation(Widget widget)
        {
            if(widget.Region != null)
            {
                return widget.City + ", " + widget.Region + ", " + widget.Country;
            }

            return widget.City + ", " + widget.Country;
        }
    }
}
