using k8s;
using k8s.Models;

namespace FleetManager.Models
{
    public record Fleet (DateTime CreatedDateTime, string Name, string @Namespace,
        int AllocatedReplicas, int ReadyReplicas, int Replicas, int ReservedReplicas);

    //public record FleetRequest(V1ObjectMeta Metadata, FleetStatus Status);

    public class FleetRequest : KubernetesObject
    {
        public V1ObjectMeta Metadata { get; set; }
        public FleetRequestSpec Spec { get; set; }
    }

    public class FleetRequestSpec
    {
        public int Replicas { get; set; }
        public FleetRequestTemplate<FleetRequestPortsSpec> Template { get; set; }
    }

    public class FleetRequestTemplate<TSpec>
    {
        public TSpec Spec { get; set; }
    }

    public class FleetRequestPortsSpec
    {
        public IList<FleetRequestPort> Ports { get; set; }
        public FleetRequestTemplate<FleetRequestContainersSpec> Template { get;set; }
    }

    public class FleetRequestPort
    {
        public string Name { get; set; }
        public int ContainerPort { get; set; }
    }

    public class FleetRequestContainersSpec
    {
        public IList<FleetRequestContainer> Containers { get; set; }
    }

    public class FleetRequestContainer
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public FleetRequestContainerResources Resources { get; set; }
    }

    public class FleetRequestContainerResources
    {
        public FleetRequestContainerResource Requests { get; set; }
        public FleetRequestContainerResource Limits { get; set; }
    }

    public class FleetRequestContainerResource
    {
        public string Memory { get; set; }
        public string Cpu { get; set; }
    }

    public record FleetResponse(V1ObjectMeta Metadata, FleetStatus Status);
    
    public record FleetStatus(int AllocatedReplicas, int ReadyReplicas, int Replicas, int ReservedReplicas);
}
