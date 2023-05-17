using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class EventSpec : BaseSpec<EventSpec, Event>
{
    public EventSpec(Guid id) : base(id)
    {
    }

}
