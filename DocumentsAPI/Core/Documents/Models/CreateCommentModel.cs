namespace DocumentsAPI.Core.Documents.Models
{
    public record CreateCommentModel(
        string Text,
        Guid DocumentId,
        Guid UserId
    );
}
