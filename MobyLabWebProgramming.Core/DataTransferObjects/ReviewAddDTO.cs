namespace MobyLabWebProgramming.Core.Entities;

public class ReviewAddDTO
{
    public int Rating { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public Guid UserId { get; set; }

}
