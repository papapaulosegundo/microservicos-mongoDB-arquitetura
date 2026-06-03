using Documents.Application.Common.DTOs;
using Documents.Application.Common.Mappings;
using Documents.Domain.Interfaces;
using MediatR;

namespace Documents.Application.Features.Documents.Queries.GetDocumentById;

public class GetDocumentByIdHandler : IRequestHandler<GetDocumentByIdQuery, DocumentResponseDto?>
{
    private readonly IDocumentRepository _documentRepository;

    public GetDocumentByIdHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<DocumentResponseDto?> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await _documentRepository.GetByIdAsync(request.Id, cancellationToken);
        return document?.ToResponseDto();
    }
}
