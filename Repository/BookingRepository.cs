using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var bookings = _context.Bookings;
            var rooms = _context.Rooms;
            var users = _context.Users;
            var hotels = _context.Hotels;
            var cities = _context.Cities;

            var capacityRoom = (from room in rooms
                                where room.RoomId == booking.RoomId
                                select room.Capacity).First();

            if (capacityRoom < booking.GuestQuant)
            {
                return new BookingResponse {
                    BookingId = 0
                };
            }

            var userLINQ = (from user in users
                            where user.Email == email
                            select user).First();

            var newBooking = new Booking
                {
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    GuestQuant = booking.GuestQuant,
                    UserId = userLINQ.UserId,
                    RoomId = booking.RoomId
                };

            bookings.Add(newBooking);
            _context.SaveChanges();

            var response = (from b in bookings
                            join r in rooms on b.RoomId equals r.RoomId
                            join h in hotels on r.HotelId equals h.HotelId
                            join c in cities on h.CityId equals c.CityId
                            where b.BookingId == newBooking.BookingId
                            select new BookingResponse
                            {
                                BookingId = b.BookingId,
                                CheckIn = b.CheckIn,
                                CheckOut = b.CheckOut,
                                GuestQuant = b.GuestQuant,
                                Room = new RoomDto
                                {
                                    RoomId = b.RoomId,
                                    Name = r.Name,
                                    Capacity = r.Capacity,
                                    Image = r.Image,
                                    Hotel = new HotelDto
                                    {
                                        HotelId = r.HotelId,
                                        Name = h.Name,
                                        Address = h.Address,
                                        CityId = c.CityId,
                                        CityName = c.Name,
                                        State = c.State
                                    }
                                }
                            }).First();

            return response;
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var bookings = _context.Bookings;
            var rooms = _context.Rooms;
            var users = _context.Users;
            var hotels = _context.Hotels;
            var cities = _context.Cities;

            var userLINQ = (from user in users
                            where user.Email == email
                            select user.UserId).First();

            var bookingLINQ = (from booking in bookings
                                where booking.BookingId == bookingId
                                select booking.UserId).First();

            if (userLINQ != bookingLINQ)
            {
                return new BookingResponse {
                    BookingId = 0
                };
            }

            var response = (from b in bookings
                            join r in rooms on b.RoomId equals r.RoomId
                            join h in hotels on r.HotelId equals h.HotelId
                            join c in cities on h.CityId equals c.CityId
                            where b.BookingId == bookingId
                            select new BookingResponse
                            {
                                BookingId = b.BookingId,
                                CheckIn = b.CheckIn,
                                CheckOut = b.CheckOut,
                                GuestQuant = b.GuestQuant,
                                Room = new RoomDto
                                {
                                    RoomId = b.RoomId,
                                    Name = r.Name,
                                    Capacity = r.Capacity,
                                    Image = r.Image,
                                    Hotel = new HotelDto
                                    {
                                        HotelId = r.HotelId,
                                        Name = h.Name,
                                        Address = h.Address,
                                        CityId = c.CityId,
                                        CityName = c.Name,
                                        State = c.State
                                    }
                                }
                            }).First();

            return response;
        }

        public Room GetRoomById(int RoomId)
        {
             throw new NotImplementedException();
        }

    }

}