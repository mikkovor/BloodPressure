using BloodPressure.Functions.Functions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace BloodPressure.IntegrationTests
{
    [Collection(IntegrationTestsCollection.Name)]
    public class CreateMeasurementTests
    {
        private readonly CreateMeasurementFunction _function;

        public CreateMeasurementTests(TestHost testHost)
        {
            _function = new CreateMeasurementFunction();
        }

        [Fact]
        public async Task Test1()
        {
            var dict = new Dictionary<string, StringValues> {{"name", "mikko"}};
            var query = new QueryCollection(dict);

            HttpRequest httpRequest = new DefaultHttpRequest(new DefaultHttpContext());
            httpRequest.Query = query;
            var res = await _function.Run(httpRequest);

            var test = res as OkObjectResult;
            test.Should().NotBeNull();
            test?.StatusCode?.Should().Be(200);
        }
    }
}