namespace GMS.Application.DTOs;

// ───── Burial ─────
public record BurialListDto(
    long BurialId,
    string BurialReferenceNo,
    DateOnly BurialDate,
    string? BurialTypeCode,
    long DeceasedId,
    string DeceasedFullName,
    long GraveId,
    string GraveCode,
    bool IsPrimary,
    DateTime CreatedAt
);

public record BurialDetailDto(
    long BurialId,
    long DeceasedId,
    string DeceasedFullName,
    long GraveId,
    string GraveCode,
    string BurialReferenceNo,
    DateOnly BurialDate,
    TimeOnly? BurialTime,
    string? BurialTypeCode,
    string? BurialPerformedBy,
    string? ResponsiblePerson,
    string? Notes,
    bool IsPrimary,
    DateTime CreatedAt
);

public record CreateBurialDto(
    long DeceasedId,
    long GraveId,
    string BurialReferenceNo,
    DateOnly BurialDate,
    TimeOnly? BurialTime,
    string? BurialTypeCode,
    string? BurialPerformedBy,
    string? ResponsiblePerson,
    string? Notes,
    bool IsPrimary
);

public record UpdateBurialDto(
    DateOnly BurialDate,
    TimeOnly? BurialTime,
    string? BurialTypeCode,
    string? BurialPerformedBy,
    string? ResponsiblePerson,
    string? Notes,
    bool IsPrimary
);
