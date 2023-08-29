using Application.Dto;

namespace Application.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDto> CreateEmployeeAsync(string name, Guid id);
    Task<ManagerDto> CreateManagerAsync(string name, Guid id, IEnumerable<Guid> subordinatesId);
}