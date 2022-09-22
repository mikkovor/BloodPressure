namespace BloodPressure.Application.Common.Dtos;

public record MeasurementsEmailDto(string UserId, DateTime? From, DateTime? To, List<MeasurementDto> Measurements);