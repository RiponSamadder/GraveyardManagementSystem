namespace GMS.Application.DTOs;

// ───── Memorial Profile ─────
public record MemorialProfileListDto(
    long ProfileId,
    long DeceasedId,
    string DeceasedFullName,
    string? Slug,
    string? ProfileStatusCode,
    bool IsPublic,
    DateTime CreatedAt
);

public record MemorialProfileDetailDto(
    long ProfileId,
    long DeceasedId,
    string DeceasedFullName,
    string? Slug,
    string? Tagline,
    string? Biography,
    string? PrimaryQuote,
    string? BackgroundPhotoPath,
    string? ProfileStatusCode,
    bool IsPublic,
    int ViewCount,
    DateTime CreatedAt,
    DateTime? ModifiedAt
);

public record CreateMemorialProfileDto(
    long DeceasedId,
    string? Slug,
    string? Tagline,
    string? Biography,
    string? PrimaryQuote,
    bool IsPublic
);

public record UpdateMemorialProfileDto(
    string? Slug,
    string? Tagline,
    string? Biography,
    string? PrimaryQuote,
    bool IsPublic,
    string? ProfileStatusCode
);

// ───── Tribute ─────
public record TributeListDto(
    long TributeId,
    long DeceasedId,
    string TributeText,
    string? TributeTypeCode,
    string? AuthorName,
    string ReviewStatusCode,
    DateTime CreatedAt
);

public record CreateTributeDto(
    long DeceasedId,
    string TributeText,
    string? TributeTypeCode,
    string? AuthorName,
    string? AuthorRelation
);

// ───── Life Timeline ─────
public record LifeTimelineDto(
    long TimelineId,
    long DeceasedId,
    string EventTitle,
    string? EventDescription,
    DateOnly? EventDate,
    short? EventYear,
    string? EventTypeCode
);

public record CreateLifeTimelineDto(
    long DeceasedId,
    string EventTitle,
    string? EventDescription,
    DateOnly? EventDate,
    short? EventYear,
    string? EventTypeCode
);

// ───── Memory ─────
public record MemoryDto(
    long MemoryId,
    long DeceasedId,
    string MemoryTitle,
    string MemoryText,
    string? AuthorName,
    string ReviewStatusCode,
    DateTime CreatedAt
);

public record CreateMemoryDto(
    long DeceasedId,
    string MemoryTitle,
    string MemoryText,
    string? AuthorName
);
