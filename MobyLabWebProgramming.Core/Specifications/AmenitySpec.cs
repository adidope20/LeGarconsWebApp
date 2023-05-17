using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class AmenitySpec : BaseSpec<AmenitySpec, Amenity>
{
    public AmenitySpec(Guid id) : base(id)
    {
    }

    public AmenitySpec(string name)
    {
        Query.Where(e => e.Name == name);
    }

}
