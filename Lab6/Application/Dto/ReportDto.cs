namespace Application.Dto;

public record ReportDto(Guid Id, Guid ManagerId, string Path, DateOnly Date);