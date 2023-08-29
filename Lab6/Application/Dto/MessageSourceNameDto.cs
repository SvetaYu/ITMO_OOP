namespace Application.Dto;

public abstract record MessageSourceNameDto;

public record EmailNameDto(string Email) : MessageSourceNameDto;

public record PhoneNameDto(string Number) : MessageSourceNameDto;
