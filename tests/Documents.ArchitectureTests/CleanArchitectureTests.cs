using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Documents.ArchitectureTests;

public class CleanArchitectureTests
{
    private static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(typeof(Documents.Domain.Entities.Document).Assembly)
        .Build();

    [Fact]
    public void Domain_Should_Not_Depend_On_Other_Layers()
    {
        var domain = Types().That().ResideInNamespace("Documents.Domain").As("Domain");
        var application = Types().That().ResideInNamespace("Documents.Application").As("Application");
        var infrastructure = Types().That().ResideInNamespace("Documents.Infrastructure").As("Infrastructure");
        var api = Types().That().ResideInNamespace("Documents.API").As("API");

        Types().That().Are(domain)
            .Should().NotDependOnAny(application)
            .AndShould().NotDependOnAny(infrastructure)
            .AndShould().NotDependOnAny(api)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void Application_Should_Not_Depend_On_Infrastructure_Or_Api()
    {
        var application = Types().That().ResideInNamespace("Documents.Application").As("Application");
        var infrastructure = Types().That().ResideInNamespace("Documents.Infrastructure").As("Infrastructure");
        var api = Types().That().ResideInNamespace("Documents.API").As("API");

        Types().That().Are(application)
            .Should().NotDependOnAny(infrastructure)
            .AndShould().NotDependOnAny(api)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void Infrastructure_Should_Not_Depend_On_Api()
    {
        var infrastructure = Types().That().ResideInNamespace("Documents.Infrastructure").As("Infrastructure");
        var api = Types().That().ResideInNamespace("Documents.API").As("API");

        Types().That().Are(infrastructure)
            .Should().NotDependOnAny(api)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void Controllers_Should_Not_Depend_On_Repositories_Directly()
    {
        var repositories = Interfaces().That().HaveNameEndingWith("Repository");

        Classes().That().ResideInNamespace("Documents.API.Controllers")
            .Should().NotDependOnAny(repositories)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}
