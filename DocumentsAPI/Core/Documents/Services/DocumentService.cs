using DocumentsAPI.Core.Documents.Interfaces;
using DocumentsAPI.Core.Documents.Models;
using DocumentsAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DocumentsAPI.Core.Documents.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _db;

        public DocumentService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Document>> GetDocumentsAsync(Guid userId)
        {
            return await _db.Documents
                .Where(d => d.OwnerId == userId && d.Status == DocumentStatus.Active)
                .OrderByDescending(d => d.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Document> GetDocumentAsync(Guid id, Guid userId)
        {
            return await _db.Documents
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(d =>
                    d.Id == id &&
                    d.OwnerId == userId &&
                    d.Status != DocumentStatus.Expired);
        }

        public async Task UpdateDocumentAsync(Guid documentId, Guid userId, DocumentChangesModel changes)
        {
            var document = await _db.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.OwnerId == userId && d.Status == DocumentStatus.Active);

            if (document is null)
                throw new InvalidOperationException("Document not found or not editable");

            document.Name = changes.Name;
            document.Description = changes.Description;
            document.ExpirationDate = changes.ExpirationDate;
            document.Latitude = changes.Latitude;
            document.Longitude = changes.Longitude;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(Guid id, Guid userId)
        {
            var document = await _db.Documents.FirstOrDefaultAsync(d => d.Id == id && d.OwnerId == userId && d.Status != DocumentStatus.Expired);

            if (document is null)
                return;

            document.Status = DocumentStatus.Expired;
            document.ExpirationDate = DateTimeOffset.UtcNow;

            await _db.SaveChangesAsync();
        }

        public async Task<Guid> CreateDocumentAsync(Guid userId, CreateDocumentModel newDocumentModel)
        {
            var document = new Document
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                Status = DocumentStatus.Active,
                Name = newDocumentModel.Name,
                Description = newDocumentModel.Description,
                CreatedAt = DateTimeOffset.UtcNow,
                ExpirationDate = newDocumentModel.ExpirationDate,
                Latitude = newDocumentModel.Latitude,
                Longitude = newDocumentModel.Longitude
            };

            _db.Documents.Add(document);
            await _db.SaveChangesAsync();

            return document.Id;
        }
    }
}
