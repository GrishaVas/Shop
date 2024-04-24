using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shop.MVC.Models;
using Shop.Services.Abstractions;
using Shop.Services.Models;

namespace Shop.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IHttpContextAccessor _context;

        public AuthController(IUsersService usersService, IHttpContextAccessor context)
        {
            _usersService = usersService;
            _context = context;
        }

        public IActionResult Login([FromQuery] string returnUrl)
        {
            return View();
        }

        public IActionResult Registration([FromQuery] string returnUrl)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            try
            {
                var user = new UserDto
                {
                    Name = loginModel.Name,
                    Password = loginModel.Password
                };

                var chekedUser = await _usersService.Login(user);

                if (chekedUser != null)
                {
                    await SignIn(chekedUser, loginModel.RememberLogin);
                }
            }
            catch (ArgumentException ex)
            {
                TempData["exception"] = ex.Message;

                return Redirect("Login");
            }


            return Redirect(loginModel.returnUrl ?? "/");
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountViewModel creaetAccountModel)
        {
            try
            {
                var user = new UserDto
                {
                    Name = creaetAccountModel.Name,
                    Password = creaetAccountModel.Password
                };

                var createdUser = await _usersService.Create(user);

                await SignIn(createdUser, creaetAccountModel.RememberLogin);
            }
            catch (ArgumentException ex)
            {
                TempData["exception"] = ex.Message;

                return Redirect("Registration");
            }

            return Redirect(creaetAccountModel.returnUrl ?? "/");
        }

        private async Task SignIn(User user, bool RememberLogin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var props = new AuthenticationProperties();

            if (RememberLogin)
            {
                props.IsPersistent = true;
                props.ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30));
            };

            await _context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), props);
        }
    }
}
