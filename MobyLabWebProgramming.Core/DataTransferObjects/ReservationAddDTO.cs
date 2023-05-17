
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReservationAddDTO
{
    public decimal Price { get; set; } = default!;
    public Guid PropertyId { get; set; } = default!;
    public EventReservationDTO Event { get; set; } = default!;
}
