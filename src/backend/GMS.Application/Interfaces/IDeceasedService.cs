using GMS.Application.DTOs;

namespace GMS.Application.Interfaces;

public interface IDeceasedService
{
    Task<ApiResponse<PagedResult<DeceasedListDto>>> GetAllAsync(long graveyardId, int page, int pageSize, string? search);
    Task<ApiResponse<DeceasedDetailDto>> GetByIdAsync(long id);
    Task<ApiResponse<DeceasedDetailDto>> CreateAsync(CreateDeceasedDto dto, long createdBy);
    Task<ApiResponse<DeceasedDetailDto>> UpdateAsync(long id, UpdateDeceasedDto dto, long modifiedBy);
    Task<ApiResponse<bool>> DeleteAsync(long id);
    Task<ApiResponse<bool>> VerifyAsync(long id, long verifiedBy);
    Task<ApiResponse<bool>> ApproveAsync(long id, long approvedBy);
}
