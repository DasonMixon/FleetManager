using FleetManager.Exceptions;
using FleetManager.Models.K8sManifests;
using FleetManager.Models.Requests.GameServer;
using FleetManager.Models.Responses.GameServer;
using k8s.Models;
using k8s;

namespace FleetManager.Services
{
    public class GameServerService : IGameServerService
    {
        private readonly IKubernetesClientService _k8s;

        public GameServerService(IKubernetesClientService kubernetesClientService)
        {
            _k8s = kubernetesClientService;
        }

        public async Task<GameServerAllocatedResponse> Allocate(AllocateGameServerRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var gameServerAllocation = new GameServerAllocation
            {
                ApiVersion = $"{Constants.GameServerAllocation.Group}/{Constants.Fleet.Version}",
                Kind = Constants.GameServerAllocation.Kind,
                Spec = new GameServerAllocationSpec
                {
                    Selectors = new List<GameServerSelector>
                    {
                        new GameServerSelector
                        {
                            LabelSelector = new V1LabelSelector
                            {
                                MatchLabels = new Dictionary<string, string>
                                {
                                    { "agones.dev/fleet", request.FleetName }
                                }
                            }
                        }
                    }
                }
            };

            var response = await _k8s.Client.CustomObjects.CreateNamespacedCustomObjectWithHttpMessagesAsync(
                gameServerAllocation, Constants.GameServerAllocation.Group, Constants.GameServerAllocation.Version,
                request.Namespace, Constants.GameServerAllocation.NamePlural);

            var gameServer = KubernetesJson.Deserialize<GameServerAllocation>(response.Body.ToString());

            if (gameServer.Status.State == "UnAllocated")
            {
                throw new GameServerAllocationException(
                    $"Unable to allocate game server for fleet {request.Namespace}/{request.FleetName}");
            }

            return new GameServerAllocatedResponse
            {
                State = gameServer.Status.State,
                Address = gameServer.Status.Address!,
                GameServerName = gameServer.Status.GameServerName!,
                Port = gameServer.Status.Ports![0].Port
            };
        }
    }
}
