using BloodPressure.Application.Common.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BloodPressure.Functions.Functions
{
    public class GetMeasurementsFunction
    {
        private readonly IBloodPressureService _bloodPressureService;

        public GetMeasurementsFunction(IBloodPressureService bloodPressureService)
        {
            _bloodPressureService = bloodPressureService;
        }

        [FunctionName("GetMeasurementsFunction")]
        [return: Queue("measurements-email")]
        public void Run(
            [ServiceBusTrigger("get-measurements", Connection = "BloodPressureConnection")]
            string myQueueItem,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
