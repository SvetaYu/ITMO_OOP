using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IEmployeeService, EmployeeService>();
        collection.AddScoped<IMessageService, MessageService>();
        collection.AddScoped<IMessageSourceService, MessageSourceService>();
        collection.AddScoped<IAuthorizationService, AuthorizationService>();
        collection.AddScoped<IReportService, ReportService>();

        return collection;
    }
}