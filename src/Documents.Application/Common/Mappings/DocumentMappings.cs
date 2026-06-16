using Documents.Application.Common.DTOs;
using Documents.Domain.Entities;

namespace Documents.Application.Common.Mappings;

public static class DocumentMappings
{
    public static DocumentResponseDto ToResponseDto(this Document document)
    {
        return new DocumentResponseDto(
            document.Id,
            document.Title,
            document.Category,
            document.OwnerId,
            document.OwnerName,
            document.Status,
            document.PendingSignatures,
            document.Content,
            document.Tags,
            document.CreatedAtUtc,
            document.UpdatedAtUtc);
    }
}
