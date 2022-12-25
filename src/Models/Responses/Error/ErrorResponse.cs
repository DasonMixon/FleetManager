namespace FleetManager.Models.Responses.Error
{
    public record ErrorResponse(IList<ErrorModel> Errors);
    public record ErrorModel(string FieldName, string Message);
}
