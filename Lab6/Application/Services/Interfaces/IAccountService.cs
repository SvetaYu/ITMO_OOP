using Application.Dto;

namespace Application.Services.Interfaces;

public interface IAccountService
{
    Task<AccountDto> CreateAccountAsync(string login, string password, Guid EmployeeId);
}