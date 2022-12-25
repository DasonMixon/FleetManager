using k8s;

namespace FleetManager.Services
{
    public class KubernetesClientService : IKubernetesClientService
    {
        public Kubernetes Client { get; init; }

        public KubernetesClientService()
        {
            // Load from the default kubeconfig on the machine.
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();

            // Load from a specific file:
            //var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(Environment.GetEnvironmentVariable("KUBECONFIG"));

            // Load from in-cluster configuration:
            //var config = KubernetesClientConfiguration.InClusterConfig()

            Client = new Kubernetes(config);
        }
    }
}
