namespace GMS.Application.DTOs;

// ───── Donation ─────
public record DonationListDto(
    long DonationId,
    string DonationNo,
    DateOnly DonationDate,
    decimal Amount,
    string PaymentMethodCode,
    string? DonorName,
    bool IsAnonymous,
    string StatusCode
);

public record DonationDetailDto(
    long DonationId,
    long GraveyardId,
    string DonationNo,
    long? DonorId,
    string? DonorName,
    long FundId,
    long? AccountId,
    DateOnly DonationDate,
    decimal Amount,
    string PaymentMethodCode,
    string? PaymentReference,
    string? PurposeCode,
    bool IsAnonymous,
    string? Remarks,
    string StatusCode,
    string? ReceiptNo,
    DateTime CreatedAt
);

public record CreateDonationDto(
    long GraveyardId,
    string DonationNo,
    long? DonorId,
    long FundId,
    long? AccountId,
    DateOnly DonationDate,
    decimal Amount,
    string PaymentMethodCode,
    string? PaymentReference,
    string? PurposeCode,
    bool IsAnonymous,
    string? Remarks
);

// ───── Expense ─────
public record ExpenseListDto(
    long ExpenseId,
    long GraveyardId,
    string ExpenseNo,
    DateOnly ExpenseDate,
    decimal Amount,
    string ExpenseCategoryCode,
    string? Description,
    string StatusCode,
    bool IsApproved
);

public record CreateExpenseDto(
    long GraveyardId,
    string ExpenseNo,
    DateOnly ExpenseDate,
    decimal Amount,
    string ExpenseCategoryCode,
    long? AccountId,
    long FundId,
    string? Description,
    string? VendorName,
    string? Remarks
);

// ───── Fund ─────
public record FundDto(
    long FundId,
    long GraveyardId,
    string FundCode,
    string FundName,
    decimal OpeningBalance,
    decimal CurrentBalance,
    bool IsActive
);

public record CreateFundDto(
    long GraveyardId,
    string FundCode,
    string FundName,
    decimal OpeningBalance
);

// ───── Dashboard ─────
public record DashboardSummaryDto(
    int TotalGraveyards,
    int TotalGraves,
    int OccupiedGraves,
    int AvailableGraves,
    int TotalDeceased,
    int TotalBurials,
    decimal TotalDonations,
    decimal TotalExpenses,
    decimal CurrentFundBalance,
    int RecentBurials,
    List<RecentActivityDto> RecentActivities
);

public record RecentActivityDto(
    string Type,
    string Description,
    DateTime ActivityDate
);
