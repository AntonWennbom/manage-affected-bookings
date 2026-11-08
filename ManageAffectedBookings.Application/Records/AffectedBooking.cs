using ManageAffectedBookings.Application.Models;

namespace ManageAffectedBookings.Application.Records;

public record AffectedBooking
    (
    Guid bookingId,
    string CustomerName,
    string CustomerEmail,
    string CustomerPhone,
    BookingPeriod Period
    );
