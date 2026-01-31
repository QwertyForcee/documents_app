namespace DocumentsAPI.Api.DTO.Documents
{
    public record DocumentListItemDTO(
        Guid Id,
        string Name,
        string Description,
        DateTimeOffset CreatedAt,
        DateTimeOffset ExpirationDate
    );
}
