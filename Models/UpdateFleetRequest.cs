namespace FleetManager.Models
{
    public record UpdateFleetRequest(string Name, string Namespace, string? Image, int? Replicas, FleetResources? Resources, int? Port);
}
