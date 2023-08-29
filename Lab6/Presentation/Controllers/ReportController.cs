using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _service;

    public ReportController(IReportService service)
    {
        _service = service;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<ReportDto>> CreateReportAsync([FromQuery] Guid employeeId, [FromBody] CreateReportModel model)
    {
      ReportDto report = await _service.CreateReportInPhysicalFsAsync(employeeId, model.Path);
      return Ok(report);
    }

}