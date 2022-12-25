using k8s;

namespace FleetManager.Services
{
    public interface IKubernetesClientService
    {
        public Kubernetes Client { get; init; }
    }
}
