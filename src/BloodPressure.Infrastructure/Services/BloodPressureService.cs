using BloodPressure.Application.Common.Dtos;
using BloodPressure.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodPressure.Infrastructure.Services
{
    public class BloodPressureService : IBloodPressureService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBloodPressureDbContext _dbContext;

        public BloodPressureService(IBloodPressureDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<MeasurementDto> CreateMeasurement(CreateMeasurementDto addMeasurement,
            CancellationToken cancellationToken)
        {
            var measurement = addMeasurement.ToEntity(_currentUserService.UserId);
            await _dbContext.Measurement.AddAsync(measurement, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new MeasurementDto(measurement);
        }

        public async Task<List<MeasurementDto>> GetMeasurements(GetMeasurementsDto getMeasurements,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Measurement.AsQueryable();

            if (getMeasurements.From.HasValue)
            {
                query = query.Where(x => x.MeasuringDate >= getMeasurements.From);
            }

            if (getMeasurements.To.HasValue)
            {
                query = query.Where(x => x.MeasuringDate <= getMeasurements.To);
            }

            return await query.Where(x => x.UserId == getMeasurements.UserId).Select(x => new MeasurementDto(x))
                .ToListAsync(cancellationToken);
        }
    }
}
