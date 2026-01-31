using DocumentsAPI.Core.Documents.Interfaces;
using DocumentsAPI.Core.Documents.Models;
using DocumentsAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DocumentsAPI.Core.Documents.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _db;
        public CommentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Comment> CreateCommentAsync(CreateCommentModel model)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.UtcNow,
                Text = model.Text,
                UserId = model.UserId,
                DocumentId = model.DocumentId
            };

            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();

            return comment;
        }

        public async Task DeleteCommentAsync(Guid commentId, Guid userId)
        {
            var comment = await _db.Comments.FirstOrDefaultAsync(d => d.Id == commentId && d.UserId == userId);

            if (comment is null)
                return;

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetCommentsAsync(Guid documentId)
        {
            return await _db.Comments
                .Where(c => c.DocumentId == documentId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
