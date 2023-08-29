using Application.Dto;
using Application.Exceptions;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _context;

    public AccountService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<AccountDto> CreateAccountAsync(string login, string password, Guid employeeId)
    {
        Employee employee = await _context.Employees.GetEntityAsync(employeeId, default);
        if (await _context.Accounts.FirstOrDefaultAsync(a => a.Login.Equals(login)) is not null)
        {
            throw AccountException.AccountAlreadyExists(login);
        } 
        var account = new Account(Guid.NewGuid(), login, password, employee);
        employee.AccountId = account.Id;
        employee.Account = account;
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account.AsDto();

    } 
}