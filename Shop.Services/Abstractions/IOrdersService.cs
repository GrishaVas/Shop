using System.Linq.Expressions;
using Shop.Infrastructure.Models;
using Shop.Services.Models;

namespace Shop.Services.Abstractions
{
    public interface IOrdersService
    {
        Task<Order> Create(CreateOrderDto createOrder);
        Task Update(UpdateOrderDto updateOrder);
        Task<Order> GetById(Guid id);
        Task<List<Order>> GetOrders(Expression<Func<Order, bool>> predicate = null);
        Task Delete(List<int> numbers);
    }
}
