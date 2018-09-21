using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportsStore.DAL.Contexts;
using SportsStore.Models.ProductModels;
using SportsStore.Infrastructure.Extensions;

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
            if (productImages == null && productImages.Count == 0)
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
            ProductImage newMainImage = _context.ProductImages.FirstOrDefault(pi => pi.ProductId == productId && !productImages.Select(i => i.FileName).Contains(pi.FileName));
            if(newMainImage != null)
                newMainImage.IsMain = true;

            _context.SaveChanges();
        }

        public Product GetProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                int productImagesCount = product.ProductImages.Count;
                for (int i = 0; i < 8 - productImagesCount; i++)
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
                    dbEntry.NetPrice = model.Product.NetPrice;
                    dbEntry.Category = model.Product.Category;
                }
            }

            SaveProductImages(model);
            _context.SaveChanges();
        }

        private void SaveProductImages(ProductEditViewModel model)
        {
            Dictionary<string, string> savedFileNames = new Dictionary<string, string>();
            if(model.Files != null)
            {
                foreach (var file in model.Files.GroupBy(f => f.FileName).Select(g => g.First()))
                {
                    string fileExtension = file.FileName.Substring(file.FileName.IndexOf("."), (file.FileName.Length - file.FileName.IndexOf(".")));
                    Random r = new Random();
                    string fileName = $"{r.Next(1000000, 9999999)}{fileExtension}";
                    var filePath = _configuration["Directories:imageDirectory"] + $"{fileName}";
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                            file.CopyTo(stream);
                    }
                    savedFileNames.Add(file.FileName, fileName);
                }
            }

            foreach(ProductImage image in model.Product.ProductImages)
            {
                if (image != null)
                {
                    if (image.ProductImageId == 0)
                    {
                        image.FileName = $"{savedFileNames[image.FileName]}";
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
