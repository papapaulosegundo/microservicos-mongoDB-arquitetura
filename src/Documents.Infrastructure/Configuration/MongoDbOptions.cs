namespace Documents.Infrastructure.Configuration;

public class MongoDbOptions
{
    public const string SectionName = "MongoDb";

    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = "gestaorh_documents_ms";
    public string DocumentsCollectionName { get; set; } = "documents";
}
