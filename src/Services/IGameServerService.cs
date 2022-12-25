using FleetManager.Models.Requests.GameServer;
using FleetManager.Models.Responses.GameServer;

namespace FleetManager.Services
{
    public interface IGameServerService
    {
        public Task<GameServerAllocatedResponse> Allocate(AllocateGameServerRequest request);
    }
}
