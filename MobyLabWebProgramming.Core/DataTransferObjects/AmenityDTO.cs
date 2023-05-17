
using System.Collections.ObjectModel;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AmenityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

}
