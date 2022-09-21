using BloodPressure.Application.Common;

namespace BloodPressure.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
