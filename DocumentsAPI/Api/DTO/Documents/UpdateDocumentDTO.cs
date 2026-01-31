namespace DocumentsAPI.Api.DTO.Documents
{
    public record UpdateDocumentDTO(
        string Name,
        string Description,
        DateTimeOffset ExpirationDate,
        double? Latitude,
        double? Longitude
    );
}
