namespace FleetManager.Models.Responses.Fleet
{
    public record FleetCreatedResponse(DateTime CreatedDateTime, string Name, string @Namespace);
    public record FleetUpdatedResponse(DateTime CreatedDateTime, string Name, string @Namespace);
    public record FleetDeletedResponse(DateTime CreatedDateTime, string Name, string @Namespace);
}
