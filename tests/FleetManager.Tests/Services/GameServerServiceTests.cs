using System;
using FleetManager.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FleetManager.Tests.Services
{
	public class GameServerServiceTests
	{
        [Fact]
        public async Task Allocate_NullRequest_ThrowsException()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            var fleetService = new GameServerService(kubernetesClientServiceMock.Object);

#pragma warning disable 8625
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await fleetService.Allocate(null));
#pragma warning restore 8625
        }
    }
}
