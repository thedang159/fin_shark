using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Migrations;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Database
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserID, p.StockId }));

            modelBuilder
                .Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserID);

            modelBuilder
                .Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            var addedEntities = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();

            foreach (var entry in addedEntities)
            {
                if (entry.Entity is Stock stock)
                {
                    stock.BeforeInsert();
                }

                if (entry.Entity is Comment comment)
                {
                    comment.BeforeInsert();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
