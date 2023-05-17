using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class AmenityAddDTO
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public ICollection<Guid>? PropertiesId { get; set; }
}
