using FleetManager.Models;

namespace FleetManager.Services
{
    public interface IKubernetesService
    {
        public Task<IEnumerable<Fleet>> ListFleets(string @namespace);
        public Task<FleetCreatedResponse> CreateFleet(CreateFleetRequest request);
        public Task<FleetUpdatedResponse> UpdateFleet(UpdateFleetRequest request);
        public Task<FleetDeletedResponse> DeleteFleet(DeleteFleetRequest fleetId);
        public Task<Fleet> GetFleet(string @namespace, string name);
    }
}
