using BloodPressure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodPressure.Application.Common.Interfaces;

public interface IBloodPressureDbContext
{
    public DbSet<Measurement> Measurement { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    void Migrate();

    void EnsureDeleted();
}
