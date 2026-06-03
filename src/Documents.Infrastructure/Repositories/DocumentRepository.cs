using Documents.Domain.Entities;
using Documents.Domain.Interfaces;
using Documents.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Documents.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly MongoDbContext _context;

    public DocumentRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Document>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Documents.Find(FilterDefinition<Document>.Empty)
            .SortByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Document?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Documents.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Document> CreateAsync(Document document, CancellationToken cancellationToken = default)
    {
        await _context.Documents.InsertOneAsync(document, cancellationToken: cancellationToken);
        return document;
    }

    public async Task<bool> UpdateAsync(Document document, CancellationToken cancellationToken = default)
    {
        var result = await _context.Documents.ReplaceOneAsync(
            x => x.Id == document.Id,
            document,
            cancellationToken: cancellationToken);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _context.Documents.DeleteOneAsync(x => x.Id == id, cancellationToken);
        return result.DeletedCount > 0;
    }
}
