using ManageAffectedBookings.Application.Models;

namespace ManageAffectedBookings.Application.Services;

public class BookingService(List<Booking> bookings)
{
    private readonly List<Booking> _bookings = bookings;

    public List<Booking> GetAffectedBookings(Guid equipmentItemId)
    {
        return [.. _bookings
            .Where(booking =>
                booking.EquipmentItem.Id == equipmentItemId &&
                booking.Period.End >= DateOnly.FromDateTime(DateTime.Today))];
    }
}
