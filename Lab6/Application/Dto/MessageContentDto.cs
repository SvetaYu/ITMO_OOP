namespace Application.Dto;

public abstract record MessageContentDto;

public record EmailContentDto(string Email, string Subject, string Body) : MessageContentDto;

public record PhoneContentDto(string Phone, string Text) : MessageContentDto;