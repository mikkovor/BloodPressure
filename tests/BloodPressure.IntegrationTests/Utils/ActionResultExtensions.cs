using Microsoft.AspNetCore.Mvc;

namespace BloodPressure.IntegrationTests.Utils
{
    public static class ActionResultExtensions
    {
        public static T? GetObjectResultContent<T>(this ActionResult<T> result)
        {
            return (T?) (result.Result as ObjectResult)?.Value;
        }
    }
}
