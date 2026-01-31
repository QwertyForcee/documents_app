namespace DocumentsAPI.Core.Documents.Models
{
    public record CreateDocumentModel(
        string Name,
        string Description,
        DateTimeOffset ExpirationDate,
        double? Latitude,
        double? Longitude
    );
}
