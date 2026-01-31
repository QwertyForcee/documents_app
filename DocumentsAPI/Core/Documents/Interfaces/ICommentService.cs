using DocumentsAPI.Core.Documents.Models;

namespace DocumentsAPI.Core.Documents.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsAsync(Guid documentId);
        Task<Comment> CreateCommentAsync(CreateCommentModel model);
        Task DeleteCommentAsync(Guid commentId, Guid userId);
    }
}
