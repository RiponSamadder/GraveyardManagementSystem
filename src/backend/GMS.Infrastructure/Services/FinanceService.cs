using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using GMS.Core.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class FinanceService : IFinanceService
{
    private readonly GraveyardDbContext _db;
    public FinanceService(GraveyardDbContext db) => _db = db;

    // ── Donations ─────────────────────────────────────────────────────────────

    public async Task<ApiResponse<PagedResult<DonationListDto>>> GetDonationsAsync(long graveyardId, int page, int pageSize)
    {
        var query = _db.FinDonations
            .Include(d => d.Donor)
            .Where(d => d.GraveyardId == graveyardId);

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(d => d.DonationDate)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(d => new DonationListDto(
                d.DonationId, d.DonationNo, d.DonationDate, d.Amount,
                d.PaymentMethodCode,
                d.Donor != null ? d.Donor.DonorName : null,
                d.IsAnonymous, d.StatusCode))
            .ToListAsync();

        return ApiResponse<PagedResult<DonationListDto>>.Ok(new PagedResult<DonationListDto>
        { Items = items, TotalCount = total, Page = page, PageSize = pageSize });
    }

    public async Task<ApiResponse<DonationDetailDto>> GetDonationByIdAsync(long id)
    {
        var d = await _db.FinDonations
            .Include(x => x.Donor)
            .FirstOrDefaultAsync(x => x.DonationId == id);
        if (d == null) return ApiResponse<DonationDetailDto>.Fail("Donation not found.");

        return ApiResponse<DonationDetailDto>.Ok(new DonationDetailDto(
            d.DonationId, d.GraveyardId, d.DonationNo, d.DonorId,
            d.Donor?.DonorName, d.FundId, d.AccountId, d.DonationDate,
            d.Amount, d.PaymentMethodCode, d.PaymentReference, d.PurposeCode,
            d.IsAnonymous, d.Remarks, d.StatusCode, d.ReceiptNo, d.CreatedAt));
    }

    public async Task<ApiResponse<DonationDetailDto>> CreateDonationAsync(CreateDonationDto dto, long createdBy)
    {
        if (await _db.FinDonations.AnyAsync(d => d.DonationNo == dto.DonationNo))
            return ApiResponse<DonationDetailDto>.Fail("Donation number already exists.");

        var entity = new FinDonation
        {
            GraveyardId = dto.GraveyardId,
            DonationNo = dto.DonationNo,
            DonorId = dto.DonorId,
            FundId = dto.FundId,
            AccountId = dto.AccountId,
            DonationDate = dto.DonationDate,
            Amount = dto.Amount,
            PaymentMethodCode = dto.PaymentMethodCode,
            PaymentReference = dto.PaymentReference,
            PurposeCode = dto.PurposeCode,
            IsAnonymous = dto.IsAnonymous,
            Remarks = dto.Remarks,
            StatusCode = "CONFIRMED",
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };

        _db.FinDonations.Add(entity);
        await _db.SaveChangesAsync();
        return await GetDonationByIdAsync(entity.DonationId);
    }

    // ── Expenses ──────────────────────────────────────────────────────────────

    public async Task<ApiResponse<PagedResult<ExpenseListDto>>> GetExpensesAsync(long graveyardId, int page, int pageSize)
    {
        var total = await _db.FinExpenses.CountAsync(e => e.GraveyardId == graveyardId);
        var items = await _db.FinExpenses
            .Where(e => e.GraveyardId == graveyardId)
            .OrderByDescending(e => e.ExpenseDate)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(e => new ExpenseListDto(
                e.ExpenseId, e.GraveyardId, e.ExpenseNo, e.ExpenseDate, e.Amount,
                e.ExpenseCategoryCode, e.Description, e.ApprovalStatusCode,
                e.ApprovalStatusCode == "APPROVED"))
            .ToListAsync();

        return ApiResponse<PagedResult<ExpenseListDto>>.Ok(new PagedResult<ExpenseListDto>
        { Items = items, TotalCount = total, Page = page, PageSize = pageSize });
    }

    public async Task<ApiResponse<ExpenseListDto>> GetExpenseByIdAsync(long id)
    {
        var e = await _db.FinExpenses.FindAsync(id);
        if (e == null) return ApiResponse<ExpenseListDto>.Fail("Expense not found.");
        return ApiResponse<ExpenseListDto>.Ok(new ExpenseListDto(
            e.ExpenseId, e.GraveyardId, e.ExpenseNo, e.ExpenseDate, e.Amount,
            e.ExpenseCategoryCode, e.Description, e.ApprovalStatusCode,
            e.ApprovalStatusCode == "APPROVED"));
    }

    public async Task<ApiResponse<ExpenseListDto>> CreateExpenseAsync(CreateExpenseDto dto, long createdBy)
    {
        var entity = new FinExpense
        {
            GraveyardId = dto.GraveyardId,
            ExpenseNo = dto.ExpenseNo,
            ExpenseDate = dto.ExpenseDate,
            Amount = dto.Amount,
            ExpenseCategoryCode = dto.ExpenseCategoryCode,
            AccountId = dto.AccountId,
            FundId = dto.FundId,
            Description = dto.Description,
            PayeeName = dto.VendorName,
            ApprovalStatusCode = "PENDING",
            PaymentStatusCode = "UNPAID",
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };
        _db.FinExpenses.Add(entity);
        await _db.SaveChangesAsync();
        return await GetExpenseByIdAsync(entity.ExpenseId);
    }

    public async Task<ApiResponse<bool>> ApproveExpenseAsync(long id, long approvedBy)
    {
        var e = await _db.FinExpenses.FindAsync(id);
        if (e == null) return ApiResponse<bool>.Fail("Expense not found.");
        e.ApprovalStatusCode = "APPROVED";
        e.ApprovedBy = approvedBy;
        e.ApprovedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Expense approved.");
    }

    // ── Funds ─────────────────────────────────────────────────────────────────

    public async Task<ApiResponse<List<FundDto>>> GetFundsAsync(long graveyardId)
    {
        var funds = await _db.FinFunds
            .Where(f => f.GraveyardId == graveyardId && f.IsActive)
            .OrderBy(f => f.FundName)
            .ToListAsync();

        var result = new List<FundDto>();
        foreach (var f in funds)
        {
            var donations = await _db.FinDonations
                .Where(d => d.FundId == f.FundId && d.StatusCode == "CONFIRMED")
                .SumAsync(d => d.Amount);
            var expenses = await _db.FinExpenses
                .Where(e => e.FundId == f.FundId && e.ApprovalStatusCode == "APPROVED")
                .SumAsync(e => e.Amount);

            result.Add(new FundDto(f.FundId, f.GraveyardId, f.FundCode, f.FundName,
                f.OpeningBalance, f.OpeningBalance + donations - expenses, f.IsActive));
        }

        return ApiResponse<List<FundDto>>.Ok(result);
    }

    public async Task<ApiResponse<FundDto>> CreateFundAsync(CreateFundDto dto, long createdBy)
    {
        var entity = new FinFund
        {
            GraveyardId = dto.GraveyardId,
            FundCode = dto.FundCode,
            FundName = dto.FundName,
            OpeningBalance = dto.OpeningBalance,
            IsActive = true,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow
        };
        _db.FinFunds.Add(entity);
        await _db.SaveChangesAsync();
        return ApiResponse<FundDto>.Ok(new FundDto(
            entity.FundId, entity.GraveyardId, entity.FundCode, entity.FundName,
            entity.OpeningBalance, entity.OpeningBalance, entity.IsActive), "Fund created.");
    }
}

