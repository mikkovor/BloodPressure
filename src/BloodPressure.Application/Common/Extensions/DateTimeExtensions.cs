namespace BloodPressure.Application.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(1)
                .Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }
    }
}
