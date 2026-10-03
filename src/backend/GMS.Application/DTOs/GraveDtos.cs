namespace GMS.Application.DTOs;

public record GraveListDto(
    long GraveId,
    long GraveyardId,
    string GraveCode,
    string? GraveNumber,
    string? GraveTypeCode,
    string GraveStatusCode,
    string? SectionName,
    string? BlockName,
    decimal? Latitude,
    decimal? Longitude,
    bool IsActive
);

public record GraveDetailDto(
    long GraveId,
    long GraveyardId,
    long? SectionId,
    long? BlockId,
    long? RowId,
    string GraveCode,
    string? GraveNumber,
    string? GraveTypeCode,
    string GraveStatusCode,
    decimal? Latitude,
    decimal? Longitude,
    string? MapReference,
    string? Description,
    string? PhotoFilePath,
    string? QrcodeValue,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? ModifiedAt,
    BurialSummaryDto? LastBurial
);

public record BurialSummaryDto(
    long BurialId,
    string BurialReferenceNo,
    DateOnly BurialDate,
    string DeceasedFullName,
    long DeceasedId
);

public record CreateGraveDto(
    long GraveyardId,
    long? SectionId,
    long? BlockId,
    long? RowId,
    string GraveCode,
    string? GraveNumber,
    string? GraveTypeCode,
    decimal? Latitude,
    decimal? Longitude,
    string? MapReference,
    string? Description
);

public record UpdateGraveDto(
    long? SectionId,
    long? BlockId,
    long? RowId,
    string? GraveNumber,
    string? GraveTypeCode,
    string GraveStatusCode,
    decimal? Latitude,
    decimal? Longitude,
    string? MapReference,
    string? Description,
    bool IsActive
);
