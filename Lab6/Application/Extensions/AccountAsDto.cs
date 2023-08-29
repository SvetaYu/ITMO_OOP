using Application.Dto;
using DataAccess.Model;

namespace Application.Extensions;

public static class AccountAsDto
{
    public static AccountDto AsDto(this Account account)
        => new AccountDto(account.Id, account.Login, account.Password, account.Employee.Id);
}