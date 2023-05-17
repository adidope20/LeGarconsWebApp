using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
namespace MobyLabWebProgramming.Core.Specifications;

public sealed class EventProjectionSpec : BaseSpec<EventProjectionSpec, Event, EventDTO>
{
    protected override Expression<Func<Event, EventDTO>> Spec => e => new()
    {
        Id = e.Id,
        Start = e.Start,
        End = e.End,
    };

    public EventProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public EventProjectionSpec(Guid id) : base(id)
    {
    }

}
