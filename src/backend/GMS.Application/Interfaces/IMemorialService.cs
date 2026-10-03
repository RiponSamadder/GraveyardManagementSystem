using GMS.Application.DTOs;

namespace GMS.Application.Interfaces;

public interface IMemorialService
{
    // Memorial Profiles
    Task<ApiResponse<PagedResult<MemorialProfileListDto>>> GetProfilesAsync(int page, int pageSize, string? search);
    Task<ApiResponse<MemorialProfileDetailDto>> GetProfileByIdAsync(long id);
    Task<ApiResponse<MemorialProfileDetailDto>> GetProfileBySlugAsync(string slug);
    Task<ApiResponse<MemorialProfileDetailDto>> CreateProfileAsync(CreateMemorialProfileDto dto, long createdBy);
    Task<ApiResponse<MemorialProfileDetailDto>> UpdateProfileAsync(long id, UpdateMemorialProfileDto dto, long modifiedBy);

    // Tributes
    Task<ApiResponse<List<TributeListDto>>> GetTributesAsync(long deceasedId);
    Task<ApiResponse<TributeListDto>> CreateTributeAsync(CreateTributeDto dto, long createdBy);
    Task<ApiResponse<bool>> ApproveTributeAsync(long id, long reviewedBy);

    // Life Timeline
    Task<ApiResponse<List<LifeTimelineDto>>> GetTimelineAsync(long deceasedId);
    Task<ApiResponse<LifeTimelineDto>> AddTimelineEventAsync(CreateLifeTimelineDto dto, long createdBy);

    // Memories
    Task<ApiResponse<List<MemoryDto>>> GetMemoriesAsync(long deceasedId);
    Task<ApiResponse<MemoryDto>> CreateMemoryAsync(CreateMemoryDto dto, long createdBy);
    Task<ApiResponse<bool>> ApproveMemoryAsync(long id, long reviewedBy);
}
