using k8s.Models;
using k8s;

namespace FleetManager.Models.K8sManifests
{
    public class CustomResourceList<T> : KubernetesObject
    {
        public V1ListMeta Metadata { get; set; } = default!;
        public List<T> Items { get; set; } = default!;
    }
}
