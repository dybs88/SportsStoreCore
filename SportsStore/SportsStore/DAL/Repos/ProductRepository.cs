using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Contexts;
using SportsStore.Models.ProductModels;

namespace SportsStore.DAL.Repos
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => 
            _context.Products
                .Include(p => p.ProductImages);

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }

        public Product GetProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductID == productId);
            for(int i = 0; i < 8 - product.ProductImages.Count; i++ )
            {
                product.ProductImages.Add(new ProductImage());
            }

            return product;
        }

        public IEnumerable<Product> GetProducts(string category)
        {
            return Products.Where(p => p.Category == category);
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }
    }
}
