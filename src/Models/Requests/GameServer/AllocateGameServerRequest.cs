namespace FleetManager.Models.Requests.GameServer
{
    public record AllocateGameServerRequest(
        string FleetName,
        string Namespace);
}
