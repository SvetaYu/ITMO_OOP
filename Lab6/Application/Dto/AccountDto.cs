namespace Application.Dto;

public record AccountDto(Guid Id, string Login, string Password, Guid EmployeeId);