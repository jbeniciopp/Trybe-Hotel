using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            var hotels = _context.Hotels;
            var cities = _context.Cities;
            var allHotels = from hotel in _context.Hotels
                            join city in cities on hotel.CityId equals city.CityId
                            select new HotelDto {
                                HotelId = hotel.HotelId,
                                Name = hotel.Name,
                                Address = hotel.Address,
                                CityId = hotel.CityId,
                                CityName = city.Name,
                                State = city.State
                            };

            return allHotels;
        }

        public HotelDto AddHotel(Hotel hotel)
        {
           var hotels = _context.Hotels;
            var cities = _context.Cities;
            hotels.Add(hotel);
            _context.SaveChanges();

            var hotelDto =  from linqHotel in hotels
                            join city in cities on linqHotel.CityId equals city.CityId
                            where linqHotel.HotelId == hotel.HotelId
                            select new HotelDto {
                                HotelId = hotel.HotelId,
                                Name = hotel.Name,
                                Address = hotel.Address,
                                CityId = city.CityId,
                                CityName = city.Name,
                                State = city.State
                            };

            return hotelDto.First();
        }
    }
}