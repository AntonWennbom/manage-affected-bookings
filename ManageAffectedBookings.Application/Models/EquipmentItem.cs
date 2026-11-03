using ManageAffectedBookings.Application.Enums;

namespace ManageAffectedBookings.Application.Models;

public class EquipmentItem()
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public EquipmentStatus Status { get; set; }
    public string? UnavailableReason { get; set; }
}
