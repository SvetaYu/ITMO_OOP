using DataAccess.Model;

namespace Application.Dto;

public record MessageDto(Guid Id, MessageTypeDto Type, MessageContentDto Content, Guid SourceId, DateTime Time, MessageState State);