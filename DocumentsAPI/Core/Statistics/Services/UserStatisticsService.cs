using DocumentsAPI.Core.Statistics.Interfaces;
using DocumentsAPI.Core.Statistics.Models;
using DocumentsAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DocumentsAPI.Core.Statistics.Services
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly AppDbContext _db;
        public UserStatisticsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserStatistics>> GetStatisticsByUserIdAsync(Guid userId)
        {
            return await _db.UserStatistics
                .Where(us => us.UserId == userId)
                .OrderByDescending(us => us.Year)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
