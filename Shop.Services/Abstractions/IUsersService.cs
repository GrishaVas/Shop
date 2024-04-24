using Shop.Services.Models;

namespace Shop.Services.Abstractions
{
    public interface IUsersService
    {
        Task<User> Create(UserDto createUser);
        string GetCurrentUserName();
        Task<User> GetByName(string name);
        Task<User> Login(UserDto user);
    }
}
