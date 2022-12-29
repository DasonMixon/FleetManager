using FleetManager.Models.Requests.Fleet;
using FleetManager.Validators;

namespace FleetManager.Tests.Validators
{
    public class DeleteFleetRequestValidatorTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("NAME", "NS")]
        public async Task Validate_InvalidData_ReturnsFalse(
            string? name,
            string? @namespace)
        {
#pragma warning disable 8604
            var request = new DeleteFleetRequest(name, @namespace);
#pragma warning restore 8604

            var sut = new DeleteFleetRequestValidator();
            var result = await sut.ValidateAsync(request);

            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("fleet-name", "default")]
        [InlineData("fleet", "default-nd")]
        public async Task Validate_ValidData_ReturnsTrue(
            string? name,
            string? @namespace)
        {
#pragma warning disable 8604
            var request = new DeleteFleetRequest(name, @namespace);
#pragma warning restore 8604

            var sut = new DeleteFleetRequestValidator();
            var result = await sut.ValidateAsync(request);

            Assert.True(result.IsValid);
        }
    }
}
