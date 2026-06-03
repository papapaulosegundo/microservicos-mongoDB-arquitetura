using Documents.Application.Common.DTOs;
using MediatR;

namespace Documents.Application.Features.Documents.Commands.UpdateDocument;

public record UpdateDocumentCommand(
    string Id,
    string Title,
    string Category,
    string OwnerId,
    string OwnerName,
    string Status,
    string Content,
    IReadOnlyCollection<string>? Tags) : IRequest<DocumentResponseDto?>;
