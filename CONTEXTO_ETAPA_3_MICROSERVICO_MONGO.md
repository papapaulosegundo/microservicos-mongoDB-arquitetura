# Contexto da Etapa 3 - Microservico 1 MongoDB

Este arquivo registra o contexto completo do que foi feito no item 3 da atividade para manter continuidade nas proximas conversas, nas proximas entregas e na integracao com o BFF.

## Objetivo desta etapa

Implementar o **Microservico 1** da arquitetura distribuida, usando:

- banco `MongoDB Atlas`
- dominio independente
- CRUD completo
- Swagger/OpenAPI documentado

Nesta solucao, o dominio escolhido para o microservico foi:

- `Documents`

## Decisao arquitetural adotada

O dominio `Documents` foi escolhido para o microservico MongoDB porque combina melhor com armazenamento NoSQL:

- estrutura flexivel
- metadados variaveis
- tags
- conteudo textual
- evolucao simples do schema

Essa decisao tambem conversa bem com o restante da arquitetura:

- frontend ja possui `mfe-documents`
- BFF ja possui contrato `/documents`
- o futuro microservico SQL pode ficar com um dominio mais relacional, como `People`

## Relacao com as etapas anteriores

### Etapa 1 - Microfrontend

Na etapa 1, o frontend foi preparado para consumir:

- `GET /documents`
- `GET /documents/:id`

mas ainda por mocks.

### Etapa 2 - BFF

Na etapa 2, o BFF foi preparado para:

- expor `/documents`
- agregar dados em `/aggregated-data`
- consumir um microservico real de documentos futuramente

Agora, nesta etapa, esse microservico comeca a existir de fato.

## O que foi implementado neste repositorio

Foi criado um novo repositorio para o microservico MongoDB:

- `microservicos-mongoDB-arquitetura`

Dentro dele, foi montada uma solucao .NET completa para o dominio `Documents`.

## Estrutura criada

```text
microservicos-mongoDB-arquitetura
|-- src
|   |-- Documents.API
|   |-- Documents.Application
|   |-- Documents.Domain
|   `-- Documents.Infrastructure
|-- tests
|   `-- Documents.ArchitectureTests
|-- DocumentsService.sln
|-- Dockerfile
|-- README.md
|-- .gitignore
`-- CONTEXTO_ETAPA_3_MICROSERVICO_MONGO.md
```

## Padroes arquiteturais aplicados

O projeto foi estruturado com:

- Clean Architecture
  - `API`
  - `Application`
  - `Domain`
  - `Infrastructure`
- Vertical Slice
  - comandos e queries separados por funcionalidade
- separacao clara entre contrato HTTP, regra de aplicacao e persistencia

## Dominio implementado

Foi criada a entidade principal:

- `Document`

Campos atuais:

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

## CRUD implementado

O microservico ficou com CRUD completo para `Documents`.

Endpoints expostos:

- `GET /api/documents`
- `GET /api/documents/{id}`
- `POST /api/documents`
- `PUT /api/documents/{id}`
- `DELETE /api/documents/{id}`

## Slices criados

Na camada `Application`, foram criados slices para:

- `CreateDocument`
- `UpdateDocument`
- `DeleteDocument`
- `GetDocumentById`
- `ListDocuments`

Tambem foram criados:

- DTOs
- validators
- handlers
- pipeline de validacao

## Integracao com MongoDB

Foi configurada a integracao com o MongoDB Atlas usando:

- `MongoDB.Driver`

Configuracao atual:

- `DatabaseName = gestaorh_documents_ms`
- `DocumentsCollectionName = documents`

Arquivo de configuracao:

- `src/Documents.API/appsettings.json`
- `src/Documents.API/appsettings.Development.json`

Connection string esperada:

```json
"MongoDb": {
  "ConnectionString": "mongodb+srv://USUARIO:SENHA@clusterarquitetura.s1lyczy4.mongodb.net/?retryWrites=true&w=majority&appName=ClusterArquitetura",
  "DatabaseName": "gestaorh_documents_ms",
  "DocumentsCollectionName": "documents"
}
```

## Swagger / OpenAPI

O projeto foi preparado com OpenAPI e Scalar para documentacao da API.

Endpoints esperados em desenvolvimento:

- `http://localhost:5102/openapi/v1.json`
- `http://localhost:5102/scalar`

