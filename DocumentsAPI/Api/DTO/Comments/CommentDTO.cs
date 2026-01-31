namespace DocumentsAPI.Api.DTO.Comments
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
