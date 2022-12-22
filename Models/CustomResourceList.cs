using k8s.Models;
using k8s;

namespace FleetManager.Models
{
    public class CustomResourceList<T> : KubernetesObject
    {
        public V1ListMeta Metadata { get; set; }
        public List<T> Items { get; set; }
    }
}
