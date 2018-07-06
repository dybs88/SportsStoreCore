using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        Product DeleteProduct(int productId);

        void SaveProduct(Product product);
    }
}
