using Documents.Application.Common.DTOs;
using Documents.Application.Common.Mappings;
using Documents.Domain.Entities;
using Documents.Domain.Interfaces;
using MediatR;

namespace Documents.Application.Features.Documents.Commands.CreateDocument;

public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, DocumentResponseDto>
{
    private readonly IDocumentRepository _documentRepository;

    public CreateDocumentHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<DocumentResponseDto> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var document = new Document
        {
            Title = request.Title,
            Category = request.Category,
            OwnerId = request.OwnerId,
            OwnerName = request.OwnerName,
            Status = request.Status,
            PendingSignatures = request.PendingSignatures,
            Content = request.Content,
            Tags = request.Tags?.ToList() ?? [],
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        var created = await _documentRepository.CreateAsync(document, cancellationToken);
        return created.ToResponseDto();
    }
}
