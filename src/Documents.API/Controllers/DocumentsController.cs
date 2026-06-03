using Documents.Application.Common.DTOs;
using Documents.Application.Features.Documents.Commands.CreateDocument;
using Documents.Application.Features.Documents.Commands.DeleteDocument;
using Documents.Application.Features.Documents.Commands.UpdateDocument;
using Documents.Application.Features.Documents.Queries.GetDocumentById;
using Documents.Application.Features.Documents.Queries.ListDocuments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Documents.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<DocumentResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ListDocumentsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DocumentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetDocumentByIdQuery(id), cancellationToken);
        return response is null ? NotFound() : Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DocumentResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateDocumentRequestDto request, CancellationToken cancellationToken)
    {
        var command = new CreateDocumentCommand(
            request.Title,
            request.Category,
            request.OwnerId,
            request.OwnerName,
            request.Status,
            request.Content,
            request.Tags);

        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DocumentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateDocumentRequestDto request, CancellationToken cancellationToken)
    {
        var command = new UpdateDocumentCommand(
            id,
            request.Title,
            request.Category,
            request.OwnerId,
            request.OwnerName,
            request.Status,
            request.Content,
            request.Tags);

        var response = await _mediator.Send(command, cancellationToken);
        return response is null ? NotFound() : Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteDocumentCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
