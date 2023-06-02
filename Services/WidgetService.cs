using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeatherCenter.Dto.Autocomplete;
using WeatherCenter.Dto.Widget;
using WeatherCenter.Interfaces;
using WeatherCenter.Models.Entities;

namespace WeatherCenter.Services
{
    public class WidgetService : IWidgetService
    {
        private readonly IRepository<Widget> _widgetsRepository;
        private readonly ILocationsService _autocompleteService;

        public WidgetService(IRepository<Widget> widgetsRepository, ILocationsService autocompleteService)
        {
            _widgetsRepository = widgetsRepository;
            _autocompleteService = autocompleteService;
        }

        public async Task<GetWeatherWidgetResponseDto> CreateWeatherWidget(CreateWeatherWidgetDto dto)
        {
            var location = await _autocompleteService.GetCoordinates(dto.PlaceId);

            var secondaryInfo = dto.SecondPart.Split(", ");

            var country = secondaryInfo.Last();
            var region = secondaryInfo.SkipLast(1).FirstOrDefault();


            var entity = new Widget()
            {
                Name = dto.WidgetName,

                City = dto.City,
                Region = region,
                Country = country,
                FullLocation = dto.City + ", " + dto.SecondPart,

                UtcOffset = location.UtcOffset,
                Latitude = location.Latitude,
                Longtitude = location.Longtitude,

                CreatedAt = DateTime.UtcNow
            };

            _widgetsRepository.Add(entity);
            _widgetsRepository.SaveChanges();

            return new GetWeatherWidgetResponseDto()
            {
                WidgetId = entity.Id,
                WidgetName = entity.Name,
                FullLocation = entity.FullLocation,

                LocalTime = DateTime.UtcNow.AddMinutes(entity.UtcOffset).ToString("HH:mm")
            };
        }

        public bool DeleteWeatherWidget(int widgetId)
        {
            var entity = _widgetsRepository.Get(x => x.Id == widgetId).FirstOrDefault();

            if (entity == null) return false;

            _widgetsRepository.Remove(entity);

            return _widgetsRepository.SaveChanges() == 1;
        }

        public GetCoordinatesResult GetCoordinates(int widgetId)
        {
            var widget = _widgetsRepository.Get(x => x.Id == widgetId).FirstOrDefault();

            if(widget == null)
            {
                return new GetCoordinatesResult() { Exists = false };
            }

            return new GetCoordinatesResult()
            {
                Exists = true,
                Location = new Location()
                {
                    lat = widget.Latitude,
                    lng = widget.Longtitude
                }
            };
        }

        public IEnumerable<GetWeatherWidgetResponseDto> GetWeatherWidgets()
        {
            return _widgetsRepository.GetAll()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new GetWeatherWidgetResponseDto()
                {
                    WidgetId = x.Id,
                    WidgetName = x.Name,

                    LocalTime = DateTime.UtcNow.AddMinutes(x.UtcOffset).ToString("HH:mm"),
                    FullLocation = x.FullLocation,
                });

        }

        public bool UpdateWeatherWidget(int widgetId, string newName)
        {
            var entity = _widgetsRepository.Get(w => w.Id == widgetId).FirstOrDefault();

            if (entity == null) return false;

            entity.Name = newName;

            _widgetsRepository.Update(entity);
            var count = _widgetsRepository.SaveChanges();

            return count > 0;
        }
    }
}
