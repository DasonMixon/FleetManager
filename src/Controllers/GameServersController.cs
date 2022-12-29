using FleetManager.Models.Requests.GameServer;
using FleetManager.Models.Responses.GameServer;
using FleetManager.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameServersController : ControllerBase
    {
        private readonly IGameServerService _gameServerService;
        private readonly IValidator<AllocateGameServerRequest> _allocateGameServerRequestValidator;

        public GameServersController(IGameServerService gameServerService,
            IValidator<AllocateGameServerRequest> allocateGameServerRequestValidator)
        {
            _gameServerService = gameServerService;
            _allocateGameServerRequestValidator = allocateGameServerRequestValidator;
        }

        [HttpPost("allocate")]
        public async Task<ActionResult<GameServerAllocatedResponse>> Allocate(AllocateGameServerRequest request)
        {
            var validationResult = _allocateGameServerRequestValidator.Validate(request);

            return validationResult.IsValid
                ? Ok(await _gameServerService.Allocate(request))
                : validationResult.BuildResult();
        }
    }
}
