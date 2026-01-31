namespace DocumentsAPI.Api.DTO.Comments
{
    public record CreateCommentDTO(
        string Text,
        Guid DocumentId
    );
}
