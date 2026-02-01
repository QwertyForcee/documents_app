using DocumentsAPI.Core.Statistics.Models;

namespace DocumentsAPI.Core.Statistics.Interfaces
{
    public interface IUserStatisticsService
    {
        Task<List<UserStatistics>> GetStatisticsByUserIdAsync(Guid userId);
    }
}
