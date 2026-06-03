using Documents.Application.Common.DTOs;
using Documents.Application.Common.Mappings;
using Documents.Domain.Interfaces;
using MediatR;

namespace Documents.Application.Features.Documents.Queries.ListDocuments;

public class ListDocumentsHandler : IRequestHandler<ListDocumentsQuery, IReadOnlyCollection<DocumentResponseDto>>
{
    private readonly IDocumentRepository _documentRepository;

    public ListDocumentsHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<IReadOnlyCollection<DocumentResponseDto>> Handle(ListDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _documentRepository.ListAsync(cancellationToken);
        return documents.Select(x => x.ToResponseDto()).ToList();
    }
}
