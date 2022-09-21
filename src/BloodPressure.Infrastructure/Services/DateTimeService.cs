using BloodPressure.Application.Common.Interfaces;

namespace BloodPressure.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
