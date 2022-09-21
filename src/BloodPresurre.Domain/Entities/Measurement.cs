using BloodPressure.Domain.Models;

namespace BloodPressure.Domain.Entities;

public class Measurement : BaseEntity
{
    public Measurement(int id, int? pulse, int systolic, int diastolic, string userId, DateTime measuringDate,
        DateTime created, DateTime modified)
    {
        Id = id;
        Pulse = pulse;
        Systolic = systolic;
        Diastolic = diastolic;
        UserId = userId;
        MeasuringDate = measuringDate;
        Created = created;
        Modified = modified;
    }

    public Measurement(int? pulse, int systolic, int diastolic, DateTime measuringDate, string userId)
    {
        Pulse = pulse;
        Systolic = systolic;
        Diastolic = diastolic;
        MeasuringDate = measuringDate;
        UserId = userId;
    }

    public int Id { get; init; }
    public int? Pulse { get; init; }
    public int Systolic { get; init; }
    public int Diastolic { get; init; }
    public string UserId { get; init; }
    public DateTime MeasuringDate { get; init; }
}