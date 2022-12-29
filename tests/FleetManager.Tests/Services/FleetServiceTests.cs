using FleetManager.Services;
using k8s.Autorest;
using Microsoft.Extensions.Logging;
using Moq;

namespace FleetManager.Tests.Services
{
    public class FleetServiceTests
    {
        [Fact]
        public async Task List_NullNamespace_ThrowsException()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

#pragma warning disable 8625
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await fleetService.List(null));
#pragma warning restore 8625
        }

        // TODO: Needs implemented
        /*[Fact]
        public async Task List_ReturnsFleetList()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            kubernetesClientServiceMock.Setup(k => k.Client.CustomObjects
                .ListNamespacedCustomObjectWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), null, null, null, null, null, null, null, null, null, null, null, default))
                .Returns(Task.FromResult(new HttpOperationResponse<object>()));
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

            var result = await fleetService.List("default");

            Assert.NotNull(result);
        }*/

        [Fact]
        public async Task Get_NullNamespace_ThrowsException()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

#pragma warning disable 8625
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await fleetService.Get(null, "fleet-name"));
#pragma warning restore 8625
        }

        [Fact]
        public async Task Get_NullName_ThrowsException()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

#pragma warning disable 8625
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await fleetService.Get("default", null));
#pragma warning restore 8625
        }

        // TODO: Needs implemented
        /*[Fact]
        public async Task Get_ReturnsFleet()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            kubernetesClientServiceMock.Setup(k => k.Client.CustomObjects
                .GetNamespacedCustomObjectWithHttpMessagesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), null, default))
                .Returns(Task.FromResult(new HttpOperationResponse<object>()));
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

            var result = await fleetService.Get("default", "fleet-name");

            Assert.NotNull(result);
        }*/

        [Fact]
        public async Task Create_NullRequest_ThrowsException()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

#pragma warning disable 8625
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await fleetService.Create(null));
#pragma warning restore 8625
        }

        [Fact]
        public async Task Update_NullRequest_ThrowsException()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

#pragma warning disable 8625
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await fleetService.Update(null));
#pragma warning restore 8625
        }

        [Fact]
        public async Task Delete_NullRequest_ThrowsException()
        {
            var kubernetesClientServiceMock = new Mock<IKubernetesClientService>();
            var loggerMock = new Mock<ILogger<FleetService>>();
            var fleetService = new FleetService(kubernetesClientServiceMock.Object, loggerMock.Object);

#pragma warning disable 8625
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await fleetService.Delete(null));
#pragma warning restore 8625
        }
    }
}
