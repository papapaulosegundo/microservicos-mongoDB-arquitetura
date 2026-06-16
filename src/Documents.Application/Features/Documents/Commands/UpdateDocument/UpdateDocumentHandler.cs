using Documents.Application.Common.DTOs;
using Documents.Application.Common.Mappings;
using Documents.Domain.Interfaces;
using MediatR;

namespace Documents.Application.Features.Documents.Commands.UpdateDocument;

public class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand, DocumentResponseDto?>
{
    private readonly IDocumentRepository _documentRepository;

    public UpdateDocumentHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<DocumentResponseDto?> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _documentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return null;
        }

        existing.Title = request.Title;
        existing.Category = request.Category;
        existing.OwnerId = request.OwnerId;
        existing.OwnerName = request.OwnerName;
        existing.Status = request.Status;
        existing.PendingSignatures = request.PendingSignatures;
        existing.Content = request.Content;
        existing.Tags = request.Tags?.ToList() ?? [];
        existing.UpdatedAtUtc = DateTime.UtcNow;

        var updated = await _documentRepository.UpdateAsync(existing, cancellationToken);
        return updated ? existing.ToResponseDto() : null;
    }
}
