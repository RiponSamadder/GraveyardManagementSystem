using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using GMS.Core.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class GraveyardService : IGraveyardService
{
    private readonly GraveyardDbContext _db;

    public GraveyardService(GraveyardDbContext db) => _db = db;

    public async Task<ApiResponse<PagedResult<GraveyardListDto>>> GetAllAsync(int page, int pageSize, string? search)
    {
        var query = _db.GmsGraveyards.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(g => g.GraveyardName.Contains(search) ||
                                     g.GraveyardCode.Contains(search) ||
                                     (g.City != null && g.City.Contains(search)));

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(g => g.GraveyardName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(g => new GraveyardListDto(
                g.GraveyardId, g.GraveyardCode, g.GraveyardName, g.GraveyardNameBn,
                g.City, g.District, g.ContactPhone, g.IsActive,
                g.GmsGraves.Count))
            .ToListAsync();

        return ApiResponse<PagedResult<GraveyardListDto>>.Ok(new PagedResult<GraveyardListDto>
        {
            Items = items, TotalCount = total, Page = page, PageSize = pageSize
        });
    }

    public async Task<ApiResponse<GraveyardDetailDto>> GetByIdAsync(long id)
    {
        var g = await _db.GmsGraveyards.FindAsync(id);
        if (g == null) return ApiResponse<GraveyardDetailDto>.Fail("Graveyard not found.");
        return ApiResponse<GraveyardDetailDto>.Ok(MapDetail(g));
    }

    public async Task<ApiResponse<GraveyardDetailDto>> CreateAsync(CreateGraveyardDto dto, long createdBy)
    {
        if (await _db.GmsGraveyards.AnyAsync(g => g.GraveyardCode == dto.GraveyardCode))
            return ApiResponse<GraveyardDetailDto>.Fail("Graveyard code already exists.");

        var entity = new GmsGraveyard
        {
            OrganizationId = dto.OrganizationId,
            GraveyardCode = dto.GraveyardCode,
            GraveyardName = dto.GraveyardName,
            GraveyardNameBn = dto.GraveyardNameBn,
            AddressLine1 = dto.AddressLine1,
            AddressLine2 = dto.AddressLine2,
            Area = dto.Area,
            City = dto.City,
            District = dto.District,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            ContactPhone = dto.ContactPhone,
            ContactEmail = dto.ContactEmail,
            Website = dto.Website,
            EstablishmentDate = dto.EstablishmentDate,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Description = dto.Description,
            VisitingHours = dto.VisitingHours,
            IsActive = true,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        _db.GmsGraveyards.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<GraveyardDetailDto>.Ok(MapDetail(entity), "Graveyard created successfully.");
    }

    public async Task<ApiResponse<GraveyardDetailDto>> UpdateAsync(long id, UpdateGraveyardDto dto, long modifiedBy)
    {
        var g = await _db.GmsGraveyards.FindAsync(id);
        if (g == null) return ApiResponse<GraveyardDetailDto>.Fail("Graveyard not found.");

        g.GraveyardName = dto.GraveyardName;
        g.GraveyardNameBn = dto.GraveyardNameBn;
        g.AddressLine1 = dto.AddressLine1;
        g.AddressLine2 = dto.AddressLine2;
        g.Area = dto.Area;
        g.City = dto.City;
        g.District = dto.District;
        g.PostalCode = dto.PostalCode;
        g.Country = dto.Country;
        g.ContactPhone = dto.ContactPhone;
        g.ContactEmail = dto.ContactEmail;
        g.Website = dto.Website;
        g.EstablishmentDate = dto.EstablishmentDate;
        g.Latitude = dto.Latitude;
        g.Longitude = dto.Longitude;
        g.Description = dto.Description;
        g.VisitingHours = dto.VisitingHours;
        g.IsActive = dto.IsActive;
        g.ModifiedBy = modifiedBy;
        g.ModifiedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return ApiResponse<GraveyardDetailDto>.Ok(MapDetail(g), "Graveyard updated successfully.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(long id)
    {
        var g = await _db.GmsGraveyards.FindAsync(id);
        if (g == null) return ApiResponse<bool>.Fail("Graveyard not found.");

        g.IsActive = false;
        g.ModifiedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Graveyard deactivated.");
    }

    // ── Sections ─────────────────────────────────────────────────────────────

    public async Task<ApiResponse<List<SectionDto>>> GetSectionsAsync(long graveyardId)
    {
        var sections = await _db.GmsGraveyardSections
            .Where(s => s.GraveyardId == graveyardId)
            .OrderBy(s => s.SectionCode)
            .Select(s => new SectionDto(s.SectionId, s.GraveyardId, s.SectionCode,
                                        s.SectionName, s.Description, s.IsActive))
            .ToListAsync();

        return ApiResponse<List<SectionDto>>.Ok(sections);
    }

    public async Task<ApiResponse<SectionDto>> CreateSectionAsync(long graveyardId, CreateSectionDto dto, long createdBy)
    {
        var entity = new GmsGraveyardSection
        {
            GraveyardId = graveyardId,
            SectionCode = dto.SectionCode,
            SectionName = dto.SectionName,
            Description = dto.Description,
            IsActive = true,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };
        _db.GmsGraveyardSections.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<SectionDto>.Ok(
            new SectionDto(entity.SectionId, entity.GraveyardId, entity.SectionCode,
                           entity.SectionName, entity.Description, entity.IsActive),
            "Section created.");
    }

    // ── Blocks ────────────────────────────────────────────────────────────────

    public async Task<ApiResponse<List<BlockDto>>> GetBlocksAsync(long sectionId)
    {
        var blocks = await _db.GmsGraveBlocks
            .Where(b => b.SectionId == sectionId)
            .OrderBy(b => b.BlockCode)
            .Select(b => new BlockDto(b.BlockId, b.SectionId, b.BlockCode, b.BlockName, b.IsActive))
            .ToListAsync();

        return ApiResponse<List<BlockDto>>.Ok(blocks);
    }

    public async Task<ApiResponse<BlockDto>> CreateBlockAsync(CreateBlockDto dto, long createdBy)
    {
        var entity = new GmsGraveBlock
        {
            SectionId = dto.SectionId,
            BlockCode = dto.BlockCode,
            BlockName = dto.BlockName,
            IsActive = true
        };
        _db.GmsGraveBlocks.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<BlockDto>.Ok(
            new BlockDto(entity.BlockId, entity.SectionId, entity.BlockCode, entity.BlockName, entity.IsActive),
            "Block created.");
    }

    // ── Mapper ───────────────────────────────────────────────────────────────

    private static GraveyardDetailDto MapDetail(GmsGraveyard g) => new(
        g.GraveyardId, g.OrganizationId, g.GraveyardCode, g.GraveyardName, g.GraveyardNameBn,
        g.AddressLine1, g.AddressLine2, g.Area, g.City, g.District, g.PostalCode, g.Country,
        g.ContactPhone, g.ContactEmail, g.Website, g.EstablishmentDate,
        g.Latitude, g.Longitude, g.Description, g.VisitingHours,
        g.IsActive, g.CreatedAt, g.ModifiedAt);
}
