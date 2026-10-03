using GMS.Application.DTOs;

namespace GMS.Application.Interfaces;

public interface IGraveService
{
    Task<ApiResponse<PagedResult<GraveListDto>>> GetAllAsync(long graveyardId, int page, int pageSize, string? status);
    Task<ApiResponse<GraveDetailDto>> GetByIdAsync(long id);
    Task<ApiResponse<GraveDetailDto>> CreateAsync(CreateGraveDto dto, long createdBy);
    Task<ApiResponse<GraveDetailDto>> UpdateAsync(long id, UpdateGraveDto dto, long modifiedBy);
    Task<ApiResponse<bool>> DeleteAsync(long id);
}

public interface IBurialService
{
    Task<ApiResponse<PagedResult<BurialListDto>>> GetAllAsync(long graveyardId, int page, int pageSize);
    Task<ApiResponse<BurialDetailDto>> GetByIdAsync(long id);
    Task<ApiResponse<BurialDetailDto>> CreateAsync(CreateBurialDto dto, long createdBy);
    Task<ApiResponse<BurialDetailDto>> UpdateAsync(long id, UpdateBurialDto dto, long modifiedBy);
    Task<ApiResponse<bool>> DeleteAsync(long id);
}
