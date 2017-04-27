using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using paylive.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace paylive.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User userFromFore)
        {
            var userFromStorage = TestUserStorage.UserList
                .FirstOrDefault(m => m.UserName == userFromFore.UserName && m.Password == userFromFore.Password);

            if (userFromStorage != null)
            {
                //you can add all of ClaimTypes in this collection
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,userFromStorage.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                    //,new Claim(ClaimTypes.Email,"emailaccount@microsoft.com") 
                };

                //init the identity instances
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SuperSecureLogin"));

                //signin
                await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

                return RedirectToAction("Index", "System");
            }
            else
            {
                ViewBag.ErrMsg = "UserName or Password is invalid";

                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");

            return RedirectToAction("Index", "System");
        }
    }

    //for simple, I'm not using the database to store the user data, just using a static class to replace it.
    public static class TestUserStorage
    {
        public static List<User> UserList { get; set; } = new List<User>() {
            new User { UserName = "admin",Password = "admin"}
        };
    }
}
