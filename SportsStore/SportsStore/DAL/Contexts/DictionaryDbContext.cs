using Microsoft.EntityFrameworkCore;
using SportsStore.Models.DictionaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Contexts
{
    public class DictionaryDbContext : DbContext
    {
        public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options)
        { }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<DocumentType>()
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (Domain.DocumentTypes)Enum.Parse(typeof(Domain.DocumentTypes), v));
        }
    }
}
