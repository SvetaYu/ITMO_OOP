using Application.Dto;
using Application.Exceptions;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly DatabaseContext _context;

    public AuthorizationService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<EmployeeDto> Authorization(string login, string password)
    {
        Account account = await 
            _context.Accounts.SingleOrDefaultAsync(account =>
                account.Login.Equals(login) && account.Password.Equals(password));
        if (account is null)
        {
            throw AuthorizationException.AccountNotFound();
        }

        return account.Employee.AsDto();
    }
}