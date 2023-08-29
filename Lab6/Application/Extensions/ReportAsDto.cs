using Application.Dto;
using DataAccess.Model;

namespace Application.Extensions;

public static class ReportAsDto
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(report.Id, report.Manager.Id, report.Path, report.Date);
}