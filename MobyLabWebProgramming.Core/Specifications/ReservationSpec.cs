using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ReservationSpec : BaseSpec<ReservationSpec, Reservation>
{
    public ReservationSpec(Guid id) : base(id)
    {
    }
    public ReservationSpec(decimal price)
    {
        Query.Where(e => e.Price == price);
    }
}