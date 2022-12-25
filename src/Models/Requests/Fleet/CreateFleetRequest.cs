namespace FleetManager.Models.Requests.Fleet
{
    public record CreateFleetRequest(
        string Name,
        string Namespace,
        string Image,
        int Replicas,
        string RequestMemory,
        string RequestCpu,
        string LimitMemory,
        string LimitCpu);
}
