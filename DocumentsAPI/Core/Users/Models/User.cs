using DocumentsAPI.Core.Documents.Models;
using DocumentsAPI.Core.Statistics.Models;

namespace DocumentsAPI.Core.Users.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserStatistics> Statistics { get; set; }
    }
}
