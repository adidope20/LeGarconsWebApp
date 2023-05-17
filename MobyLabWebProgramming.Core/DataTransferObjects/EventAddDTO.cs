
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class EventAddDTO
{
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
    public Boolean Available { get; set; } = default!;
    public Boolean Blocked { get; set; } = default!;
    public Boolean Booked { get; set; } = default!;
    public Guid? PropertyId { get; set; }
}
