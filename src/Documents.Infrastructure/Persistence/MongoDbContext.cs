using Documents.Domain.Entities;
using Documents.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Documents.Infrastructure.Persistence;

public class MongoDbContext
{
    public IMongoCollection<Document> Documents { get; }

    public MongoDbContext(IOptions<MongoDbOptions> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        Documents = database.GetCollection<Document>(settings.DocumentsCollectionName);
    }
}
