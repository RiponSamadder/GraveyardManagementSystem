namespace GMS.Application.DTOs;

// ───── Graveyard ─────
public record GraveyardListDto(
    long GraveyardId,
    string GraveyardCode,
    string GraveyardName,
    string? GraveyardNameBn,
    string? City,
    string? District,
    string? ContactPhone,
    bool IsActive,
    int TotalGraves
);

public record GraveyardDetailDto(
    long GraveyardId,
    long OrganizationId,
    string GraveyardCode,
    string GraveyardName,
    string? GraveyardNameBn,
    string? AddressLine1,
    string? AddressLine2,
    string? Area,
    string? City,
    string? District,
    string? PostalCode,
    string? Country,
    string? ContactPhone,
    string? ContactEmail,
    string? Website,
    DateOnly? EstablishmentDate,
    decimal? Latitude,
    decimal? Longitude,
    string? Description,
    string? VisitingHours,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? ModifiedAt
);

public record CreateGraveyardDto(
    long OrganizationId,
    string GraveyardCode,
    string GraveyardName,
    string? GraveyardNameBn,
    string? AddressLine1,
    string? AddressLine2,
    string? Area,
    string? City,
    string? District,
    string? PostalCode,
    string? Country,
    string? ContactPhone,
    string? ContactEmail,
    string? Website,
    DateOnly? EstablishmentDate,
    decimal? Latitude,
    decimal? Longitude,
    string? Description,
    string? VisitingHours
);

public record UpdateGraveyardDto(
    string GraveyardName,
    string? GraveyardNameBn,
    string? AddressLine1,
    string? AddressLine2,
    string? Area,
    string? City,
    string? District,
    string? PostalCode,
    string? Country,
    string? ContactPhone,
    string? ContactEmail,
    string? Website,
    DateOnly? EstablishmentDate,
    decimal? Latitude,
    decimal? Longitude,
    string? Description,
    string? VisitingHours,
    bool IsActive
);

// ───── Section ─────
public record SectionDto(
    long SectionId,
    long GraveyardId,
    string SectionCode,
    string SectionName,
    string? Description,
    bool IsActive
);

public record CreateSectionDto(
    string SectionCode,
    string SectionName,
    string? Description
);

// ───── Block ─────
public record BlockDto(
    long BlockId,
    long SectionId,
    string BlockCode,
    string BlockName,
    bool IsActive
);

public record CreateBlockDto(
    long SectionId,
    string BlockCode,
    string BlockName
);
