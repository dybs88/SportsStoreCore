using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Cart;
using SportsStore.Models.ComponentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Components
{
    public class QuickMenuViewComponent : ViewComponent
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private Cart _cart;

        public QuickMenuViewComponent(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, Cart cart)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cart = cart;
        }

        public IViewComponentResult Invoke(string returnUrl)
        {
            bool isUserLogged = false;
            var user = _userManager.GetUserAsync(HttpContext.User);
            if (user.Result != null)
                isUserLogged = true;

            QuickMenuViewModel model = new QuickMenuViewModel
            {
                IsUserLogged = isUserLogged,
                Cart = _cart,
                LoggedUser = user.Result,
                ReturnUrl = returnUrl
            };

            return View(model);
        }
    }
}
