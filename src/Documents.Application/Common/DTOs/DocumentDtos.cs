namespace Documents.Application.Common.DTOs;

public record DocumentResponseDto(
    string Id,
    string Title,
    string Category,
    string OwnerId,
    string OwnerName,
    string Status,
    int PendingSignatures,
    string Content,
    IReadOnlyCollection<string> Tags,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);

public record CreateDocumentRequestDto(
    string Title,
    string Category,
    string OwnerId,
    string OwnerName,
    string Status,
    int PendingSignatures,
    string Content,
    IReadOnlyCollection<string>? Tags);

public record UpdateDocumentRequestDto(
    string Title,
    string Category,
    string OwnerId,
    string OwnerName,
    string Status,
    int PendingSignatures,
    string Content,
    IReadOnlyCollection<string>? Tags);
