using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using GMS.Core.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class MemorialService : IMemorialService
{
    private readonly GraveyardDbContext _db;
    public MemorialService(GraveyardDbContext db) => _db = db;

    // ── Memorial Profiles ─────────────────────────────────────────────────────

    public async Task<ApiResponse<PagedResult<MemorialProfileListDto>>> GetProfilesAsync(int page, int pageSize, string? search)
    {
        var query = _db.MemMemorialProfiles.Include(p => p.Deceased).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Deceased.FullName.Contains(search) || p.MemorialSlug.Contains(search));

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(p => new MemorialProfileListDto(
                p.MemorialProfileId, p.DeceasedId, p.Deceased.FullName,
                p.MemorialSlug, p.StatusCode, p.VisibilityCode == "PUBLIC", p.CreatedAt))
            .ToListAsync();

        return ApiResponse<PagedResult<MemorialProfileListDto>>.Ok(new PagedResult<MemorialProfileListDto>
        { Items = items, TotalCount = total, Page = page, PageSize = pageSize });
    }

    public async Task<ApiResponse<MemorialProfileDetailDto>> GetProfileByIdAsync(long id)
    {
        var p = await _db.MemMemorialProfiles
            .Include(x => x.Deceased)
            .FirstOrDefaultAsync(x => x.MemorialProfileId == id);
        if (p == null) return ApiResponse<MemorialProfileDetailDto>.Fail("Memorial profile not found.");
        return ApiResponse<MemorialProfileDetailDto>.Ok(MapProfile(p));
    }

    public async Task<ApiResponse<MemorialProfileDetailDto>> GetProfileBySlugAsync(string slug)
    {
        var p = await _db.MemMemorialProfiles
            .Include(x => x.Deceased)
            .FirstOrDefaultAsync(x => x.MemorialSlug == slug);
        if (p == null) return ApiResponse<MemorialProfileDetailDto>.Fail("Memorial profile not found.");
        return ApiResponse<MemorialProfileDetailDto>.Ok(MapProfile(p));
    }

    public async Task<ApiResponse<MemorialProfileDetailDto>> CreateProfileAsync(CreateMemorialProfileDto dto, long createdBy)
    {
        if (await _db.MemMemorialProfiles.AnyAsync(p => p.MemorialSlug == dto.Slug))
            return ApiResponse<MemorialProfileDetailDto>.Fail("Slug already exists.");

        var entity = new MemMemorialProfile
        {
            DeceasedId = dto.DeceasedId,
            MemorialSlug = dto.Slug ?? $"memorial-{dto.DeceasedId}-{DateTime.UtcNow.Ticks}",
            MemorialTitle = dto.Tagline,
            ShortBiography = dto.Tagline,
            FullBiography = dto.Biography,
            FinalMessage = dto.PrimaryQuote,
            VisibilityCode = dto.IsPublic ? "PUBLIC" : "PRIVATE",
            StatusCode = "DRAFT",
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        _db.MemMemorialProfiles.Add(entity);
        await _db.SaveChangesAsync();
        return await GetProfileByIdAsync(entity.MemorialProfileId);
    }

    public async Task<ApiResponse<MemorialProfileDetailDto>> UpdateProfileAsync(long id, UpdateMemorialProfileDto dto, long modifiedBy)
    {
        var p = await _db.MemMemorialProfiles.FindAsync(id);
        if (p == null) return ApiResponse<MemorialProfileDetailDto>.Fail("Memorial profile not found.");

        if (!string.IsNullOrEmpty(dto.Slug)) p.MemorialSlug = dto.Slug;
        p.MemorialTitle = dto.Tagline;
        p.ShortBiography = dto.Tagline;
        p.FullBiography = dto.Biography;
        p.FinalMessage = dto.PrimaryQuote;
        p.VisibilityCode = dto.IsPublic ? "PUBLIC" : "PRIVATE";
        if (!string.IsNullOrEmpty(dto.ProfileStatusCode)) p.StatusCode = dto.ProfileStatusCode;
        p.ModifiedBy = modifiedBy;
        p.ModifiedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return await GetProfileByIdAsync(id);
    }

    // ── Tributes ──────────────────────────────────────────────────────────────

    public async Task<ApiResponse<List<TributeListDto>>> GetTributesAsync(long deceasedId)
    {
        var tributes = await _db.MemTributes
            .Where(t => t.DeceasedId == deceasedId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TributeListDto(
                t.TributeId, t.DeceasedId, t.TributeText,
                null, t.AuthorName, t.StatusCode, t.CreatedAt))
            .ToListAsync();
        return ApiResponse<List<TributeListDto>>.Ok(tributes);
    }

    public async Task<ApiResponse<TributeListDto>> CreateTributeAsync(CreateTributeDto dto, long createdBy)
    {
        var entity = new MemTribute
        {
            DeceasedId = dto.DeceasedId,
            TributeText = dto.TributeText,
            AuthorName = dto.AuthorName ?? "Anonymous",
            UserId = createdBy,
            StatusCode = "PENDING",
            IsPublic = false,
            IsAnonymous = string.IsNullOrEmpty(dto.AuthorName),
            CreatedAt = DateTime.UtcNow
        };
        _db.MemTributes.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<TributeListDto>.Ok(new TributeListDto(
            entity.TributeId, entity.DeceasedId, entity.TributeText,
            null, entity.AuthorName, entity.StatusCode, entity.CreatedAt));
    }

    public async Task<ApiResponse<bool>> ApproveTributeAsync(long id, long reviewedBy)
    {
        var t = await _db.MemTributes.FindAsync(id);
        if (t == null) return ApiResponse<bool>.Fail("Tribute not found.");
        t.StatusCode = "APPROVED";
        t.IsPublic = true;
        t.ReviewedBy = reviewedBy;
        t.ReviewedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Tribute approved.");
    }

    // ── Life Timeline ─────────────────────────────────────────────────────────

    public async Task<ApiResponse<List<LifeTimelineDto>>> GetTimelineAsync(long deceasedId)
    {
        var events = await _db.MemLifeTimelines
            .Where(t => t.DeceasedId == deceasedId)
            .OrderBy(t => t.EventDate).ThenBy(t => t.ApproximateYear)
            .Select(t => new LifeTimelineDto(
                t.TimelineId, t.DeceasedId, t.EventTitle, t.EventDescription,
                t.EventDate, t.ApproximateYear, null))
            .ToListAsync();
        return ApiResponse<List<LifeTimelineDto>>.Ok(events);
    }

    public async Task<ApiResponse<LifeTimelineDto>> AddTimelineEventAsync(CreateLifeTimelineDto dto, long createdBy)
    {
        var entity = new MemLifeTimeline
        {
            DeceasedId = dto.DeceasedId,
            EventTitle = dto.EventTitle,
            EventDescription = dto.EventDescription,
            EventDate = dto.EventDate,
            ApproximateYear = dto.EventYear,
            StatusCode = "PUBLISHED",
            IsPublic = true,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };
        _db.MemLifeTimelines.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<LifeTimelineDto>.Ok(new LifeTimelineDto(
            entity.TimelineId, entity.DeceasedId, entity.EventTitle,
            entity.EventDescription, entity.EventDate, entity.ApproximateYear, null));
    }

    // ── Memories ──────────────────────────────────────────────────────────────

    public async Task<ApiResponse<List<MemoryDto>>> GetMemoriesAsync(long deceasedId)
    {
        var memories = await _db.MemMemories
            .Where(m => m.DeceasedId == deceasedId)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new MemoryDto(
                m.MemoryId, m.DeceasedId, m.Title ?? "Memory", m.MemoryText,
                m.AuthorName, m.StatusCode, m.CreatedAt))
            .ToListAsync();
        return ApiResponse<List<MemoryDto>>.Ok(memories);
    }

    public async Task<ApiResponse<MemoryDto>> CreateMemoryAsync(CreateMemoryDto dto, long createdBy)
    {
        var entity = new MemMemory
        {
            DeceasedId = dto.DeceasedId,
            Title = dto.MemoryTitle,
            MemoryText = dto.MemoryText,
            AuthorName = dto.AuthorName ?? "Anonymous",
            UserId = createdBy,
            StatusCode = "PENDING",
            IsPublic = false,
            IsAnonymous = string.IsNullOrEmpty(dto.AuthorName),
            CreatedAt = DateTime.UtcNow
        };
        _db.MemMemories.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<MemoryDto>.Ok(new MemoryDto(
            entity.MemoryId, entity.DeceasedId, entity.Title!, entity.MemoryText,
            entity.AuthorName, entity.StatusCode, entity.CreatedAt));
    }

    public async Task<ApiResponse<bool>> ApproveMemoryAsync(long id, long reviewedBy)
    {
        var m = await _db.MemMemories.FindAsync(id);
        if (m == null) return ApiResponse<bool>.Fail("Memory not found.");
        m.StatusCode = "APPROVED";
        m.IsPublic = true;
        m.ReviewedBy = reviewedBy;
        m.ReviewedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Memory approved.");
    }

    // ── Mapper ────────────────────────────────────────────────────────────────

    private static MemorialProfileDetailDto MapProfile(MemMemorialProfile p) => new(
        p.MemorialProfileId, p.DeceasedId, p.Deceased.FullName,
        p.MemorialSlug, p.MemorialTitle, p.FullBiography, p.FinalMessage,
        null, p.StatusCode, p.VisibilityCode == "PUBLIC", 0, p.CreatedAt, p.ModifiedAt);
}
