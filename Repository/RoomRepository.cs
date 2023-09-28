using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var rooms = _context.Rooms;
            var cities = _context.Cities;
            var hotels = _context.Hotels;

            var allRooms = from room in rooms
                           join hotel in hotels on room.HotelId equals hotel.HotelId
                           join city in cities on hotel.CityId equals city.CityId
                           where room.HotelId == HotelId
                           select new RoomDto
                           {
                                RoomId = room.RoomId,
                                Name = room.Name,
                                Capacity = room.Capacity,
                                Image = room.Image,
                                Hotel = new HotelDto
                                {
                                    HotelId = hotel.HotelId,
                                    Name = hotel.Name,
                                    Address = hotel.Address,
                                    CityId = city.CityId,
                                    CityName = city.Name,
                                    State = city.State
                                }
                            };

            return allRooms;
        }

        public RoomDto AddRoom(Room room) {
            var allRooms = _context.Rooms;
            allRooms.Add(room);
            _context.SaveChanges();

            var cities = _context.Cities;
            var hotels = _context.Hotels;

            var newRoom = from rooms in allRooms
                            join hotel in hotels on rooms.HotelId equals hotel.HotelId
                            join city in cities on hotel.CityId equals city.CityId
                            where rooms.RoomId == room.RoomId
                            select new RoomDto
                            {
                                RoomId = rooms.RoomId,
                                Name = rooms.Name,
                                Capacity = rooms.Capacity,
                                Image = rooms.Image,
                                Hotel = new HotelDto
                                {
                                    HotelId = hotel.HotelId,
                                    Name = hotel.Name,
                                    Address = hotel.Address,
                                    CityId = city.CityId,
                                    CityName = city.Name,
                                    State = city.State
                                }
                            };

            return newRoom.First();
        }

        public void DeleteRoom(int RoomId) {
            var rooms = _context.Rooms;

            var room = rooms.Find(RoomId) ?? throw new System.Exception();
            
            rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}