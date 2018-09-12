using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ComponentViewModels
{
    public class ProductImagesViewModel
    {
        public int ProductId { get; set; }
        public IList<ProductImage> ProductImages { get; set; }
        public bool IsEdit { get; set; }
    }
}
