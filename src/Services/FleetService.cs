using FleetManager.Models.K8sManifests;
using FleetManager.Models.Requests.Fleet;
using FleetManager.Models.Responses.Fleet;
using k8s.Models;
using k8s;

namespace FleetManager.Services
{
    public class FleetService : IFleetService
    {
        private readonly IKubernetesClientService _k8s;
        private readonly ILogger<FleetService> _logger;

        public FleetService(IKubernetesClientService kubernetesClientService, ILogger<FleetService> logger)
        {
            _k8s = kubernetesClientService;
            _logger = logger;
        }

        public async Task<IEnumerable<Fleet>> List(string @namespace)
        {
            ArgumentNullException.ThrowIfNull(@namespace);

            var response = await _k8s.Client.CustomObjects.ListNamespacedCustomObjectWithHttpMessagesAsync(
                Constants.Fleet.Group,
                Constants.Fleet.Version,
                @namespace,
                Constants.Fleet.NamePlural);
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

        public async Task<Fleet> Get(string @namespace, string name)
        {
            ArgumentNullException.ThrowIfNull(@namespace);
            ArgumentNullException.ThrowIfNull(name);

            var response = await _k8s.Client.CustomObjects.GetNamespacedCustomObjectWithHttpMessagesAsync(
                Constants.Fleet.Group,
                Constants.Fleet.Version,
                @namespace,
                Constants.Fleet.NamePlural,
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

        public async Task<FleetCreatedResponse> Create(CreateFleetRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var fleet = new FleetRequest
            {
                ApiVersion = $"{Constants.Fleet.Group}/{Constants.Fleet.Version}",
                Kind = Constants.Fleet.Kind,
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
                                                Requests = new FleetRequestContainerResource(
                                                    request.RequestMemory, request.RequestCpu),
                                                Limits = new FleetRequestContainerResource(
                                                    request.LimitMemory, request.LimitCpu)
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response = await _k8s.Client.CustomObjects.CreateNamespacedCustomObjectWithHttpMessagesAsync(
                fleet, Constants.Fleet.Group, Constants.Fleet.Version, request.Namespace, Constants.Fleet.NamePlural);

            var createdFleet = KubernetesJson.Deserialize<FleetResponse>(response.Body.ToString());

            return new FleetCreatedResponse(
                createdFleet.Metadata.CreationTimestamp!.Value,
                createdFleet.Metadata.Name,
                createdFleet.Metadata.NamespaceProperty
            );
        }

        public async Task<FleetUpdatedResponse> Update(UpdateFleetRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var fleet = new FleetRequest
            {
                ApiVersion = $"{Constants.Fleet.Group}/{Constants.Fleet.Version}",
                Kind = Constants.Fleet.Kind,
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
                                                Requests = new FleetRequestContainerResource(
                                                    request.RequestMemory, request.RequestCpu),
                                                Limits = new FleetRequestContainerResource(
                                                    request.LimitMemory, request.LimitCpu)
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response = await _k8s.Client.CustomObjects.PatchNamespacedCustomObjectWithHttpMessagesAsync(
                fleet, Constants.Fleet.Group, Constants.Fleet.Version, request.Namespace, Constants.Fleet.NamePlural, request.Name);

            _logger.LogInformation("Response: {response}", response.Body.ToString());

            return null;
        }

        public async Task<FleetDeletedResponse> Delete(DeleteFleetRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var response = await _k8s.Client.CustomObjects.DeleteNamespacedCustomObjectWithHttpMessagesAsync(
                Constants.Fleet.Group, Constants.Fleet.Version, request.Namespace, Constants.Fleet.NamePlural, request.Name);

            _logger.LogInformation("Response: {response}", response.Body.ToString());

            return null;
        }
    }
}
