namespace FleetManager.Models.Requests.Fleet
{
    public record UpdateFleetRequest(
        string Name,
        string Namespace,
        string? Image = null,
        int? Replicas = null,
        string? RequestMemory = null,
        string? RequestCpu = null,
        string? LimitMemory = null,
        string? LimitCpu = null);
}
