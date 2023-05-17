
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReservationDTO
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal Price { get; set; } = default!;

    public UserDTO User { get; set; } = default!;
    public PropertyDTO Property { get; set; } = default!;
    public EventDTO Event { get; set; } = default!;
}
