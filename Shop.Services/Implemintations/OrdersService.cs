using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Abstractions;
using Shop.Infrastructure.Models;
using Shop.Services.Abstractions;
using Shop.Services.Models;

namespace Shop.Services.Implemintations
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUsersService _usersService;

        public OrdersService(IOrdersRepository ordersRepository, IUsersService usersService)
        {
            _ordersRepository = ordersRepository;
            _usersService = usersService;
        }

        public async Task<Order> Create(CreateOrderDto createOrder)
        {
            if (String.IsNullOrEmpty(createOrder.Name))
            {
                throw new ArgumentException("Name cannot be null!");
            }

            var order = new Order
            {
                Name = createOrder.Name,
                CreatedBy = _usersService.GetCurrentUserName()
            };

            if (createOrder.Number != null)
            {
                var isOrderExists = await _ordersRepository.GetQuery().AnyAsync(o => o.Number == createOrder.Number);

                if (isOrderExists)
                {
                    throw new ArgumentException($"Order with number: {createOrder.Number} already exists!");
                }

                order.Number = (int)createOrder.Number;
            }

            await _ordersRepository.Create(order);

            return order;
        }

        public async Task Delete(List<int> numbers)
        {
            var ids = await _ordersRepository.GetQuery()
                .Where(o => numbers.Contains((int)o.Number))
                .Select(o => o.Id)
                .ToListAsync();

            await _ordersRepository.Delete(ids);
        }

        public Task<Order> GetById(Guid id)
        {
            return _ordersRepository.GetById(id);
        }

        public Task<List<Order>> GetOrders(Expression<Func<Order, bool>> predicate = null)
        {
            return _ordersRepository.GetOrders(predicate);
        }

        public async Task Update(UpdateOrderDto updateOrder)
        {
            if (String.IsNullOrEmpty(updateOrder.Name) || updateOrder.Number == null)
            {
                throw new ArgumentException("Fill in all input fields!");
            }

            var order = await _ordersRepository.GetOrder(o => o.Number == updateOrder.Number);

            if (order != null)
            {
                order.Name = updateOrder.Name;

                await _ordersRepository.Update(order);
            }
        }
    }
}
