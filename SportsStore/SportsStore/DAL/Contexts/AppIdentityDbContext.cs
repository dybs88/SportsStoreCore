using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsStore.Domain;
using SportsStore.Migrations.AppIdentityDb;
using SportsStore.Models.Identity;
using SportsStore.Models.Parameters;

namespace SportsStore.DAL.Contexts
{
    public class AppIdentityDbContext : IdentityDbContext<SportUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<SystemParameter> Parameters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("Users", "Security");
            builder.Entity<SportUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");

            builder.Entity<SystemParameter>()
                .Property(sp => sp.ParameterType)
                .HasConversion
                (
                    x => x.ToString(),
                    x => (ParameterType)Enum.Parse(typeof(ParameterType), x)
                );

            builder.Entity<SystemParameter>()
                .HasIndex(sp => sp.Key)
                .IsUnique();
        }
    }
}
