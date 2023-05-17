
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReviewDTO
{
    public Guid Id { get; set; }
    public int Rating { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public UserDTO User { get; set; }

}
