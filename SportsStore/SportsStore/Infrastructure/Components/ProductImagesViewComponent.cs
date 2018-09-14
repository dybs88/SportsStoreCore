using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ComponentViewModels;
using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Components
{
    public class ProductImagesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Product product, bool isEdit)
        {
            return View(new ProductImagesViewModel { Product = product, IsEdit = isEdit });
        }
    }
}
