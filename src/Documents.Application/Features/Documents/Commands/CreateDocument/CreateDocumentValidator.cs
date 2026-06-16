using FluentValidation;

namespace Documents.Application.Features.Documents.Commands.CreateDocument;

public class CreateDocumentValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Category).NotEmpty().MaximumLength(80);
        RuleFor(x => x.OwnerId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.OwnerName).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PendingSignatures).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Content).NotEmpty();
    }
}
