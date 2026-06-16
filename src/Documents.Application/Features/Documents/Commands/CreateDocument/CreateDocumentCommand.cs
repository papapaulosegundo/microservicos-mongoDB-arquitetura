using Documents.Application.Common.DTOs;
using MediatR;

namespace Documents.Application.Features.Documents.Commands.CreateDocument;

public record CreateDocumentCommand(
    string Title,
    string Category,
    string OwnerId,
    string OwnerName,
    string Status,
    int PendingSignatures,
    string Content,
    IReadOnlyCollection<string>? Tags) : IRequest<DocumentResponseDto>;
