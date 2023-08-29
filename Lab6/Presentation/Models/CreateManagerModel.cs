namespace Presentation.Models;

public record CreateManagerModel(string Name, Guid[] SubordinatesId);