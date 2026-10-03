using System.Security.Claims;
using GMS.Application.DTOs;
using GMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMS.Api.Controllers;

[ApiController]
[Route("api/graves")]
[Authorize]
[Produces("application/json")]
public class GravesController : ControllerBase
{
    private readonly IGraveService _svc;
    public GravesController(IGraveService svc) => _svc = svc;
    private long UserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] long graveyardId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? status = null)
        => Ok(await _svc.GetAllAsync(graveyardId, page, pageSize, status));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGraveDto dto)
    {
        var r = await _svc.CreateAsync(dto, UserId);
        return r.Success ? CreatedAtAction(nameof(GetById), new { id = r.Data!.GraveId }, r) : BadRequest(r);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateGraveDto dto)
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
}

[ApiController]
[Route("api/burials")]
[Authorize]
[Produces("application/json")]
public class BurialsController : ControllerBase
{
    private readonly IBurialService _svc;
    public BurialsController(IBurialService svc) => _svc = svc;
    private long UserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] long graveyardId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(await _svc.GetAllAsync(graveyardId, page, pageSize));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r.Success ? Ok(r) : NotFound(r);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBurialDto dto)
    {
        var r = await _svc.CreateAsync(dto, UserId);
        return r.Success ? CreatedAtAction(nameof(GetById), new { id = r.Data!.BurialId }, r) : BadRequest(r);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateBurialDto dto)
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
}
