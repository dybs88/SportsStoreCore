using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.OrderModels;
using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.AbstractContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Customer> Customers { get; set; }

        int SaveChanges();

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    }
}
