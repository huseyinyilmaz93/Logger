using System;
using System.Linq;
using Logger.Data.EF.Common;
using Microsoft.EntityFrameworkCore;

namespace Logger.Data.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Log> Logs { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            var dateTimeNow = DateTime.Now;
            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedDate = dateTimeNow;

                if (entityEntry.State != EntityState.Added)
                    continue;

                ((BaseEntity)entityEntry.Entity).Id = Guid.NewGuid();
                ((BaseEntity)entityEntry.Entity).CreatedDate = dateTimeNow;
            }

            return base.SaveChanges();

        }
    }
}
