using FleetManager.Controllers;
using FleetManager.Models.Requests.GameServer;
using FleetManager.Models.Responses.GameServer;
using FleetManager.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FleetManager.Tests.Controllers
{
    public class GameServersControllerTests
    {
        [Fact]
        public async Task Allocate_ReturnsGameServerAllocatedResponse()
        {
            var expectedGameServerAllocatedResponse = new GameServerAllocatedResponse
            {
                State = "Allocated",
                GameServerName = "game-server-1",
                Address = "1.1.1.1",
                Port = 1234
            };

            var gameServerServiceMock = new Mock<IGameServerService>();
            gameServerServiceMock.Setup(f => f.Allocate(It.IsAny<AllocateGameServerRequest>()))
                .Returns(Task.FromResult(expectedGameServerAllocatedResponse));
            var sut = new GameServersController(gameServerServiceMock.Object);

            var request = new AllocateGameServerRequest("fleet-name", "default");
            var result = await sut.Allocate(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedGameServerAllocatedResponse, ((OkObjectResult)result.Result).Value);

            gameServerServiceMock.Verify(f => f.Allocate(request), Times.Once());
        }
    }
}
