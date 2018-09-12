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
        public IViewComponentResult Invoke(IList<ProductImage> productImages, int productId, bool isEdit)
        {
            return View(new ProductImagesViewModel { ProductId = productId, ProductImages = productImages, IsEdit = isEdit });
        }
    }
}
