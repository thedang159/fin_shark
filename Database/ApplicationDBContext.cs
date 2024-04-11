using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }

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
