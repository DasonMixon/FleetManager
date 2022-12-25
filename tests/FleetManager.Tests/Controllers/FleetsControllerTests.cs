﻿using FleetManager.Controllers;
using FleetManager.Exceptions;
using FleetManager.Models.K8sManifests;
using FleetManager.Models.Requests.Fleet;
using FleetManager.Models.Responses.Fleet;
using FleetManager.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FleetManager.Tests.Controllers
{
    public class FleetsControllerTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("default", null)]
        [InlineData(null, "fleet-name")]
        public async Task Get_InvalidParameters_ThrowsException(string @namespace, string name)
        {
            var fleetServiceMock = new Mock<IFleetService>();
            var sut = new FleetsController(fleetServiceMock.Object);

            await Assert.ThrowsAsync<ControllerParameterValidationException>(
                async () => await sut.Get(@namespace, name));
        }

        [Fact]
        public async Task Get_ReturnsFleet()
        {
            var expectedFleet = new Fleet(DateTime.UtcNow, "fleet-name", "default", 0, 3, 3, 0);

            var fleetServiceMock = new Mock<IFleetService>();
            fleetServiceMock.Setup(f => f.Get(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(expectedFleet));
            var sut = new FleetsController(fleetServiceMock.Object);

            var result = await sut.Get("default", "fleet-name");

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedFleet, ((OkObjectResult)result.Result).Value);

            fleetServiceMock.Verify(f => f.Get("default", "fleet-name"), Times.Once());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task List_InvalidParameters_ThrowsException(string @namespace)
        {
            var fleetServiceMock = new Mock<IFleetService>();
            var sut = new FleetsController(fleetServiceMock.Object);

            await Assert.ThrowsAsync<ControllerParameterValidationException>(
                async () => await sut.List(@namespace));
        }

        [Fact]
        public async Task List_ReturnsFleetList()
        {
            var expectedFleets = new List<Fleet>
            {
                new Fleet(DateTime.UtcNow, "fleet-name-1", "default", 0, 3, 3, 0),
                new Fleet(DateTime.UtcNow, "fleet-name-2", "default", 1, 1, 3, 1),
            };

            var fleetServiceMock = new Mock<IFleetService>();
            fleetServiceMock.Setup(f => f.List(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedFleets.AsEnumerable()));
            var sut = new FleetsController(fleetServiceMock.Object);

            var result = await sut.List("default");

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedFleets, ((OkObjectResult)result.Result).Value);

            fleetServiceMock.Verify(f => f.List("default"), Times.Once());
        }

        [Fact]
        public async Task Create_ReturnsFleetCreatedResponse()
        {
            var expectedFleetCreatedResponse = new FleetCreatedResponse(
                DateTime.UtcNow, "fleet-name", "default");

            var fleetServiceMock = new Mock<IFleetService>();
            fleetServiceMock.Setup(f => f.Create(It.IsAny<CreateFleetRequest>()))
                .Returns(Task.FromResult(expectedFleetCreatedResponse));
            var sut = new FleetsController(fleetServiceMock.Object);

            var request = new CreateFleetRequest(
                "fleet-name", "default", "dockerhub.com/someimage:latest", 3, "64Mi", "30m", "128Mi", "60m");
            var result = await sut.Create(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedFleetCreatedResponse, ((OkObjectResult)result.Result).Value);

            fleetServiceMock.Verify(f => f.Create(request), Times.Once());
        }

        [Fact]
        public async Task Update_ReturnsFleetUpdatedResponse()
        {
            var expectedFleetUpdatedResponse = new FleetUpdatedResponse(
                DateTime.UtcNow, "fleet-name", "default");

            var fleetServiceMock = new Mock<IFleetService>();
            fleetServiceMock.Setup(f => f.Update(It.IsAny<UpdateFleetRequest>()))
                .Returns(Task.FromResult(expectedFleetUpdatedResponse));
            var sut = new FleetsController(fleetServiceMock.Object);

            var request = new UpdateFleetRequest("fleet-name", "default", Replicas: 6);
            var result = await sut.Update(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedFleetUpdatedResponse, ((OkObjectResult)result.Result).Value);

            fleetServiceMock.Verify(f => f.Update(request), Times.Once());
        }

        [Fact]
        public async Task Delete_ReturnsFleetDeletedResponse()
        {
            var expectedFleetDeletedResponse = new FleetDeletedResponse(
                DateTime.UtcNow, "fleet-name", "default");

            var fleetServiceMock = new Mock<IFleetService>();
            fleetServiceMock.Setup(f => f.Delete(It.IsAny<DeleteFleetRequest>()))
                .Returns(Task.FromResult(expectedFleetDeletedResponse));
            var sut = new FleetsController(fleetServiceMock.Object);

            var request = new DeleteFleetRequest("fleet-name", "default");
            var result = await sut.Delete(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedFleetDeletedResponse, ((OkObjectResult)result.Result).Value);

            fleetServiceMock.Verify(f => f.Delete(request), Times.Once());
        }
    }
}
