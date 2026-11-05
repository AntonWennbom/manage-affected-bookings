using ManageAffectedBookings.Application.Enums;

namespace ManageAffectedBookings.Application.Models;

public class Booking
{
    public Guid Id { get; set; }
    public EquipmentItem EquipmentItem { get; set; } = new EquipmentItem();
    public Guid CustomerId { get; set; }
    public BookingStatus Status { get; set; }
    public BookingPeriod Period { get; set; } = new BookingPeriod();
}
