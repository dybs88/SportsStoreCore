using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Helpers;

namespace SportsStore.Models.ProductModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageHelper PageHelper { get; set; }
        public string CurrentCategory { get; set; }

        public ProductListViewModel(IEnumerable<Product> products, int totalItems, int pageSize, int currentPage, string currentCategory)
        {
            Products = products;
            CurrentCategory = currentCategory;

            PageHelper = new PageHelper
            {
                ItemPerPage = pageSize,
                CurrentPage = currentPage,
                TotalItems = totalItems
            };
        }
    }
}
