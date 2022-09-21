using System.Threading;
using System.Threading.Tasks;
using BloodPressure.Application.Common.Dtos;
using BloodPressure.Application.Common.Extensions;
using BloodPressure.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace BloodPressure.Functions.Functions;

public class CreateMeasurementFunction
{
    private readonly IBloodPressureService _bloodPressureService;

    public CreateMeasurementFunction(IBloodPressureService bloodPressureService)
    {
        _bloodPressureService = bloodPressureService;
    }

    [FunctionName("CreateMeasurement")]
    public async Task<ActionResult<MeasurementDto>> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
        CreateMeasurementDto createMeasurement, CancellationToken cancellationToken)
    {
        // TODO MOVE THIS TO SERVICE LEVEL
        if (!createMeasurement.IsValid<CreateMeasurementDto, CreateMeasurementDtoValidator>(out var validationResults))
        {
            return new BadRequestObjectResult(validationResults);
        }

        // TODO is there some exception filters available? try catch?
        var result = await _bloodPressureService.CreateMeasurement(createMeasurement, cancellationToken);
        return new OkObjectResult(result);
    }
}