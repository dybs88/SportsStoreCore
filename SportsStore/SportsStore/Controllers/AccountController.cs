using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Controllers.Base;
using SportsStore.Domain;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models.Account;
using SportsStore.Models.Cart;
using SportsStore.Models.Identity;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private UserManager<SportUser> _userManager;
        private SignInManager<SportUser> _signInManager;
        private Cart _cart;

        public AccountController(UserManager<SportUser> userManager, SignInManager<SportUser> signInManager, Cart cart)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cart = cart;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel {ReturnUrl = returnUrl};
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                SportUser user = await _userManager.FindByNameAsync(model.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, IdentityRoleNames.Employees))
                            return Redirect("/User/Index");
                        else
                            return RedirectToAction("Index","Customer", new { customerId = user.CustomerId});
                    }
                }
            }

            ModelState.AddModelError("","Nieprawidłowa nazwa użytkownika lub hasło");
            return View(model);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            _cart.ClearCart();
            return Redirect(returnUrl);
        }
    }
}