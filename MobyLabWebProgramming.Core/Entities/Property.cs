using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Property : BaseEntity
{
    public string Name { get; set; } = default!;
    public int Rooms { get; set; } = default!;
    public int Bedrooms { get; set; } = default!;
    public int Bathrooms { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Address { get; set; } = default!;
    public decimal NightPrice { get; set; } = default!;
    public PropertyTypeEnum Type { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Reservation> Reservations { get; set; } = default!;
    public ICollection<Amenity> Amenities { get; set; } = default!;
    public ICollection<Event> Events { get; set; } = default!;
}

