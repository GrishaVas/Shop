using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.MVC.Models;
using Shop.Services.Abstractions;
using Shop.Services.Models;

namespace Shop.MVC.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IUsersService _userService;

        public OrdersController(IOrdersService ordersService, IUsersService userService)
        {
            _ordersService = ordersService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _ordersService.GetOrders();
            ViewData["CurrentUser"] = _userService.GetCurrentUserName();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderDto order)
        {
            try
            {
                await _ordersService.Create(order);
            }
            catch (ArgumentException ex)
            {

                TempData["exception"] = ex.Message;
            }


            return Redirect("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(List<int> numbers)
        {
            await _ordersService.Delete(numbers);

            return Redirect("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Change(UpdateOrderDto order)
        {
            try
            {
                await _ordersService.Update(order);
            }
            catch (ArgumentException ex)
            {

                TempData["exception"] = ex.Message;
            }

            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
