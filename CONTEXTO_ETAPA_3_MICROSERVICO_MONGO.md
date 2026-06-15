# Contexto Atual - Microservico MongoDB

Este arquivo descreve somente o que existe hoje neste repositorio, como o servico funciona e o que ainda falta considerando o que o `Entrega03.md` pede.

## Escopo deste repositorio

Este repositorio implementa o microservico do dominio `Documents`, que cobre o item 3 da entrega:

- microservico independente em ASP.NET Core
- persistencia em MongoDB Atlas
- CRUD completo de documentos
- documentacao via OpenAPI/Swagger
- dockerizacao do servico

Ele nao contem o microfrontend, o BFF, o microservico SQL, a Azure Function nem o API Gateway. Esses itens fazem parte da entrega geral, mas nao estao neste codigo.

## Estrutura atual

```text
src
|-- Documents.API
|-- Documents.Application
|-- Documents.Domain
`-- Documents.Infrastructure
tests
`-- Documents.ArchitectureTests
```

## Stack e organizacao

O codigo esta organizado em Clean Architecture com separacao entre `API`, `Application`, `Domain` e `Infrastructure`.

Tambem ha organizacao por feature na camada `Application`, com slices para:

- `CreateDocument`
- `UpdateDocument`
- `DeleteDocument`
- `GetDocumentById`
- `ListDocuments`

Tecnologias encontradas no repositorio:

- .NET 9
- ASP.NET Core Web API
- MediatR
- FluentValidation
- MongoDB.Driver
- Swagger / OpenAPI
- xUnit + ArchUnitNET

## Modelo de dados atual

A entidade persistida e `Document`, com estes campos:

- `Id`
- `Title`
- `Category`
- `OwnerId`
- `OwnerName`
- `Status`
- `Content`
- `Tags`
- `CreatedAtUtc`
- `UpdatedAtUtc`

O `Id` usa `ObjectId` do MongoDB com representacao em `string`.

## Endpoints implementados

O controller exposto em `src/Documents.API/Controllers/DocumentsController.cs` publica:

- `GET /api/documents`
- `GET /api/documents/{id}`
- `POST /api/documents`
- `PUT /api/documents/{id}`
- `DELETE /api/documents/{id}`

As operacoes passam pelo `IMediator`; o controller nao acessa repositorio diretamente.

## Como a conexao com o Mongo funciona hoje

A configuracao e lida da secao `MongoDb` em:

- `src/Documents.API/appsettings.json`
- `src/Documents.API/appsettings.Development.json`

Campos usados:

- `ConnectionString`
- `DatabaseName`
- `DocumentsCollectionName`

Valores configurados no repositorio:

- `DatabaseName`: `gestaorh_documents_ms`
- `DocumentsCollectionName`: `documents`

Na infraestrutura, `AddInfrastructure` registra:

- `MongoDbOptions` via `IOptions`
- `MongoDbContext` como `Singleton`
- `IDocumentRepository` com implementacao `DocumentRepository`

O fluxo da conexao e este:

1. `Program.cs` chama `builder.Services.AddInfrastructure(builder.Configuration)`.
2. `DependencyInjection.cs` faz o bind da secao `MongoDb`.
3. `MongoDbContext` cria um `MongoClient` com a `ConnectionString`.
4. O contexto abre o banco configurado em `DatabaseName`.
5. O repositorio usa a collection configurada em `DocumentsCollectionName`.

Hoje a string de conexao esta preenchida nos arquivos de configuracao do projeto. O repositorio nao usa variaveis de ambiente para essa configuracao.

## Comportamento da API

O pipeline atual da API faz o seguinte:

- usa controllers com JSON em camelCase
- registra OpenAPI e Swagger
- aplica validacao com `FluentValidation` via pipeline do MediatR
- usa `ExceptionHandlingMiddleware` para retornar:
  - `400` em erro de validacao
  - `500` em erro inesperado
- ativa `UseHttpsRedirection()`

O Swagger so e exposto em ambiente de desenvolvimento.

## Execucao local

Em `launchSettings.json`, a API esta configurada para rodar localmente em:

- `http://localhost:5102`
- `https://localhost:7102` no perfil HTTPS

Com a aplicacao em desenvolvimento, os endpoints de documentacao sao:

- `http://localhost:5102/swagger`
- `http://localhost:5102/openapi/v1.json`

## Docker e publicacao

Existe um `Dockerfile` no repositorio com:

- build e publish da solucao em .NET 9
- imagem final `mcr.microsoft.com/dotnet/aspnet:9.0`
- exposicao da porta `8080`
- `ASPNETCORE_URLS=http://+:8080`

Tambem existem:

- `.dockerignore`
- `.github/workflows/docker-mongodb.yml`

O workflow publica imagem no Docker Hub usando os secrets:

- `DOCKERHUB_USERNAME`
- `DOCKERHUB_TOKEN`

e gera tags para `${DOCKERHUB_USERNAME}/pjbl-mongodb`.

## Testes existentes

Existe um projeto de testes de arquitetura em `tests/Documents.ArchitectureTests` cobrindo:

- `Domain` sem dependencia de outras camadas
- `Application` sem dependencia de `Infrastructure` e `API`
- `Infrastructure` sem dependencia de `API`
- controllers sem dependencia direta de repositorios

## O que foi possivel confirmar daqui

Foi possivel confirmar por leitura do codigo:

- a arquitetura em camadas existe
- o CRUD HTTP esta implementado
- a conexao com Mongo esta configurada
- Swagger/OpenAPI esta configurado
- Dockerfile e workflow de publicacao existem
- ha testes de arquitetura no repositorio

Nao foi possivel confirmar execucao de `dotnet build` e `dotnet test` neste ambiente porque o restore falhou ao acessar `https://api.nuget.org/v3/index.json`.

## O que ainda falta para a entrega, comparando com `Entrega03.md`

No escopo deste repositorio, ainda faltam evidencias ou artefatos finais que o arquivo de entrega pede:

- confirmar o servico rodando com Swagger acessivel
- confirmar o CRUD funcionando contra o MongoDB Atlas
- disponibilizar link do repositorio publico no GitHub
- disponibilizar link da imagem publicada no Docker Hub
- atualizar o `README.md` com a documentacao final exigida, se necessario

Fora do escopo deste repositorio, a entrega geral ainda pede componentes que nao estao aqui:

- microfrontend
- BFF com `GET /aggregated-data`
- microservico SQL
- Azure Function
- API Gateway
- PDF/ARC42 com links finais
- C4 Model atualizado
- Canvas
- prints
- video no YouTube

## Arquivos principais para consultar

- `src/Documents.API/Program.cs`
- `src/Documents.API/Controllers/DocumentsController.cs`
- `src/Documents.API/appsettings.json`
- `src/Documents.API/appsettings.Development.json`
- `src/Documents.API/Properties/launchSettings.json`
- `src/Documents.Infrastructure/DependencyInjection.cs`
- `src/Documents.Infrastructure/Persistence/MongoDbContext.cs`
- `src/Documents.Infrastructure/Repositories/DocumentRepository.cs`
- `src/Documents.Domain/Entities/Document.cs`
- `tests/Documents.ArchitectureTests/CleanArchitectureTests.cs`
- `Dockerfile`
- `.github/workflows/docker-mongodb.yml`
