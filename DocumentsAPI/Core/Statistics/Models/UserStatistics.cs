using DocumentsAPI.Core.Users.Models;

namespace DocumentsAPI.Core.Statistics.Models
{
    public class UserStatistics
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int DocumentsCreated { get; set; }

        public User User { get; set; }
    }
}
