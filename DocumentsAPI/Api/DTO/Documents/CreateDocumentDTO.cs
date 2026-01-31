namespace DocumentsAPI.Api.DTO.Documents
{
    public record CreateDocumentDTO(
        string Name,
        string Description,
        DateTimeOffset ExpirationDate,
        double? Latitude,
        double? Longitude
    );
}
