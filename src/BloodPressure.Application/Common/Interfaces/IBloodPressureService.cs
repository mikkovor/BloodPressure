using BloodPressure.Application.Common.Dtos;

namespace BloodPressure.Application.Common.Interfaces;

public interface IBloodPressureService
{
    Task<MeasurementDto> CreateMeasurement(CreateMeasurementDto addMeasurement, CancellationToken cancellationToken);

    Task<List<MeasurementDto>> GetMeasurements(GetMeasurementsDto getMeasurements, CancellationToken cancellationToken);
}
