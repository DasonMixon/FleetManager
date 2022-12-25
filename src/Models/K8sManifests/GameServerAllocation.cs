using k8s;
using k8s.Models;

namespace FleetManager.Models.K8sManifests
{
    public class GameServerAllocation : KubernetesObject
    {
        public V1ObjectMeta Metadata { get; set; } = default!;
        public GameServerAllocationSpec Spec { get; set; } = default!;
        public GameServerStatus Status { get; set; } = default!;
    }

    public class GameServerAllocationSpec
    {
        public IList<GameServerSelector> Selectors { get; set; } = default!;
    }

    public class GameServerSelector
    {
        public V1LabelSelector LabelSelector { get; set; } = default!;
    }

    public class GameServerStatus
    {
        public string State { get; set; } = default!;
        public string? GameServerName { get; set; }
        public IList<GameServerPort>? Ports { get; set; }
        public string? Address { get; set; }
        public string? NodeName { get; set; }
    }

    public class GameServerPort
    {
        public string Name { get; set; } = default!;
        public int Port { get; set; } = default!;
    }
}
