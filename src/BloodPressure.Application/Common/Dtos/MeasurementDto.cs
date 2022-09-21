using BloodPressure.Domain.Entities;

namespace BloodPressure.Application.Common.Dtos
{
    public record MeasurementDto(int Id, int? Pulse, int Systolic, int Diastolic, DateTime MeasuringDate)
    {
        public MeasurementDto(Measurement m) : this(m.Id, m.Pulse, m.Systolic, m.Diastolic, m.MeasuringDate)
        {
        }
    }
}
