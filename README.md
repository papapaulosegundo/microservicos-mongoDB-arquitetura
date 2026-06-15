# Documents Microservice

Microservico responsavel pelo dominio de `Documents` da arquitetura distribuida do PJBL. Ele foi implementado em **ASP.NET Core / C#** com **MongoDB Atlas** como banco NoSQL principal.

## Papel na arquitetura

Este servico representa o item 3 da atividade:

- microservico independente
- banco MongoDB Atlas
- CRUD completo
- Swagger/OpenAPI documentado
- preparado para ser consumido pelo BFF

## Stack

- .NET 9
- ASP.NET Core Web API
- MongoDB Atlas
- MediatR
- FluentValidation
- Scalar/OpenAPI
- xUnit + ArchUnitNET

## Estrutura

```text
src
|-- Documents.API
|-- Documents.Application
|-- Documents.Domain
`-- Documents.Infrastructure
tests
`-- Documents.ArchitectureTests
```

## Endpoints

- `GET /api/documents`
- `GET /api/documents/{id}`
- `POST /api/documents`
- `PUT /api/documents/{id}`
- `DELETE /api/documents/{id}`

## Configuracao do MongoDB

Edite [appsettings.json](./src/Documents.API/appsettings.json) ou `appsettings.Development.json`:

```json
"MongoDb": {
  "ConnectionString": "mongodb+srv://<db_user>:<db_password>@clusterarquitetura.s1lyczy4.mongodb.net/?retryWrites=true&w=majority&appName=ClusterArquitetura",
  "DatabaseName": "gestaorh_documents_ms",
  "DocumentsCollectionName": "documents"
}
```

Substitua:

- `<db_user>` pelo usuario do Atlas
- `<db_password>` pela senha do Atlas

## Como rodar localmente

```bash
dotnet restore
dotnet run --project src/Documents.API
```

OpenAPI:

- `http://localhost:5102/openapi/v1.json`
- `http://localhost:5102/scalar`

## Docker

```bash
docker build -t seuusuario/pjbl/documents-ms:v1 .
docker run -p 8080:8080 seuusuario/pjbl/documents-ms:v1
```

## Integracao com o BFF

O BFF deve consumir este servico pela URL base do microservico. Quando for integrar:

- trocar o BFF para `UseMocks = false`
- apontar `DocumentsBaseUrl` para a porta deste servico

## Alunos

- Paulo César Muchalski
- Paulo Vitor
- Giulia Casteluci
- Juliano
- Gabriela Otte
