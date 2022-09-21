using System.Reflection;
using BloodPressure.Application.Common;
using BloodPressure.Domain.Entities;
using BloodPressure.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodPressure.Infrastructure
{
    internal class BloodPressureDbContext : DbContext, IBloodPressureDbContext
    {
        private readonly IDateTime _dateTime;

        public BloodPressureDbContext(DbContextOptions<BloodPressureDbContext> options, IDateTime dateTime) :
            base(options)
        {
            _dateTime = dateTime ?? throw new NullReferenceException(nameof(dateTime));
        }

        public DbSet<Measurement> Measurement { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public void Migrate()
        {
            base.Database.Migrate();
        }

        public void EnsureDeleted()
        {
            base.Database.EnsureDeleted();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Entity specific configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
