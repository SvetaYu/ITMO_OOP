using System;
using System.Threading.Tasks;
using Application.Dto;
using Application.Extensions;
using Xunit;
using Application.Services;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests;

public class UnitTest1 : IDisposable
{
    private readonly DatabaseContext _context;
    private IEmployeeService _employeeService;
    private IAccountService _accountService;
    private IMessageSourceService _messageSourceService;
    private IMessageService _messageService;
    private IAuthorizationService _authorizationService;
    private IReportService _reportService;

    public UnitTest1()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("inMemory")
            .Options;
        _context = new DatabaseContext(options);
        _employeeService = new EmployeeService(_context);
        _accountService = new AccountService(_context);
        _messageSourceService = new MessageSourceService(_context);
        _messageService = new MessageService(_context);
        _authorizationService = new AuthorizationService(_context);
        _reportService = new ReportService(_context);
    }

    [Fact]
    public async Task<EmployeeDto> CreateEmployee()
    {
        var id = Guid.NewGuid();
        EmployeeDto employee = await _employeeService.CreateEmployeeAsync("Name", id);
        Assert.Single(_context.Employees, employee => employee.Id.Equals(id));
        return employee;
    }

    [Theory]
    [InlineData("login", "password")]
    public async Task<AccountDto> CreateEmployeesAccountAsync(string login, string password)
    {
        EmployeeDto employeeDto = await CreateEmployee();
        AccountDto employeeAccountDto = await _accountService.CreateAccountAsync(login, password, employeeDto.Id);
        Assert.Single(_context.Accounts, account => account.Id.Equals(employeeAccountDto.Id));
        Assert.Single(_context.Accounts, account => account.Login.Equals(employeeAccountDto.Login));
        Employee employee = await _context.Employees.GetEntityAsync(employeeDto.Id, default);
        Account account = await _context.Accounts.GetEntityAsync(employeeAccountDto.Id, default);
        Assert.Equal(account, employee.Account);
        return employeeAccountDto;
    }

    [Fact]
    public async Task<ManagerDto> CreateManager()
    {
        var id = Guid.NewGuid();
        EmployeeDto employeeDto = await CreateEmployee();
        ManagerDto managerDto = await _employeeService.CreateManagerAsync("Name", id, new[] { employeeDto.Id });
        Assert.Single(_context.Employees, employee => employee.Id.Equals(id));
        Manager manager = await GetManager(id);
        Employee employee = await _context.Employees.GetEntityAsync(employeeDto.Id, default);
        Assert.Contains(employee, manager.Subordinates);
        return managerDto;
    }
    
    [Theory]
    [InlineData("123", "456")]
    public async Task<AccountDto> CreateManagersAccount(string login, string password)
    {
        await CreateEmployee();
        ManagerDto managerDto = await CreateManager();
        AccountDto managerAccountDto = await _accountService.CreateAccountAsync(login, password, managerDto.Id);
        Assert.Single(_context.Accounts, account => account.Id.Equals(managerAccountDto.Id));
        Assert.Single(_context.Accounts, account => account.Login.Equals(managerAccountDto.Login));
        Manager manager = await GetManager(managerDto.Id);
        Account account = await _context.Accounts.GetEntityAsync(managerAccountDto.Id, default);
        Assert.Equal(account, manager.Account);
        return managerAccountDto;
    }
    
    [Theory]
    [InlineData("89053927613")]
    private async Task<MessageSourceDto> CreatePhoneMessageSource(string number)
    {
        MessageSourceDto source =
            await _messageSourceService.CreateMessageSource(MessageTypeDto.Phone, new PhoneNameDto(number));
        Assert.Single(_context.MessageSources, s => s.Id.Equals(source.Id));
        return source;
    }

    [Theory]
    [InlineData("89053927613")]
    private async Task<MessageDto> CreatePhoneMessage(string number)
    {
        MessageDto message = await _messageService.ReceiveMessageAsync(new CreateMessageDto(MessageTypeDto.Phone,
            new PhoneContentDto(number, "HELLO")));
        Assert.Single(_context.Messages, s => s.Id.Equals(message.Id));
        return message;
    }

    [Theory]
    [InlineData("l", "p", "89614580903")]
    public async Task<EmployeeDto> RespondMessage(string login, string password, string number)
    {
        int startCount = await _context.MessagesResponse.CountAsync();
        await CreateEmployee();
        AccountDto account = await CreateEmployeesAccountAsync(login, password);
        await CreatePhoneMessageSource(number);
        MessageDto messageDto = await CreatePhoneMessage(number);
        EmployeeDto employeeDto = await _authorizationService.Authorization(account.Login, account.Password);
        await _messageService.RespondMessageAsync(messageDto.Id,
            new CreateMessageDto(MessageTypeDto.Phone, new PhoneContentDto(number, "hello!")), employeeDto.Id);
        Assert.Equal(startCount + 1, await _context.MessagesResponse.CountAsync());
        return employeeDto;
    }

    [Fact]
    public async Task CreateReport()
    {
        int startCount = await _context.Reports.CountAsync();
        await RespondMessage("ll", "pp", "1234567890");
        AccountDto managerAccount = await CreateManagersAccount("1", "2");
        await _reportService.CreateReportInMemoryAsync(managerAccount.EmployeeId);
        Assert.Equal(startCount + 1, await _context.Reports.CountAsync());
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
    private async Task<Manager> GetManager(Guid id)
    {
        Employee? roma = await _context.Employees.GetEntityAsync(id, default);
        if (roma is Manager manager)
        {
            return manager;
        }

        throw new Exception();
    }
}