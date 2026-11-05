using ManageAffectedBookings.Application.Enums;
using ManageAffectedBookings.Application.Models;

namespace ManageAffectedBookings.Application.Services;

public class BookingService(
    List<Booking> bookings,
    List<Customer> customers)
{
    private readonly List<Booking> _bookings = bookings;
    private readonly List<Customer> _customers = customers;

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
            var user = _customers
                .FirstOrDefault(user => user.Id == booking.CustomerId);

            return new AffectedBooking(
                booking.Id,
                user?.Name ?? "Unknown",
                user?.EmailAdress ?? "Unknown",
                user?.PhoneNumber ?? "Unknown",
                booking.Period);
        })];
    }
}
