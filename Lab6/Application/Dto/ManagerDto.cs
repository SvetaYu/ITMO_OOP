namespace Application.Dto;

public record ManagerDto(string Name, Guid Id, Guid[] SubordinatesId,  Guid? AccountId);