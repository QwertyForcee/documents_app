using DocumentsAPI.Core.Documents.Interfaces;
using DocumentsAPI.Core.Documents.Models;
using DocumentsAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DocumentsAPI.Core.Documents.Services
{
    public class CleanUpOldDocumentsService : ICleanUpOldDocumentsService
    {
        private readonly AppDbContext _db;

        public CleanUpOldDocumentsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task CleanUpOldDocumentsAsync()
        {
            var currentDate = DateTimeOffset.UtcNow;
            var thresholdDate = currentDate.AddDays(-30);

            await _db.Documents.Where(d => d.Status == DocumentStatus.Active && d.ExpirationDate >= currentDate)
                .ExecuteUpdateAsync(d => d.SetProperty(d => d.Status, DocumentStatus.Expired));

            await _db.Documents.Where(d => d.Status == DocumentStatus.Expired && d.ExpirationDate <= thresholdDate).ExecuteDeleteAsync();
            await _db.SaveChangesAsync();
        }
    }
}
