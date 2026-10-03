using System.Security.Claims;
using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMS.Api.Controllers;

[ApiController]
[Route("api/graveyards")]
[Authorize]
[Produces("application/json")]
public class GraveyardsController : ControllerBase
{
    private readonly IGraveyardService _svc;
    public GraveyardsController(IGraveyardService svc) => _svc = svc;

    private long UserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => Ok(await _svc.GetAllAsync(page, pageSize, search));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGraveyardDto dto)
    {
        var r = await _svc.CreateAsync(dto, UserId);
        return r.Success ? CreatedAtAction(nameof(GetById), new { id = r.Data!.GraveyardId }, r) : BadRequest(r);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateGraveyardDto dto)
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

    // ── Sections ─────────────────────────────────────────────────────────────

    [HttpGet("{id:long}/sections")]
    public async Task<IActionResult> GetSections(long id)
        => Ok(await _svc.GetSectionsAsync(id));

    [HttpPost("{id:long}/sections")]
    public async Task<IActionResult> CreateSection(long id, [FromBody] CreateSectionDto dto)
    {
        var r = await _svc.CreateSectionAsync(id, dto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("sections/{sectionId:long}/blocks")]
    public async Task<IActionResult> GetBlocks(long sectionId)
        => Ok(await _svc.GetBlocksAsync(sectionId));

    [HttpPost("blocks")]
    public async Task<IActionResult> CreateBlock([FromBody] CreateBlockDto dto)
    {
        var r = await _svc.CreateBlockAsync(dto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }
}
