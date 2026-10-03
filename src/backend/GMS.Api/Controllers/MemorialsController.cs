using System.Security.Claims;
using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMS.Api.Controllers;

[ApiController]
[Route("api/memorials")]
[Produces("application/json")]
public class MemorialsController : ControllerBase
{
    private readonly IMemorialService _svc;
    public MemorialsController(IMemorialService svc) => _svc = svc;
    private long UserId => User.Identity?.IsAuthenticated == true
        ? long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : 0L;

    // ── Memorial Profiles ─────────────────────────────────────────────────────

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        => Ok(await _svc.GetProfilesAsync(page, pageSize, search));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var r = await _svc.GetProfileByIdAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpGet("slug/{slug}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var r = await _svc.GetProfileBySlugAsync(slug);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateMemorialProfileDto dto)
    {
        var r = await _svc.CreateProfileAsync(dto, UserId);
        return r.Success ? CreatedAtAction(nameof(GetById), new { id = r.Data!.ProfileId }, r) : BadRequest(r);
    }

    [HttpPut("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateMemorialProfileDto dto)
    {
        var r = await _svc.UpdateProfileAsync(id, dto, UserId);
        return r.Success ? Ok(r) : NotFound(r);
    }

    // ── Tributes ──────────────────────────────────────────────────────────────

    [HttpGet("{deceasedId:long}/tributes")]
    public async Task<IActionResult> GetTributes(long deceasedId)
        => Ok(await _svc.GetTributesAsync(deceasedId));

    [HttpPost("{deceasedId:long}/tributes")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateTribute(long deceasedId, [FromBody] CreateTributeDto dto)
    {
        var tribdto = dto with { DeceasedId = deceasedId };
        var r = await _svc.CreateTributeAsync(tribdto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost("tributes/{id:long}/approve")]
    [Authorize]
    public async Task<IActionResult> ApproveTribute(long id)
    {
        var r = await _svc.ApproveTributeAsync(id, UserId);
        return r.Success ? Ok(r) : NotFound(r);
    }

    // ── Life Timeline ─────────────────────────────────────────────────────────

    [HttpGet("{deceasedId:long}/timeline")]
    public async Task<IActionResult> GetTimeline(long deceasedId)
        => Ok(await _svc.GetTimelineAsync(deceasedId));

    [HttpPost("{deceasedId:long}/timeline")]
    [Authorize]
    public async Task<IActionResult> AddTimeline(long deceasedId, [FromBody] CreateLifeTimelineDto dto)
    {
        var evdto = dto with { DeceasedId = deceasedId };
        var r = await _svc.AddTimelineEventAsync(evdto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    // ── Memories ──────────────────────────────────────────────────────────────

    [HttpGet("{deceasedId:long}/memories")]
    public async Task<IActionResult> GetMemories(long deceasedId)
        => Ok(await _svc.GetMemoriesAsync(deceasedId));

    [HttpPost("{deceasedId:long}/memories")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateMemory(long deceasedId, [FromBody] CreateMemoryDto dto)
    {
        var memdto = dto with { DeceasedId = deceasedId };
        var r = await _svc.CreateMemoryAsync(memdto, UserId);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost("memories/{id:long}/approve")]
    [Authorize]
    public async Task<IActionResult> ApproveMemory(long id)
    {
        var r = await _svc.ApproveMemoryAsync(id, UserId);
        return r.Success ? Ok(r) : NotFound(r);
    }
}
