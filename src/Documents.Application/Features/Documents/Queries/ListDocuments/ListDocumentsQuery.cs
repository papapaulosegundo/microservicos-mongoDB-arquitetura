using Documents.Application.Common.DTOs;
using MediatR;

namespace Documents.Application.Features.Documents.Queries.ListDocuments;

public record ListDocumentsQuery : IRequest<IReadOnlyCollection<DocumentResponseDto>>;
