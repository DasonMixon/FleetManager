namespace FleetManager.Models.Requests.Fleet
{
    public record DeleteFleetRequest(
        string Name,
        string Namespace);
}
