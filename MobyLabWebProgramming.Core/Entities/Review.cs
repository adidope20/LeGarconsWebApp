namespace MobyLabWebProgramming.Core.Entities;

public class Review : BaseEntity
{
    public int Rating { get; set; } = default!;
    public string Description { get; set; } = default!;

    public Guid UserId { get; set; }
    public User? User { get; set; }

}
