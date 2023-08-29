using Application.Dto;
using DataAccess.Model;

namespace Application.Extensions;

public static class ManagerAsDto
{
    public static ManagerDto AsDto(this Manager manager)
        => new ManagerDto(manager.Name, manager.Id, manager.Subordinates.Select(m => m.Id).ToArray(), manager.AccountId);
}