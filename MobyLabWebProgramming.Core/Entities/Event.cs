namespace MobyLabWebProgramming.Core.Entities;

public class Event : BaseEntity
{
    public Boolean Available { get; set; } = default!;
    public Boolean Blocked { get; set; } = default!;
    public Boolean Booked { get; set; } = default!;
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
    public Reservation? Reservation { get; set; }
    public Guid? PropertyId { get; set; }
    public Property? Property { get; set; }
}