## Porta padronizada

Foi alinhada a porta local do microservico para:

- `5102`

Essa escolha foi feita para casar com a configuracao ja prevista no BFF.

## Dockerizacao

Foi criado um `Dockerfile` para a futura publicacao da imagem.

Objetivo:

- facilitar entrega
- preparar push no Docker Hub
- permitir execucao isolada do servico

## Testes de arquitetura

Foi criado um projeto separado:

- `tests/Documents.ArchitectureTests`

Esses testes validam regras basicas de:

- isolamento do dominio
- independencia da aplicacao
- infraestrutura sem dependencia da API
- controllers sem acesso direto a repository

## Validacao realizada

Foi validado nesta etapa:

- `dotnet build DocumentsService.sln`
- `dotnet test tests/Documents.ArchitectureTests/Documents.ArchitectureTests.csproj`

Resultado:

- build com sucesso
- testes de arquitetura aprovados

## O que ainda falta fazer agora

Depois de preencher usuario e senha do Atlas, os proximos passos imediatos sao:

1. rodar o microservico localmente
2. abrir o `Scalar`
3. testar o CRUD
4. confirmar no Atlas se a collection `documents` foi criada
5. inserir pelo menos um documento de teste
6. depois integrar o BFF a esse servico real

## Como esta previsto que vai ficar depois

Quando a integracao com o BFF for feita, o fluxo esperado sera:

1. `mfe-documents` chama o BFF
2. BFF chama este microservico MongoDB
3. microservico consulta a collection `documents`
4. BFF adapta ou agrega a resposta
5. frontend recebe dados reais

## Premissas para as proximas conversas

Para manter consistencia nas proximas etapas, considerar sempre:

1. Este repositorio representa o microservico MongoDB do dominio `Documents`.
2. O banco principal deste servico e `MongoDB Atlas`.
3. A porta local esperada e `5102`.
4. O nome do banco definido foi `gestaorh_documents_ms`.
5. A collection principal definida foi `documents`.
6. O consumo pelo frontend nao sera direto; passara pelo BFF.
7. O BFF deve substituir mocks por chamadas reais quando essa integracao for ligada.

## Sugestao natural de continuidade

A sequencia mais natural a partir daqui e:

1. subir o microservico local
2. testar `POST /api/documents`
3. testar `GET /api/documents`
4. verificar criacao dos dados no Atlas
5. voltar ao repositorio do BFF
6. trocar `UseMocks` para `false`
7. apontar `DocumentsBaseUrl` para `http://localhost:5102/`
8. integrar o `DocumentsBffClient` ao contrato real

## Arquivos mais importantes para retomar rapido

Se precisarmos retomar rapido a etapa 3 depois, olhar primeiro:

- `src/Documents.API/Program.cs`
- `src/Documents.API/Controllers/DocumentsController.cs`
- `src/Documents.API/appsettings.json`
- `src/Documents.API/appsettings.Development.json`
- `src/Documents.Domain/Entities/Document.cs`
- `src/Documents.Domain/Interfaces/IDocumentRepository.cs`
- `src/Documents.Infrastructure/Persistence/MongoDbContext.cs`
- `src/Documents.Infrastructure/Repositories/DocumentRepository.cs`
- `tests/Documents.ArchitectureTests/CleanArchitectureTests.cs`
- `Dockerfile`
- `README.md`
- `CONTEXTO_ETAPA_3_MICROSERVICO_MONGO.md`

## Observacao final

Nesta etapa, o foco principal foi tirar o microservico MongoDB do zero e deixa-lo pronto para:

- demonstracao isolada
- validacao academica
- integracao futura com o BFF

Com isso, o item 3 ja possui uma base real, coerente com o restante da arquitetura e pronta para os proximos passos da entrega.
