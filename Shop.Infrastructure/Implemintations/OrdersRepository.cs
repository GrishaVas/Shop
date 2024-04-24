using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Abstractions;
using Shop.Infrastructure.Models;

namespace Shop.Infrastructure.Implemintations
{
    public class OrdersRepository : IOrdersRepository
    {
        public readonly ShopDbContext _dbContext;

        public OrdersRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Create(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task Delete(List<Guid> ids)
        {
            var orders = await _dbContext.Orders.Where(o => ids.Contains(o.Id)).ToArrayAsync();

            foreach (var order in orders)
            {
                _dbContext.Orders.Remove(order);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            return order;
        }

        public Task<List<Order>> GetOrders(Expression<Func<Order, bool>> predicate = null)
        {
            var query = _dbContext.Orders.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToListAsync();
        }

        public Task<Order> GetOrder(Expression<Func<Order, bool>> predicate)
        {
            return _dbContext.Orders.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<Order> GetQuery()
        {
            return _dbContext.Orders.AsQueryable();
        }

        public async Task Update(Order updateOrder)
        {
            var order = await _dbContext.Orders.FindAsync(updateOrder.Id);

            if (order != null)
            {
                _dbContext.Orders.Update(order);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
