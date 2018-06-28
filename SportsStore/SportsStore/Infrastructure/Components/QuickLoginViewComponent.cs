using System;
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
            LoginModel model = new LoginModel{ReturnUrl = returnUrl};
            return View(model);
        }
    }
}
