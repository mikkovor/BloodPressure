using BloodPressure.Application.Common;

namespace BloodPressure.Infrastructure.Services
{
    internal class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
