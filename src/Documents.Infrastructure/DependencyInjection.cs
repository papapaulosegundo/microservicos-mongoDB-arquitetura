using Documents.Domain.Interfaces;
using Documents.Infrastructure.Configuration;
using Documents.Infrastructure.Persistence;
using Documents.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Documents.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.SectionName));
        services.AddSingleton<MongoDbContext>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        return services;
    }
}
