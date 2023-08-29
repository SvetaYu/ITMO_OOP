using Application.Dto;

namespace Application.Services.Interfaces;

public interface IReportService
{
    Task<ReportDto> CreateReportInPhysicalFsAsync(Guid employeeId, string path);
    Task<ReportDto> CreateReportInMemoryAsync(Guid employeeId);
}