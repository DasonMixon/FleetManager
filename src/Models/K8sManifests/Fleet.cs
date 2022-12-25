using k8s;
using k8s.Models;

namespace FleetManager.Models.K8sManifests
{
    // TODO: Split these up into separate files based on groupings
    public record Fleet(DateTime CreatedDateTime, string Name, string @Namespace,
        int AllocatedReplicas, int ReadyReplicas, int Replicas, int ReservedReplicas);

    public class FleetRequest : KubernetesObject
    {
        public V1ObjectMeta Metadata { get; init; } = default!;
        public FleetRequestSpec Spec { get; init; } = default!;
    }

    public record FleetRequestSpec
    {
        public int? Replicas { get; set; }
        public FleetRequestTemplate<FleetRequestPortsSpec> Template { get; init; } = default!;
    }

    public record FleetRequestTemplate<TSpec>
    {
        public TSpec Spec { get; init; } = default!;
    };

    public record FleetRequestPortsSpec
    {
        public IList<FleetRequestPort>? Ports { get; init; }
        public FleetRequestTemplate<FleetRequestContainersSpec> Template { get; init; } = default!;
    };

    public record FleetRequestPort
    {
        public string? Name { get; init; }
        public int? ContainerPort { get; init; }
    };

    public record FleetRequestContainersSpec
    {
        public IList<FleetRequestContainer> Containers { get; init; } = default!;
    }

    public record FleetRequestContainer
    {
        public string Name { get; init; } = default!;
        public string? Image { get; set; }
        public FleetRequestContainerResources? Resources { get; set; }
    }

    public record FleetRequestContainerResources
    {
        public FleetRequestContainerResource Requests { get; init; } = default!;
        public FleetRequestContainerResource Limits { get; init; } = default!;
    }

    public record FleetRequestContainerResource(string? Memory, string? Cpu);

    public record FleetResponse(V1ObjectMeta Metadata, FleetStatus Status);

    public record FleetStatus(int AllocatedReplicas, int ReadyReplicas, int Replicas, int ReservedReplicas);
}
