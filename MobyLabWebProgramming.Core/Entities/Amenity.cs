
namespace MobyLabWebProgramming.Core.Entities;

public class Amenity : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public ICollection<Property> Properties { get; set; } = default!;
}
