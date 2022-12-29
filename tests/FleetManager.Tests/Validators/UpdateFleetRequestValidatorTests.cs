using FleetManager.Models.Requests.Fleet;
using FleetManager.Validators;

namespace FleetManager.Tests.Validators
{
    public class UpdateFleetRequestValidatorTests
    {
        [Theory]
        [InlineData(null, null, null, null, null, null, null, null)]
        [InlineData("", "", "", -1, "", "", "", "")]
        [InlineData(" ", " ", " ", 0, " ", " ", " ", " ")]
        [InlineData("NAME", "NS", "IMG", 1000, "1", "2", "3", "4")]
        public async Task Validate_InvalidData_ReturnsFalse(
            string? name,
            string? @namespace,
            string? image,
            int? replicas,
            string? requestMemory,
            string? requestCpu,
            string? limitMemory,
            string? limitCpu)
        {
#pragma warning disable 8604
            var request = new UpdateFleetRequest(name, @namespace, image, replicas,
                requestMemory, requestCpu, limitMemory, limitCpu);
#pragma warning restore 8604

            var sut = new UpdateFleetRequestValidator();
            var result = await sut.ValidateAsync(request);

            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("fleet-name", "default", null, null, null, null, null, null)]
        [InlineData("fleet-name", "default", "img.dev/somewhere:latest", null, null, null, null, null)]
        [InlineData("fleet-name", "default", "img.dev/somewhere:latest", 3, null, null, null, null)]
        [InlineData("fleet-name", "default", "img.dev/somewhere:latest", 3, "24Mi", null, null, null)]
        [InlineData("fleet-name", "default", "img.dev/somewhere:latest", 3, "24Mi", "10m", null, null)]
        [InlineData("fleet-name", "default", "img.dev/somewhere:latest", 3, "24Mi", "10m", "64Mi", null)]
        [InlineData("fleet-name", "default", "img.dev/somewhere:latest", 3, "24Mi", "10m", "64Mi", "20m")]
        public async Task Validate_ValidData_ReturnsTrue(
            string? name,
            string? @namespace,
            string? image,
            int? replicas,
            string? requestMemory,
            string? requestCpu,
            string? limitMemory,
            string? limitCpu)
        {
#pragma warning disable 8604
            var request = new UpdateFleetRequest(name, @namespace, image, replicas,
                requestMemory, requestCpu, limitMemory, limitCpu);
#pragma warning restore 8604

            var sut = new UpdateFleetRequestValidator();
            var result = await sut.ValidateAsync(request);

            Assert.True(result.IsValid);
        }
    }
}
