using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
namespace MobyLabWebProgramming.Core.Specifications;

public sealed class PropertyProjectionSpec : BaseSpec<PropertyProjectionSpec, Property, PropertyDTO>
{
    protected override Expression<Func<Property, PropertyDTO>> Spec => e => new()
    {
        Id = e.Id,
        Rooms = e.Rooms,
        Type = e.Type,
        User = new()
        {
            Id = e.User.Id,
            Email = e.User.Email,
            Name = e.User.Name,
            Role = e.User.Role
        }
    };

    public PropertyProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public PropertyProjectionSpec(Guid id) : base(id)
    {
    }

    public PropertyProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); 
    }
}
