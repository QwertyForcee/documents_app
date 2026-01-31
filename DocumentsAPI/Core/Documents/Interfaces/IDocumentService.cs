using DocumentsAPI.Core.Documents.Models;

namespace DocumentsAPI.Core.Documents.Interfaces
{
    public interface IDocumentService
    {
        Task<List<Document>> GetDocumentsAsync(Guid userId);
        Task<Document> GetDocumentAsync(Guid id, Guid userId);
        Task<Guid> CreateDocumentAsync(Guid userId, CreateDocumentModel newDocumentModel);
        Task UpdateDocumentAsync(Guid documentId, Guid userId, DocumentChangesModel documentChanges);
        Task DeleteDocumentAsync(Guid id, Guid userId);
    }
}
