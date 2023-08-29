using Application.Dto;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Model;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly DatabaseContext _context;

    public EmployeeService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<EmployeeDto> CreateEmployeeAsync(string name, Guid id)
    {
        var employee = new Employee(name, id);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee.AsDto();
    }
    public async Task<ManagerDto> CreateManagerAsync(string name, Guid id, IEnumerable<Guid> subordinatesId)
    {
        var subordinates = new List<Employee>();
        foreach (Guid sId in subordinatesId)
        {
            subordinates.Add( await _context.Employees.GetEntityAsync(sId, default));
        }

        var manager = new Manager(name, id, subordinates);

        _context.Employees.Add(manager);
        await _context.SaveChangesAsync();
        return manager.AsDto();
    }
}