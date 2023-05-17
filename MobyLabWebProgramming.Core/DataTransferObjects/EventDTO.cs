
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class EventDTO
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
    public Boolean Available { get; set; } = default!;
    public Boolean Blocked { get; set; } = default!;
    public Boolean Booked { get; set; } = default!;
    public ReservationDTO Reservation { get; set; } = default!;
}
