
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class PropertyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Rooms { get; set; } = default!;
    public int Bedrooms { get; set; } = default!;
    public int Bathrooms { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Address { get; set; } = default!;
    public decimal NightPrice { get; set; } = default!;
    public PropertyTypeEnum Type { get; set; } = default!;
    public UserDTO User { get; set; } = default!;
}
