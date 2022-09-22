using System.Threading;
using System.Threading.Tasks;
using BloodPressure.Application.Common.Dtos;
using BloodPressure.Application.Common.Extensions;
using BloodPressure.Application.Common.Interfaces;
using BloodPressure.Functions;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BloodPressure.Functions.Functions
{
    public class GetMeasurementsFunction
    {
        private readonly IBloodPressureService _bloodPressureService;
        private readonly ILogger _logger;

        public GetMeasurementsFunction(IBloodPressureService bloodPressureService,
            ILogger<GetMeasurementsFunction> logger)
        {
            _bloodPressureService = bloodPressureService;
            _logger = logger;
        }

        [FunctionName("GetMeasurementsFunction")]
        [return: ServiceBus("measurements-email", Connection = "BloodPressureConnection")]
        public async Task<MeasurementsEmailDto> Run(
            [ServiceBusTrigger("get-measurements", Connection = "BloodPressureConnection")]
            GetMeasurementsDto getMeasurements, CancellationToken cancellationToken)
        {
            if (!getMeasurements.IsValid<GetMeasurementsDto, GetMeasurementsDtoValidator>(out var validationResult))
            {
                _logger.LogError("Request {0} was not valid. Found errors: {1}", getMeasurements, validationResult);
                throw new ValidationException(validationResult.Errors);
            }

            var measurements = await _bloodPressureService.GetMeasurements(getMeasurements, cancellationToken);

            return new MeasurementsEmailDto(getMeasurements.UserId, getMeasurements.From,
                getMeasurements.To,
                measurements);
        }
    }
}
