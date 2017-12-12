using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShBazmool.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ShBazmool.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShBazmoolDbContext dbContext;
        protected UserManager userManager;

        public AccountController(ShBazmoolDbContext context)
        {
            dbContext = context;
            userManager = new UserManager(dbContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            //compare user account from database
            if (!userManager.IsValid(user.UserName, user.Password))
                return View();

            //Create Claims
            List<Claim> claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, "Admin"),
                 new Claim(ClaimTypes.Email, user.UserName)
            };

            //Create Scheme
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            //Create Identity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, scheme);

            //Create Principal
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(scheme, principal);
            // SignIn(principal, scheme);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            await HttpContext.SignOutAsync(scheme);
            return RedirectToAction("Login");
        }

    }
}
