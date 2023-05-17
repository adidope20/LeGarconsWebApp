using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class PropertySpec : BaseSpec<PropertySpec, Property>
{
    public PropertySpec(Guid id) : base(id)
    {
    }

    public PropertySpec(string name)
    {
        Query.Where(e => e.Name == name);
    }

}
