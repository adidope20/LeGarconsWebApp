namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record EventUpdateDTO(Guid Id, DateTime Start, DateTime End, Boolean Available, Boolean Blocked, Boolean Booked);
