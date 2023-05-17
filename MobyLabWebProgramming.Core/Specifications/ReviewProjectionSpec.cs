using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ReviewProjectionSpec : BaseSpec<ReviewProjectionSpec, Review, ReviewDTO>
{
    protected override Expression<Func<Review, ReviewDTO>> Spec => e => new()
    {
        Id = e.Id,
        Rating = e.Rating,
        Description = e.Description,
        User = new()
        {
            Id = e.User.Id,
            Email = e.User.Email,
            Name = e.User.Name,
            Role = e.User.Role
        }
    };

    public ReviewProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ReviewProjectionSpec(Guid id) : base(id)
    {
    }

}
