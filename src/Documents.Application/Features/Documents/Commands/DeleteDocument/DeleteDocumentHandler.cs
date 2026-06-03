using Documents.Domain.Interfaces;
using MediatR;

namespace Documents.Application.Features.Documents.Commands.DeleteDocument;

public class DeleteDocumentHandler : IRequestHandler<DeleteDocumentCommand, bool>
{
    private readonly IDocumentRepository _documentRepository;

    public DeleteDocumentHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        return _documentRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
