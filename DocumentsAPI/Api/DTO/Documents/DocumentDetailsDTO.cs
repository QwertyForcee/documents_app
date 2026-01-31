namespace DocumentsAPI.Api.DTO.Documents
{
    public record DocumentDetailsDTO(
        Guid Id,
        string Name,
        string Description,
        DateTimeOffset CreatedAt,
        DateTimeOffset ExpirationDate,
        double? Latitude,
        double? Longitude
    );
}
