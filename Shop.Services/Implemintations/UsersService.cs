using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Shop.Infrastructure.Abstractions;
using Shop.Services.Abstractions;
using Shop.Services.Models;

namespace Shop.Services.Implemintations
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _context;

        public UsersService(IUsersRepository usersRepository, IHttpContextAccessor Context)
        {
            _usersRepository = usersRepository;
            _context = Context;
        }

        public async Task<User> Login(UserDto user)
        {
            var userFromDb = await _usersRepository.GetUser(u => u.Name == user.Name);

            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Fill in all input fields!");
            }

            if (userFromDb == null)
            {
                throw new ArgumentException("User does not exists!");
            }

            if (userFromDb.Password != Sha256(user.Password))
            {
                throw new ArgumentException("Wrong password!");
            }

            return userFromDb;
        }

        public async Task<User> Create(UserDto createUser)
        {
            if (string.IsNullOrEmpty(createUser.Name) || string.IsNullOrEmpty(createUser.Password))
            {
                throw new ArgumentException("Fill in all input fields!");
            }

            var user = await _usersRepository.GetUser(u => u.Name == createUser.Name);

            if (user != null)
            {
                throw new ArgumentException($"User with name: {createUser.Name} already exists!");
            }

            user = new User
            {
                Name = createUser.Name,
                Password = Sha256(createUser.Password)
            };

            await _usersRepository.Create(user);

            return user;
        }

        public Task<User> GetByName(string name)
        {
            return _usersRepository.GetUser(u => u.Name == name);
        }

        public string GetCurrentUserName()
        {
            var user = _context.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != null && user?.Identity?.IsAuthenticated == true)
            {
                return user.Claims
                    .SingleOrDefault(c => c.Type == ClaimTypes.Name)?
                    .Value;
            }

            return null;
        }

        private string Sha256(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
