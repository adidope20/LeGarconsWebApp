using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ReservationProjectionSpec : BaseSpec<ReservationProjectionSpec, Reservation, ReservationDTO>
{
    protected override Expression<Func<Reservation, ReservationDTO>> Spec => e => new()
    {
        Id = e.Id,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt,
        User = new()
        {
            Id = e.User.Id,
            Email = e.User.Email,
            Name = e.User.Name,
            Role = e.User.Role
        },
        Property = new()
        {
            Id = e.Property.Id,
            Rooms = e.Property.Rooms,
            Type = e.Property.Type,
            User = new()
            {
                Id = e.User.Id,
                Email = e.User.Email,
                Name = e.User.Name,
                Role = e.User.Role
            }
        },
        Event = new()
        {
            Id = e.Event.Id,
            Start = e.Event.Start,
            End = e.Event.End,
            Available = e.Event.Available,
            Booked = e.Event.Booked,
            Blocked = e.Event.Blocked
        }
    };

    public ReservationProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ReservationProjectionSpec(Guid id) : base(id)
    {
    }
}
