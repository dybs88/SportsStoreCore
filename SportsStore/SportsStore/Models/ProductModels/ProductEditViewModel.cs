using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ProductModels
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
