namespace DocumentsAPI.Core.Documents.Models
{
    public record DocumentChangesModel(
        string Name,
        string Description,
        DateTimeOffset ExpirationDate,
        double? Latitude,
        double? Longitude
    );
}
