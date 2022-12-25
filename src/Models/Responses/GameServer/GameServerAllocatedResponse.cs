namespace FleetManager.Models.Responses.GameServer
{
    public class GameServerAllocatedResponse
    {
        public string GameServerName { get; set; } = default!;
        public string State { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}
