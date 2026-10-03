using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using GMS.Core.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class DeceasedService : IDeceasedService
{
    private readonly GraveyardDbContext _db;
    public DeceasedService(GraveyardDbContext db) => _db = db;

    public async Task<ApiResponse<PagedResult<DeceasedListDto>>> GetAllAsync(long graveyardId, int page, int pageSize, string? search)
    {
        var query = _db.GmsDeceasedPeople.Where(d => d.GraveyardId == graveyardId);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(d => d.FullName.Contains(search) || d.DeceasedCode.Contains(search));

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(d => d.DateOfDeath)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(d => new DeceasedListDto(
                d.DeceasedId, d.DeceasedCode, d.FullName, d.FullNameBn,
                d.GenderCode, d.DateOfBirth, d.DateOfDeath, d.Occupation,
                d.RecordStatusCode, d.IsVerified, d.IsActive))
            .ToListAsync();

        return ApiResponse<PagedResult<DeceasedListDto>>.Ok(new PagedResult<DeceasedListDto>
        { Items = items, TotalCount = total, Page = page, PageSize = pageSize });
    }

    public async Task<ApiResponse<DeceasedDetailDto>> GetByIdAsync(long id)
    {
        var d = await _db.GmsDeceasedPeople.FindAsync(id);
        if (d == null) return ApiResponse<DeceasedDetailDto>.Fail("Deceased person not found.");
        return ApiResponse<DeceasedDetailDto>.Ok(MapDetail(d));
    }

    public async Task<ApiResponse<DeceasedDetailDto>> CreateAsync(CreateDeceasedDto dto, long createdBy)
    {
        if (await _db.GmsDeceasedPeople.AnyAsync(d => d.DeceasedCode == dto.DeceasedCode))
            return ApiResponse<DeceasedDetailDto>.Fail("Deceased code already exists.");

        var entity = new GmsDeceasedPerson
        {
            GraveyardId = dto.GraveyardId,
            DeceasedCode = dto.DeceasedCode,
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            FullName = dto.FullName,
            FullNameBn = dto.FullNameBn,
            GenderCode = dto.GenderCode,
            DateOfBirth = dto.DateOfBirth,
            ApproximateBirthYear = dto.ApproximateBirthYear,
            DateOfDeath = dto.DateOfDeath,
            DeathTime = dto.DeathTime,
            PlaceOfBirth = dto.PlaceOfBirth,
            PlaceOfDeath = dto.PlaceOfDeath,
            Nationality = dto.Nationality,
            Occupation = dto.Occupation,
            ReligionCode = dto.ReligionCode,
            MaritalStatusCode = dto.MaritalStatusCode,
            FatherName = dto.FatherName,
            MotherName = dto.MotherName,
            SpouseName = dto.SpouseName,
            AddressLine1 = dto.AddressLine1,
            City = dto.City,
            District = dto.District,
            Country = dto.Country,
            AgeAtDeathYears = dto.AgeAtDeathYears,
            DeathCause = dto.DeathCause,
            DeathCertificateNo = dto.DeathCertificateNo,
            ShortIntroduction = dto.ShortIntroduction,
            RecordStatusCode = "DRAFT",
            VisibilityCode = dto.VisibilityCode ?? "PUBLIC",
            IsVerified = false,
            IsActive = true,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        _db.GmsDeceasedPeople.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<DeceasedDetailDto>.Ok(MapDetail(entity), "Deceased record created.");
    }

    public async Task<ApiResponse<DeceasedDetailDto>> UpdateAsync(long id, UpdateDeceasedDto dto, long modifiedBy)
    {
        var d = await _db.GmsDeceasedPeople.FindAsync(id);
        if (d == null) return ApiResponse<DeceasedDetailDto>.Fail("Deceased person not found.");

        d.FirstName = dto.FirstName;
        d.MiddleName = dto.MiddleName;
        d.LastName = dto.LastName;
        d.FullName = dto.FullName;
        d.FullNameBn = dto.FullNameBn;
        d.GenderCode = dto.GenderCode;
        d.DateOfBirth = dto.DateOfBirth;
        d.DateOfDeath = dto.DateOfDeath;
        d.DeathTime = dto.DeathTime;
        d.PlaceOfDeath = dto.PlaceOfDeath;
        d.Nationality = dto.Nationality;
        d.Occupation = dto.Occupation;
        d.ReligionCode = dto.ReligionCode;
        d.MaritalStatusCode = dto.MaritalStatusCode;
        d.FatherName = dto.FatherName;
        d.MotherName = dto.MotherName;
        d.SpouseName = dto.SpouseName;
        d.AddressLine1 = dto.AddressLine1;
        d.City = dto.City;
        d.District = dto.District;
        d.AgeAtDeathYears = dto.AgeAtDeathYears;
        d.DeathCause = dto.DeathCause;
        d.DeathCertificateNo = dto.DeathCertificateNo;
        d.ShortIntroduction = dto.ShortIntroduction;
        d.VisibilityCode = dto.VisibilityCode ?? d.VisibilityCode;
        d.RecordStatusCode = dto.RecordStatusCode;
        d.IsActive = dto.IsActive;
        d.ModifiedBy = modifiedBy;
        d.ModifiedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return ApiResponse<DeceasedDetailDto>.Ok(MapDetail(d), "Updated successfully.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(long id)
    {
        var d = await _db.GmsDeceasedPeople.FindAsync(id);
        if (d == null) return ApiResponse<bool>.Fail("Deceased person not found.");
        d.IsActive = false;
        d.ModifiedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }

    public async Task<ApiResponse<bool>> VerifyAsync(long id, long verifiedBy)
    {
        var d = await _db.GmsDeceasedPeople.FindAsync(id);
        if (d == null) return ApiResponse<bool>.Fail("Deceased person not found.");
        d.IsVerified = true;
        d.VerifiedBy = verifiedBy;
        d.VerifiedAt = DateTime.UtcNow;
        d.ModifiedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Verified.");
    }

    public async Task<ApiResponse<bool>> ApproveAsync(long id, long approvedBy)
    {
        var d = await _db.GmsDeceasedPeople.FindAsync(id);
        if (d == null) return ApiResponse<bool>.Fail("Deceased person not found.");
        d.ApprovedBy = approvedBy;
        d.ApprovedAt = DateTime.UtcNow;
        d.RecordStatusCode = "APPROVED";
        d.ModifiedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Approved.");
    }

    private static DeceasedDetailDto MapDetail(GmsDeceasedPerson d) => new(
        d.DeceasedId, d.GraveyardId, d.DeceasedCode, d.FirstName, d.MiddleName, d.LastName,
        d.FullName, d.FullNameBn, d.GenderCode, d.DateOfBirth, d.ApproximateBirthYear,
        d.DateOfDeath, d.DeathTime, d.PlaceOfBirth, d.PlaceOfDeath, d.Nationality,
        d.Occupation, d.ReligionCode, d.MaritalStatusCode, d.FatherName, d.MotherName,
        d.SpouseName, d.AddressLine1, d.City, d.District, d.Country, d.AgeAtDeathYears,
        d.DeathCause, d.DeathCertificateNo, d.PrimaryPhotoPath, d.ShortIntroduction,
        d.RecordStatusCode, d.VisibilityCode, d.IsVerified, d.IsActive, d.CreatedAt);
}
