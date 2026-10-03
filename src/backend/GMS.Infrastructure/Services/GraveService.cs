using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using GMS.Core.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class GraveService : IGraveService
{
    private readonly GraveyardDbContext _db;
    public GraveService(GraveyardDbContext db) => _db = db;

    public async Task<ApiResponse<PagedResult<GraveListDto>>> GetAllAsync(long graveyardId, int page, int pageSize, string? status)
    {
        var query = _db.GmsGraves
            .Include(g => g.Section)
            .Include(g => g.Block)
            .Where(g => g.GraveyardId == graveyardId);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(g => g.GraveStatusCode == status);

        var total = await query.CountAsync();
        var items = await query
            .OrderBy(g => g.GraveCode)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(g => new GraveListDto(
                g.GraveId, g.GraveyardId, g.GraveCode, g.GraveNumber,
                g.GraveTypeCode, g.GraveStatusCode,
                g.Section != null ? g.Section.SectionName : null,
                g.Block != null ? g.Block.BlockName : null,
                g.Latitude, g.Longitude, g.IsActive))
            .ToListAsync();

        return ApiResponse<PagedResult<GraveListDto>>.Ok(new PagedResult<GraveListDto>
        { Items = items, TotalCount = total, Page = page, PageSize = pageSize });
    }

    public async Task<ApiResponse<GraveDetailDto>> GetByIdAsync(long id)
    {
        var g = await _db.GmsGraves
            .Include(x => x.GmsBurials).ThenInclude(b => b.Deceased)
            .FirstOrDefaultAsync(x => x.GraveId == id);

        if (g == null) return ApiResponse<GraveDetailDto>.Fail("Grave not found.");

        var lastBurial = g.GmsBurials
            .OrderByDescending(b => b.BurialDate)
            .FirstOrDefault();

        BurialSummaryDto? burialSummary = lastBurial == null ? null : new BurialSummaryDto(
            lastBurial.BurialId, lastBurial.BurialReferenceNo, lastBurial.BurialDate,
            lastBurial.Deceased.FullName, lastBurial.DeceasedId);

        return ApiResponse<GraveDetailDto>.Ok(new GraveDetailDto(
            g.GraveId, g.GraveyardId, g.SectionId, g.BlockId, g.RowId,
            g.GraveCode, g.GraveNumber, g.GraveTypeCode, g.GraveStatusCode,
            g.Latitude, g.Longitude, g.MapReference, g.Description,
            g.PhotoFilePath, g.QrcodeValue, g.IsActive, g.CreatedAt, g.ModifiedAt, burialSummary));
    }

    public async Task<ApiResponse<GraveDetailDto>> CreateAsync(CreateGraveDto dto, long createdBy)
    {
        if (await _db.GmsGraves.AnyAsync(g => g.GraveCode == dto.GraveCode && g.GraveyardId == dto.GraveyardId))
            return ApiResponse<GraveDetailDto>.Fail("Grave code already exists in this graveyard.");

        var entity = new GmsGrave
        {
            GraveyardId = dto.GraveyardId,
            SectionId = dto.SectionId,
            BlockId = dto.BlockId,
            RowId = dto.RowId,
            GraveCode = dto.GraveCode,
            GraveNumber = dto.GraveNumber,
            GraveTypeCode = dto.GraveTypeCode,
            GraveStatusCode = "AVAILABLE",
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            MapReference = dto.MapReference,
            Description = dto.Description,
            IsActive = true,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        _db.GmsGraves.Add(entity);
        await _db.SaveChangesAsync();
        return await GetByIdAsync(entity.GraveId);
    }

    public async Task<ApiResponse<GraveDetailDto>> UpdateAsync(long id, UpdateGraveDto dto, long modifiedBy)
    {
        var g = await _db.GmsGraves.FindAsync(id);
        if (g == null) return ApiResponse<GraveDetailDto>.Fail("Grave not found.");

        g.SectionId = dto.SectionId;
        g.BlockId = dto.BlockId;
        g.RowId = dto.RowId;
        g.GraveNumber = dto.GraveNumber;
        g.GraveTypeCode = dto.GraveTypeCode;
        g.GraveStatusCode = dto.GraveStatusCode;
        g.Latitude = dto.Latitude;
        g.Longitude = dto.Longitude;
        g.MapReference = dto.MapReference;
        g.Description = dto.Description;
        g.IsActive = dto.IsActive;
        g.ModifiedBy = modifiedBy;
        g.ModifiedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(long id)
    {
        var g = await _db.GmsGraves.FindAsync(id);
        if (g == null) return ApiResponse<bool>.Fail("Grave not found.");
        g.IsActive = false;
        g.ModifiedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Grave deactivated.");
    }
}

public class BurialService : IBurialService
{
    private readonly GraveyardDbContext _db;
    public BurialService(GraveyardDbContext db) => _db = db;

    public async Task<ApiResponse<PagedResult<BurialListDto>>> GetAllAsync(long graveyardId, int page, int pageSize)
    {
        var query = _db.GmsBurials
            .Include(b => b.Deceased)
            .Include(b => b.Grave)
            .Where(b => b.Grave.GraveyardId == graveyardId);

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(b => b.BurialDate)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(b => new BurialListDto(
                b.BurialId, b.BurialReferenceNo, b.BurialDate, b.BurialTypeCode,
                b.DeceasedId, b.Deceased.FullName, b.GraveId, b.Grave.GraveCode,
                b.IsPrimary, b.CreatedAt))
            .ToListAsync();

        return ApiResponse<PagedResult<BurialListDto>>.Ok(new PagedResult<BurialListDto>
        { Items = items, TotalCount = total, Page = page, PageSize = pageSize });
    }

    public async Task<ApiResponse<BurialDetailDto>> GetByIdAsync(long id)
    {
        var b = await _db.GmsBurials
            .Include(x => x.Deceased)
            .Include(x => x.Grave)
            .FirstOrDefaultAsync(x => x.BurialId == id);

        if (b == null) return ApiResponse<BurialDetailDto>.Fail("Burial not found.");

        return ApiResponse<BurialDetailDto>.Ok(new BurialDetailDto(
            b.BurialId, b.DeceasedId, b.Deceased.FullName, b.GraveId, b.Grave.GraveCode,
            b.BurialReferenceNo, b.BurialDate, b.BurialTime, b.BurialTypeCode,
            b.BurialPerformedBy, b.ResponsiblePerson, b.Notes, b.IsPrimary, b.CreatedAt));
    }

    public async Task<ApiResponse<BurialDetailDto>> CreateAsync(CreateBurialDto dto, long createdBy)
    {
        if (await _db.GmsBurials.AnyAsync(b => b.BurialReferenceNo == dto.BurialReferenceNo))
            return ApiResponse<BurialDetailDto>.Fail("Burial reference number already exists.");

        var entity = new GmsBurial
        {
            DeceasedId = dto.DeceasedId,
            GraveId = dto.GraveId,
            BurialReferenceNo = dto.BurialReferenceNo,
            BurialDate = dto.BurialDate,
            BurialTime = dto.BurialTime,
            BurialTypeCode = dto.BurialTypeCode,
            BurialPerformedBy = dto.BurialPerformedBy,
            ResponsiblePerson = dto.ResponsiblePerson,
            Notes = dto.Notes,
            IsPrimary = dto.IsPrimary,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };
        _db.GmsBurials.Add(entity);

        // Mark grave as OCCUPIED
        var grave = await _db.GmsGraves.FindAsync(dto.GraveId);
        if (grave != null)
        {
            grave.GraveStatusCode = "OCCUPIED";
            grave.ModifiedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
        return await GetByIdAsync(entity.BurialId);
    }

    public async Task<ApiResponse<BurialDetailDto>> UpdateAsync(long id, UpdateBurialDto dto, long modifiedBy)
    {
        var b = await _db.GmsBurials.FindAsync(id);
        if (b == null) return ApiResponse<BurialDetailDto>.Fail("Burial not found.");

        b.BurialDate = dto.BurialDate;
        b.BurialTime = dto.BurialTime;
        b.BurialTypeCode = dto.BurialTypeCode;
        b.BurialPerformedBy = dto.BurialPerformedBy;
        b.ResponsiblePerson = dto.ResponsiblePerson;
        b.Notes = dto.Notes;
        b.IsPrimary = dto.IsPrimary;
        b.ModifiedBy = modifiedBy;
        b.ModifiedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(long id)
    {
        var b = await _db.GmsBurials.FindAsync(id);
        if (b == null) return ApiResponse<bool>.Fail("Burial not found.");
        _db.GmsBurials.Remove(b);
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Burial deleted.");
    }
}
