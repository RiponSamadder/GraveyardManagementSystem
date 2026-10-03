using System.Security.Claims;
using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMS.Api.Controllers;

[ApiController]
[Route("api/deceased")]
[Authorize]
[Produces("application/json")]
public class DeceasedController : ControllerBase
{
    private readonly IDeceasedService _svc;
    public DeceasedController(IDeceasedService svc) => _svc = svc;
    private long UserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] long graveyardId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => Ok(await _svc.GetAllAsync(graveyardId, page, pageSize, search));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeceasedDto dto)
    {
        var r = await _svc.CreateAsync(dto, UserId);
        return r.Success ? CreatedAtAction(nameof(GetById), new { id = r.Data!.DeceasedId }, r) : BadRequest(r);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateDeceasedDto dto)
    {
        var r = await _svc.UpdateAsync(id, dto, UserId);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var r = await _svc.DeleteAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost("{id:long}/verify")]
    public async Task<IActionResult> Verify(long id)
    {
        var r = await _svc.VerifyAsync(id, UserId);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost("{id:long}/approve")]
    public async Task<IActionResult> Approve(long id)
    {
        var r = await _svc.ApproveAsync(id, UserId);
        return r.Success ? Ok(r) : NotFound(r);
    }
}
