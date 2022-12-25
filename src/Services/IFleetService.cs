using FleetManager.Models.K8sManifests;
using FleetManager.Models.Requests.Fleet;
using FleetManager.Models.Responses.Fleet;

namespace FleetManager.Services
{
    public interface IFleetService
    {
        public Task<Fleet> Get(string @namespace, string name);
        public Task<IEnumerable<Fleet>> List(string @namespace);
        public Task<FleetCreatedResponse> Create(CreateFleetRequest request);
        public Task<FleetUpdatedResponse> Update(UpdateFleetRequest request);
        public Task<FleetDeletedResponse> Delete(DeleteFleetRequest fleetId);
    }
}
