namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ReviewUpdateDTO(Guid Id, string? Description, int Rating);
