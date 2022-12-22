namespace FleetManager.Models
{
    public record CreateFleetRequest(string Name, string Namespace, string Image, int Replicas, FleetResources Resources, int? Port);
    public record FleetResources(string RequestMemory, string RequestCpu, string LimitMemory, string LimitCpu);
}
