using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportsStore.DAL.Contexts;
using SportsStore.Models.ProductModels;

namespace SportsStore.DAL.Repos
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(IServiceProvider provider, IConfiguration config, ApplicationDbContext context)
            : base(provider, config)
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

        public void DeleteProductImages(IList<ProductImage> productImages)
        {
            if (productImages == null)
            return;

            foreach(var image in productImages)
            {
                var filePath = $"{_configuration["Directories:imageDirectory"]}{image.FileName}";
                File.Delete(filePath);

                ProductImage dbEntry = _context.ProductImages.FirstOrDefault(i => i.FileName == image.FileName && i.ProductId == i.ProductId);
                if(dbEntry != null)
                    _context.ProductImages.Remove(dbEntry);
            }

            int productId = productImages.First().ProductId;
            ProductImage newMainImage = _context.ProductImages.First(pi => pi.ProductId == productId && !productImages.Select(i => i.FileName).Contains(pi.FileName));
            if(newMainImage != null)
                newMainImage.IsMain = true;

            _context.SaveChanges();
        }

        public Product GetProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                for (int i = 0; i < 8 - product.ProductImages.Count; i++)
                {
                    product.ProductImages.Add(new ProductImage());
                }
            }
            
            return product;
        }

        public IEnumerable<Product> GetProducts(string category)
        {
            return Products.Where(p => p.Category == category);
        }

        public void SaveProduct(ProductEditViewModel model)
        {
            if (model.Product.ProductID == 0)
                _context.Products.Add(model.Product);
            else
            {
                Product dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == model.Product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = model.Product.Name;
                    dbEntry.Description = model.Product.Description;
                    dbEntry.Price = model.Product.Price;
                    dbEntry.Category = model.Product.Category;
                }
            }

            SaveProductImages(model);
            _context.SaveChanges();
        }

        private void SaveProductImages(ProductEditViewModel model)
        {
            foreach (var file in model.Files)
            {
                Random r = new Random();
                int randomValue = r.Next(1000000, 9999999);
                var filePath = _configuration["Directories:imageDirectory"] + $"p{model.Product.ProductID}_{randomValue}_{file.FileName}";
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        file.CopyTo(stream);
                }

                ProductImage image = model.Product.ProductImages.FirstOrDefault(pi => pi.FileName == file.FileName);
                if(image != null)
                {
                    if (image.ProductImageId == 0)
                    {
                        image.FileName = $"p{model.Product.ProductID}_{randomValue}_{image.FileName}";
                        _context.ProductImages.Add(image);
                    }
                    else
                    {
                        ProductImage dbEntry =
                            _context.ProductImages.FirstOrDefault(i => i.ProductImageId == image.ProductImageId);
                        if (dbEntry != null)
                        {
                            dbEntry.FileName = image.FileName;
                            dbEntry.ProductId = image.ProductId;
                            dbEntry.IsMain = image.IsMain;
                        }
                    }
                }
            }            
        }
    }
}
