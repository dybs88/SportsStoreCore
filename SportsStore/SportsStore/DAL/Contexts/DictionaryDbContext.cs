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

        public DbSet<VatRate> VatRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<DocumentType>()
                .Property(e => e.DocumentKind)
                .HasConversion(
                    v => v.ToString(),
                    v => (Domain.DocumentKind)Enum.Parse(typeof(Domain.DocumentKind), v));

            builder
                .Entity<VatRate>()
                .HasIndex(vr => vr.Symbol)
                .IsUnique();
        }
    }
}
