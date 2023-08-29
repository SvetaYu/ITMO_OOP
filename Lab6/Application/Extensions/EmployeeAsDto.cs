using Application.Dto;
using DataAccess.Model;

namespace Application.Extensions;

public static class EmployeeAsDto
{
    public static EmployeeDto AsDto(this Employee employee)
        => new EmployeeDto(employee.Id, employee.Name , employee.AccountId);
}