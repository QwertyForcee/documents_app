using DocumentsAPI.Core.Users.Models;

namespace DocumentsAPI.Core.Documents.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;


        public Guid DocumentId { get; set; }
        public Document Document { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
