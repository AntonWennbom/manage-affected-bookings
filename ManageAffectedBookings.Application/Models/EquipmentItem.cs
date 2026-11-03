using ManageAffectedBookings.Application.Enums;

namespace ManageAffectedBookings.Application.Models;

public class EquipmentItem(Guid id, Guid equipmentId, EquipmentStatus status, string? unavailableReason)
{
    public Guid Id = id;
    public Guid EquipmentId = equipmentId;
    public EquipmentStatus Status = status;
    public string? UnavailableReason = unavailableReason;
}
