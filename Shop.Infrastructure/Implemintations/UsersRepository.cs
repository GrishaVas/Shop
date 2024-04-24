using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Abstractions;
using Shop.Services.Models;

namespace Shop.Infrastructure.Implemintations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ShopDbContext _dbContext;

        public UsersRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Create(User user)
        {
            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }

        public Task<User> GetUser(Expression<Func<User, bool>> predicate)
        {
            return _dbContext.Users.FirstOrDefaultAsync(predicate);
        }
    }
}
