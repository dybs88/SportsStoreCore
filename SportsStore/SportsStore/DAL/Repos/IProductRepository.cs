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
        void DeleteProductImages(IList<ProductImage> productImages);
        Product GetProduct(int productId);
        IEnumerable<Product> GetProducts(string category);
        void SaveProduct(ProductEditViewModel product);
    }
}
