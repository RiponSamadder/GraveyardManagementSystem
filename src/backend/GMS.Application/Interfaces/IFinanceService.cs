using GMS.Application.DTOs;

namespace GMS.Application.Interfaces;

public interface IFinanceService
{
    // Donations
    Task<ApiResponse<PagedResult<DonationListDto>>> GetDonationsAsync(long graveyardId, int page, int pageSize);
    Task<ApiResponse<DonationDetailDto>> GetDonationByIdAsync(long id);
    Task<ApiResponse<DonationDetailDto>> CreateDonationAsync(CreateDonationDto dto, long createdBy);

    // Expenses
    Task<ApiResponse<PagedResult<ExpenseListDto>>> GetExpensesAsync(long graveyardId, int page, int pageSize);
    Task<ApiResponse<ExpenseListDto>> GetExpenseByIdAsync(long id);
    Task<ApiResponse<ExpenseListDto>> CreateExpenseAsync(CreateExpenseDto dto, long createdBy);
    Task<ApiResponse<bool>> ApproveExpenseAsync(long id, long approvedBy);

    // Funds
    Task<ApiResponse<List<FundDto>>> GetFundsAsync(long graveyardId);
    Task<ApiResponse<FundDto>> CreateFundAsync(CreateFundDto dto, long createdBy);
}

public interface IDashboardService
{
    Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync(long? graveyardId);
}
