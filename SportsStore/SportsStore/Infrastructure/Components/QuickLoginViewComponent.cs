﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Account;

namespace SportsStore.Infrastructure.Components
{
    public class QuickLoginViewComponent : ViewComponent
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public QuickLoginViewComponent(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke(string returnUrl)
        {
            bool isUserLogged = false;
            var user = _userManager.GetUserAsync(HttpContext.User);
            if (user.Result != null)
                isUserLogged = true;

            LoginModel model = new LoginModel{LoggedUser = user.Result, IsUserLogged = isUserLogged, ReturnUrl = returnUrl};
            return View(model);
        }
    }
}
