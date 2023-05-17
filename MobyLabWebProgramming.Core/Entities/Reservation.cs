namespace MobyLabWebProgramming.Core.Entities;
public class Reservation : BaseEntity
{
    public decimal Price { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public Guid EventId { get; set; }
    public Event Event { get; set; } = default!;
    public Guid PropertyId { get; set; }
    public Property Property { get; set; } = default!;
}

