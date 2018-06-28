using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            _repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            
            var categories = _repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x);

            NavigationMenuViewModel model = new NavigationMenuViewModel(categories, (string)RouteData?.Values["category"]);

            return View(model);
        }
    }
}
