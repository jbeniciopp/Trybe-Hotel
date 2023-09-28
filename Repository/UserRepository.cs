using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var users = _context.Users;

            var query = from user in users
                        where user.Email == login.Email
                        select user;

            if (query.First().Password != login.Password)
            {
                return new UserDto
                            {
                                UserId = 0,
                                Name = "Null",
                                Email = "Null",
                                UserType = "Null"
                            };
            }

            return new UserDto
                        {
                            UserId = query.First().UserId,
                            Name = query.First().Name,
                            Email = query.First().Email,
                            UserType = query.First().UserType
                        };
        }
        public UserDto Add(UserDtoInsert user)
        {
            var users = _context.Users;

            User newUser = new()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            };

            users.Add(newUser);
            _context.SaveChanges();

            UserDto response = new()
            {
                UserId = newUser.UserId,
                Name = newUser.Name,
                Email = newUser.Email,
                UserType = newUser.UserType
            };

            return response;
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            var users = _context.Users;

            var query = from user in users
                        where user.Email == userEmail
                        select new UserDto
                        {
                            UserId = user.UserId,
                            Name = user.Name,
                            Email = user.Email,
                            UserType = user.UserType
                        };

            if (!query.Any())
            {
                return new UserDto
                            {
                                UserId = 0
                            };
            }

            Console.WriteLine(query.Count());

            return query.FirstOrDefault();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _context.Users;

            var response = from user in users
                            select new UserDto {
                                UserId = user.UserId,
                                Name = user.Name,
                                Email = user.Email,
                                UserType = user.UserType
                            };

            return response;
        }

    }
}