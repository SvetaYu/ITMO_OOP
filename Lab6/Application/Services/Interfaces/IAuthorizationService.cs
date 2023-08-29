using Application.Dto;

namespace Application.Services.Interfaces;

public interface IAuthorizationService
{
    Task<EmployeeDto> Authorization(string login, string password);
}