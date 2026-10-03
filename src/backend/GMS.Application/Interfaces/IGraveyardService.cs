using GMS.Application.DTOs;

namespace GMS.Application.Interfaces;

public interface IGraveyardService
{
    Task<ApiResponse<PagedResult<GraveyardListDto>>> GetAllAsync(int page, int pageSize, string? search);
    Task<ApiResponse<GraveyardDetailDto>> GetByIdAsync(long id);
    Task<ApiResponse<GraveyardDetailDto>> CreateAsync(CreateGraveyardDto dto, long createdBy);
    Task<ApiResponse<GraveyardDetailDto>> UpdateAsync(long id, UpdateGraveyardDto dto, long modifiedBy);
    Task<ApiResponse<bool>> DeleteAsync(long id);
    Task<ApiResponse<List<SectionDto>>> GetSectionsAsync(long graveyardId);
    Task<ApiResponse<SectionDto>> CreateSectionAsync(long graveyardId, CreateSectionDto dto, long createdBy);
    Task<ApiResponse<List<BlockDto>>> GetBlocksAsync(long sectionId);
    Task<ApiResponse<BlockDto>> CreateBlockAsync(CreateBlockDto dto, long createdBy);
}
