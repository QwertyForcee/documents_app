using DocumentsAPI.Core.Users.Models;

namespace DocumentsAPI.Core.Documents.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public DocumentStatus Status { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }


        public ICollection<Comment> Comments { get; set; }
    }
}
