using Documents.Application.Common.DTOs;
using MediatR;

namespace Documents.Application.Features.Documents.Queries.GetDocumentById;

public record GetDocumentByIdQuery(string Id) : IRequest<DocumentResponseDto?>;
