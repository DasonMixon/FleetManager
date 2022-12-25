using FleetManager.Models.Requests.GameServer;
using FleetManager.Models.Responses.GameServer;
using FleetManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameServersController : ControllerBase
    {
        private readonly IGameServerService _gameServerService;

        public GameServersController(IGameServerService gameServerService)
        {
            _gameServerService = gameServerService;
        }

        [HttpPost("allocate")]
        public async Task<ActionResult<GameServerAllocatedResponse>> Allocate(AllocateGameServerRequest request)
        {
            return Ok(await _gameServerService.Allocate(request));
        }
    }
}
