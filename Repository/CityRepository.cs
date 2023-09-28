using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<CityDto> GetCities()
        {
            var cities = _context.Cities;
            var allCities = from city in _context.Cities 
                            select new CityDto {
                                CityId = city.CityId,
                                Name = city.Name,
                                State = city.State
                            };

            return allCities;
        }

        public CityDto AddCity(City city)
        {
            var addCity = _context.Cities.Add(city);
            _context.SaveChanges();

            var cityDto = new CityDto {
                CityId = addCity.Entity.CityId, 
                Name = addCity.Entity.Name,
                State = addCity.Entity.State
            };

            return cityDto;
        }

        public CityDto UpdateCity(City city)
        {
            var updtCity = _context.Cities.Update(city);
            _context.SaveChanges();

            var cityDto = new CityDto {
                CityId = updtCity.Entity.CityId,
                Name = updtCity.Entity.Name,
                State = updtCity.Entity.State,
            };

            return cityDto;
        }

    }
}