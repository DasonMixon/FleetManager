using FleetManager.Models.Requests.GameServer;
using FleetManager.Validators;

namespace FleetManager.Tests.Validators
{
    public class AllocateGameServerRequestValidatorTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("FLEET:NAME", "DEFAULT")]
        public async Task Validate_InvalidData_ReturnsFalse(string? fleetName, string? @namespace)
        {
#pragma warning disable 8604
            var request = new AllocateGameServerRequest(fleetName, @namespace);
#pragma warning restore 8604

            var sut = new AllocateGameServerRequestValidator();
            var result = await sut.ValidateAsync(request);

            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("fleet-name", "default")]
        [InlineData("fleet", "default-ns")]
        public async Task Validate_ValidData_ReturnsTrue(string fleetName, string @namespace)
        {
            var request = new AllocateGameServerRequest(fleetName, @namespace);

            var sut = new AllocateGameServerRequestValidator();
            var result = await sut.ValidateAsync(request);

            Assert.True(result.IsValid);
        }
    }
}
