using ManageAffectedBookings.Application.Enums;
using ManageAffectedBookings.Application.Models;

namespace ManageAffectedBookings.Application.Services;

public class BookingService(
    List<Booking> bookings,
    List<User> users)
{
    private readonly List<Booking> _bookings = bookings;
    private readonly List<User> _users = users;

    public List<AffectedBooking> GetAffectedBookings(Guid equipmentItemId)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        return [.. _bookings
        .Where(booking =>
            booking.EquipmentItem.Id == equipmentItemId &&
            booking.Period.End >= today &&
            booking.Status == BookingStatus.Confirmed)
        .Select(booking =>
        {
            var user = _users
                .FirstOrDefault(user => user.Id == booking.UserId);

            return new AffectedBooking(
                booking.Id,
                user?.Name ?? "Unknown",
                user?.Email ?? "Unknown",
                user?.Phone ?? "Unknown",
                booking.Period);
        })];
    }
}
