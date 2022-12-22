using FleetManager.Models;
using k8s;
using k8s.Models;

namespace FleetManager.Services
{
    public class KubernetesService : IKubernetesService
    {
        private readonly ILogger<KubernetesService> _logger;
        private Kubernetes Client { get; set; }

        public KubernetesService(ILogger<KubernetesService> logger)
        {
            _logger = logger;

            // Load from the default kubeconfig on the machine.
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();

            // Load from a specific file:
            //var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(Environment.GetEnvironmentVariable("KUBECONFIG"));

            // Load from in-cluster configuration:
            //var config = KubernetesClientConfiguration.InClusterConfig()

            Client = new Kubernetes(config);
        }

        public async Task<IEnumerable<Fleet>> ListFleets(string @namespace)
        {
            var response = await Client.CustomObjects.ListNamespacedCustomObjectWithHttpMessagesAsync(
                Constants.Fleet.FleetGroup,
                Constants.Fleet.FleetVersion,
                @namespace,
                Constants.Fleet.FleetNamePlural);
            var fleetList = KubernetesJson.Deserialize<CustomResourceList<FleetResponse>>(response.Body.ToString());
            return fleetList.Items.Select(f => new Fleet(
                f.Metadata.CreationTimestamp!.Value,
                f.Metadata.Name,
                f.Metadata.NamespaceProperty,
                f.Status.AllocatedReplicas,
                f.Status.ReadyReplicas,
                f.Status.Replicas,
                f.Status.ReservedReplicas
            ));
        }

        public async Task<Fleet> GetFleet(string @namespace, string name)
        {
            var response = await Client.CustomObjects.GetNamespacedCustomObjectWithHttpMessagesAsync(
                Constants.Fleet.FleetGroup,
                Constants.Fleet.FleetVersion,
                @namespace,
                Constants.Fleet.FleetNamePlural,
                name);
            var fleet = KubernetesJson.Deserialize<FleetResponse>(response.Body.ToString());

            return new Fleet(
                fleet.Metadata.CreationTimestamp!.Value,
                fleet.Metadata.Name,
                fleet.Metadata.NamespaceProperty,
                fleet.Status.AllocatedReplicas,
                fleet.Status.ReadyReplicas,
                fleet.Status.Replicas,
                fleet.Status.ReservedReplicas
            );
        }

        public async Task<FleetCreatedResponse> CreateFleet(CreateFleetRequest request)
        {
            var fleet = new FleetRequest
            {
                ApiVersion = $"{Constants.Fleet.FleetGroup}/{Constants.Fleet.FleetVersion}",
                Kind = Constants.Fleet.FleetKind,
                Metadata = new V1ObjectMeta
                {
                    Name = request.Name,
                    NamespaceProperty = request.Namespace
                },
                Spec = new FleetRequestSpec
                {
                    Replicas = request.Replicas,
                    Template = new FleetRequestTemplate<FleetRequestPortsSpec>
                    {
                        Spec = new FleetRequestPortsSpec
                        {
                            Template = new FleetRequestTemplate<FleetRequestContainersSpec>
                            {
                                Spec = new FleetRequestContainersSpec
                                {
                                    Containers = new List<FleetRequestContainer>
                                    {
                                        new FleetRequestContainer
                                        {
                                            Name = request.Name,
                                            Image = request.Image,
                                            Resources = new FleetRequestContainerResources
                                            {
                                                Requests = new FleetRequestContainerResource
                                                {
                                                    Memory = request.Resources.RequestMemory,
                                                    Cpu = request.Resources.RequestCpu
                                                },
                                                Limits = new FleetRequestContainerResource
                                                {
                                                    Memory = request.Resources.LimitMemory,
                                                    Cpu = request.Resources.LimitCpu
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response = await Client.CustomObjects.CreateNamespacedCustomObjectWithHttpMessagesAsync(
                fleet, Constants.Fleet.FleetGroup, Constants.Fleet.FleetVersion, request.Namespace, Constants.Fleet.FleetNamePlural);

            var createdFleet = KubernetesJson.Deserialize<FleetResponse>(response.Body.ToString());

            return new FleetCreatedResponse(
                createdFleet.Metadata.CreationTimestamp!.Value,
                createdFleet.Metadata.Name,
                createdFleet.Metadata.NamespaceProperty
            );
        }

        public async Task<FleetUpdatedResponse> UpdateFleet(UpdateFleetRequest request)
        {
            return null;
        }

        public async Task<FleetDeletedResponse> DeleteFleet(DeleteFleetRequest request)
        {
            // TODO: Lookup name using ID

            var response = await Client.CustomObjects.DeleteClusterCustomObjectWithHttpMessagesAsync(
                Constants.Fleet.FleetGroup, Constants.Fleet.FleetVersion, Constants.Fleet.FleetNamePlural, "dummyname");

            _logger.LogInformation("Response: {response}", response.Body.ToString());

            return null;
        }
    }
}
