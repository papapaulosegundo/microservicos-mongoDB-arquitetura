using MediatR;

namespace Documents.Application.Features.Documents.Commands.DeleteDocument;

public record DeleteDocumentCommand(string Id) : IRequest<bool>;
