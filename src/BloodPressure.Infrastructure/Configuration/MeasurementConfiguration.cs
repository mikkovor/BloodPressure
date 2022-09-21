using BloodPressure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodPressure.Infrastructure.Configuration
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Systolic).IsRequired();
            builder.Property(x => x.Diastolic).IsRequired();
        }
    }
}
