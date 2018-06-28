using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product {Name = "Produkt 1", Price = 10M},
            new Product {Name = "Produkt 2", Price = 20},
            new Product {Name = "Produkt 3", Price = 30M}
        }.AsQueryable();
    }
}
