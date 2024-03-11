using System;
using System.Collections.Generic;

namespace Dal;

public partial class Trip
{
    public int TripCode { get; set; }

    public string? TripDestination { get; set; }

    public int? TypeCode { get; set; }

    public DateTime? TripDate { get; set; }

    public TimeSpan? DepartureTime { get; set; }

    public int? TripDurationHours { get; set; }

    public int? AvailablePlaces { get; set; }

    public decimal? Price { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<BookingPlace> BookingPlaces { get; set; } = new List<BookingPlace>();

    public virtual TripType? TypeCodeNavigation { get; set; }
}
