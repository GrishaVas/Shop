using System.Linq.Expressions;
using Shop.Services.Models;

namespace Shop.Infrastructure.Abstractions
{
    public interface IUsersRepository
    {
        Task<User> Create(User user);
        Task<User> GetUser(Expression<Func<User, bool>> predicate);
    }
}
