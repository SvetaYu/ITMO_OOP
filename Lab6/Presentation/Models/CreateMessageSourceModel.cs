using Application.Dto;

namespace Presentation.Models;

public record CreateMessageSourceModel(MessageTypeDto Type, MessageSourceNameDto Name);