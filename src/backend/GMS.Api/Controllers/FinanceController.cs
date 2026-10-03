using System.Security.Claims;
using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMS.Api.Controllers;

[ApiController]
[Route("api/finance")]
[Authorize]
[Produces("application/json")]
public class FinanceController : ControllerBase
{
    private readonly IFinanceService _svc;
    public FinanceController(IFinanceService svc) => _svc = svc;
    private long UserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // ── Donations ─────────────────────────────────────────────────────────────

    [HttpGet("donations")]
    public async Task<IActionResult> GetDonations([FromQuery] long graveyardId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(await _svc.GetDonationsAsync(graveyardId, page, pageSize));

    [HttpGet("donations/{id:long}")]
    public async Task<IActionResult> GetDonation(long id)
    {
        var r = await _svc.GetDonationByIdAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost("donations")]
    public async Task<IActionResult> CreateDonation([FromBody] CreateDonationDto dto)
    {
        var r = await _svc.CreateDonationAsync(dto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // ── Expenses ──────────────────────────────────────────────────────────────

    [HttpGet("expenses")]
    public async Task<IActionResult> GetExpenses([FromQuery] long graveyardId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(await _svc.GetExpensesAsync(graveyardId, page, pageSize));

    [HttpGet("expenses/{id:long}")]
    public async Task<IActionResult> GetExpense(long id)
    {
        var r = await _svc.GetExpenseByIdAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost("expenses")]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto dto)
    {
        var r = await _svc.CreateExpenseAsync(dto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost("expenses/{id:long}/approve")]
    public async Task<IActionResult> ApproveExpense(long id)
    {
        var r = await _svc.ApproveExpenseAsync(id, UserId);
        return r.Success ? Ok(r) : NotFound(r);
    }

    // ── Funds ─────────────────────────────────────────────────────────────────

    [HttpGet("funds")]
    public async Task<IActionResult> GetFunds([FromQuery] long graveyardId)
        => Ok(await _svc.GetFundsAsync(graveyardId));

    [HttpPost("funds")]
    public async Task<IActionResult> CreateFund([FromBody] CreateFundDto dto)
    {
        var r = await _svc.CreateFundAsync(dto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}

[ApiController]
[Route("api/dashboard")]
[Authorize]
[Produces("application/json")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _svc;
    public DashboardController(IDashboardService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetSummary([FromQuery] long? graveyardId = null)
        => Ok(await _svc.GetSummaryAsync(graveyardId));
}
