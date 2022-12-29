using k8s;

namespace FleetManager.Services
{
    public class KubernetesClientService : IKubernetesClientService
    {
        private KubernetesClientConfiguration Config { get; set; }
        private Kubernetes? _client;
        public Kubernetes Client {
            get {
                _client ??= new Kubernetes(Config);
                return _client;
            }
            private set { _client = value; }
        }

        public KubernetesClientService()
        {
            // Load from the default kubeconfig on the machine.
            Config = KubernetesClientConfiguration.BuildConfigFromConfigFile();

            // Load from a specific file:
            //var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(Environment.GetEnvironmentVariable("KUBECONFIG"));

            // Load from in-cluster configuration:
            //var config = KubernetesClientConfiguration.InClusterConfig()
        }
    }
}
