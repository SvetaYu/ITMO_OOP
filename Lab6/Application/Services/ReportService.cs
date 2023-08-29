using Application.Dto;
using Application.Exceptions;
using Application.Extensions;
using Application.Services.Interfaces;
using DataAccess;
using DataAccess.Model;

namespace Application.Services;

public class ReportService : IReportService
{
    private readonly DatabaseContext _context;

    public ReportService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> CreateReportInPhysicalFsAsync(Guid employeeId, string path)
    {
        var repository = new PhysicalRepository(path);
        return await CreateReportAsync(employeeId, repository);
    }

    public async Task<ReportDto> CreateReportInMemoryAsync(Guid employeeId)
    {
        var repository = new InMemoryRepository();
        return await CreateReportAsync(employeeId, repository);
    }

    private async Task<ReportDto> CreateReportAsync(Guid employeeId, Repository repository)
    {
        Employee employee = await _context.Employees.GetEntityAsync(employeeId, default);
        if (employee is not Manager manager) throw EmployeeException.UnavailableOperation();
        
        string text = await GenerateText(manager);
        string path = CreateReportFile(employeeId, repository, text);
        var report = new Report(Guid.NewGuid(), manager, Path.Combine(repository.Path, path));
        
        _context.Reports.Add(report);
        await _context.SaveChangesAsync();
        return report.AsDto();
    }

    private async Task<string> GenerateText(Manager manager)
    {
        int processedMessageCount = (await ProcessedMessagesAsync(manager)).Count();
        string result = $"Processed: {processedMessageCount}";
        foreach (MessageSource source in _context.MessageSources)
        {
            int receivedMessageAtSourceCount = (await ReceivedMessageAtSourceAsync(manager, source)).Count();
            result += $"\nOn {source.Id}:\nReceived: {receivedMessageAtSourceCount}";
        }

        return result;
    }
    private Task<IQueryable<MessageResponse>> ProcessedMessagesAsync(Manager manager)
    {
        return Task.FromResult(
            _context.MessagesResponse.Where(response => manager.Subordinates.Contains(response.Employee)));
    }
    private Task<IQueryable<Message>> ReceivedMessageAtSourceAsync(Manager manager, MessageSource source)
    {
        return Task.FromResult(_context.Messages.Where(message => message.MessageSource.Equals(source)));
    }

    private string CreateReportFile(Guid employeeId, Repository repository, string text)
    {
        DateTime time = DateTime.Now;
        string timeToStr = time.ToString("dd.MM.yyyy-hh.mm.ss.ff");
        string dir = employeeId.ToString();
        repository.CreateDirectory(dir);
        string newPath = Path.Combine(dir, timeToStr);
        repository.CreateFileWithText(newPath +".txt", text);
        return newPath;
    }
}