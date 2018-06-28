using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure.Components
{
    public class AdminNavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            AdminNavigationMenuViewModel model = new AdminNavigationMenuViewModel();
            return View(model);
        }
    }
}
