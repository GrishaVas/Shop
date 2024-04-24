using System.Linq.Expressions;
using Shop.Infrastructure.Models;

namespace Shop.Infrastructure.Abstractions
{
    public interface IOrdersRepository
    {
        Task<Order> Create(Order order);
        Task Update(Order updateOrder);
        Task<Order> GetById(Guid id);
        Task<List<Order>> GetOrders(Expression<Func<Order, bool>> predicate = null);
        Task<Order> GetOrder(Expression<Func<Order, bool>> predicate);
        Task Delete(List<Guid> ids);
        IQueryable<Order> GetQuery();
    }
}
