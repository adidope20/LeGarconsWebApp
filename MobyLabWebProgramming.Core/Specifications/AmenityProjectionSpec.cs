using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
namespace MobyLabWebProgramming.Core.Specifications;

public sealed class AmenityProjectionSpec : BaseSpec<AmenityProjectionSpec, Amenity, AmenityDTO>
{
    protected override Expression<Func<Amenity, AmenityDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
    };

    public AmenityProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public AmenityProjectionSpec(Guid id) : base(id)
    {
    }

    public AmenityProjectionSpec(string? search)
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
