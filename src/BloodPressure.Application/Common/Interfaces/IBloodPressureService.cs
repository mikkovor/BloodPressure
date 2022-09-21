using BloodPressure.Application.Common.Dtos;

namespace BloodPressure.Application.Common.Interfaces;

public interface IBloodPressureService
{
    Task<MeasurementDto> CreateMeasurement(CreateMeasurementDto addMeasurement, CancellationToken cancellationToken);
}
