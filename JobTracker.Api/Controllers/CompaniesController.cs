using JobTracker.Core.DTOs;
using JobTracker.Core.Exceptions;
using JobTracker.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private const int TempUserId = 1; // TODO: replace with the JWT user id in Week 2
    private readonly ICompanyService _service;

    public CompaniesController(ICompanyService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<CompanyResponse>>> GetAll() =>
        Ok(await _service.GetAllAsync(TempUserId));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CompanyResponse>> GetById(int id)
    {
        var company = await _service.GetByIdAsync(TempUserId, id);
        return company is null ? NotFound() : Ok(company);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyResponse>> Create(CreateCompanyRequest request)
    {
        try
        {
            var created = await _service.CreateAsync(TempUserId, request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCompanyRequest request)
    {
        try
        {
            return await _service.UpdateAsync(TempUserId, id, request) ? NoContent() : NotFound();
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _service.DeleteAsync(TempUserId, id) ? NoContent() : NotFound();
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}