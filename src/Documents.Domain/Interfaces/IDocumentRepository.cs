using Documents.Domain.Entities;

namespace Documents.Domain.Interfaces;

public interface IDocumentRepository
{
    Task<IReadOnlyCollection<Document>> ListAsync(CancellationToken cancellationToken = default);
    Task<Document?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<Document> CreateAsync(Document document, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Document document, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
