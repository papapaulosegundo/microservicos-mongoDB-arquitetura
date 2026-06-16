using FluentValidation;

namespace Documents.Application.Features.Documents.Commands.UpdateDocument;

public class UpdateDocumentValidator : AbstractValidator<UpdateDocumentCommand>
{
    public UpdateDocumentValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Category).NotEmpty().MaximumLength(80);
        RuleFor(x => x.OwnerId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.OwnerName).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PendingSignatures).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Content).NotEmpty();
    }
}