public class DashboardService : IDashboardService
{
    private readonly GraveyardDbContext _db;
    public DashboardService(GraveyardDbContext db) => _db = db;

    public async Task<ApiResponse<DashboardSummaryDto>> GetSummaryAsync(long? graveyardId)
    {
        var graveyardQuery = _db.GmsGraveyards.AsQueryable();
        if (graveyardId.HasValue)
            graveyardQuery = graveyardQuery.Where(g => g.GraveyardId == graveyardId.Value);

        var totalGraveyards = await graveyardQuery.CountAsync();
        var graveQuery = _db.GmsGraves.AsQueryable();
        if (graveyardId.HasValue) graveQuery = graveQuery.Where(g => g.GraveyardId == graveyardId.Value);

        var totalGraves = await graveQuery.CountAsync();
        var occupied = await graveQuery.CountAsync(g => g.GraveStatusCode == "OCCUPIED");

        var deceasedQuery = _db.GmsDeceasedPeople.AsQueryable();
        if (graveyardId.HasValue) deceasedQuery = deceasedQuery.Where(d => d.GraveyardId == graveyardId.Value);
        var totalDeceased = await deceasedQuery.CountAsync();

        var burialQuery = _db.GmsBurials.Include(b => b.Grave).AsQueryable();
        if (graveyardId.HasValue) burialQuery = burialQuery.Where(b => b.Grave.GraveyardId == graveyardId.Value);
        var totalBurials = await burialQuery.CountAsync();
        var recentBurials = await burialQuery.CountAsync(b => b.BurialDate >= DateOnly.FromDateTime(DateTime.Today.AddDays(-30)));

        var donationQuery = _db.FinDonations.AsQueryable();
        if (graveyardId.HasValue) donationQuery = donationQuery.Where(d => d.GraveyardId == graveyardId.Value);
        var totalDonations = await donationQuery.Where(d => d.StatusCode == "CONFIRMED").SumAsync(d => d.Amount);

        var expenseQuery = _db.FinExpenses.AsQueryable();
        if (graveyardId.HasValue) expenseQuery = expenseQuery.Where(e => e.GraveyardId == graveyardId.Value);
        var totalExpenses = await expenseQuery.Where(e => e.ApprovalStatusCode == "APPROVED").SumAsync(e => e.Amount);

        var openingFunds = await _db.FinFunds
            .Where(f => !graveyardId.HasValue || f.GraveyardId == graveyardId.Value)
            .SumAsync(f => f.OpeningBalance);

        var activities = new List<RecentActivityDto>();
        var recentBurialsList = await burialQuery
            .OrderByDescending(b => b.CreatedAt)
            .Take(5)
            .Select(b => new RecentActivityDto("Burial", $"Burial: {b.BurialReferenceNo}", b.CreatedAt))
            .ToListAsync();
        activities.AddRange(recentBurialsList);

        return ApiResponse<DashboardSummaryDto>.Ok(new DashboardSummaryDto(
            totalGraveyards, totalGraves, occupied, totalGraves - occupied,
            totalDeceased, totalBurials, totalDonations, totalExpenses,
            openingFunds + totalDonations - totalExpenses,
            recentBurials, activities));
    }
}
