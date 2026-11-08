using ManageAffectedBookings.Application.Enums;
using ManageAffectedBookings.Application.Models;

namespace ManageAffectedBookings.Application.Services;

public class EquipmentService(List<EquipmentItem> items, List<Equipment> equipments)
{
    private readonly List<EquipmentItem> _items = items;
    private readonly List<Equipment> _equipments = equipments;

    public bool MarkUnavailable(Guid id, string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason cannot be null or empty.");

        var item = _items.FirstOrDefault(item => item.Id == id);

        if (item is null)
            return false;

        item.Status = EquipmentStatus.Unavailable;
        item.UnavailableReason = reason;

        return true;
    }

    public EquipmentItem? GetEquipmentItemById(Guid id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public Equipment? GetEquipmentById(Guid id)
    {
        var item = _equipments.FirstOrDefault(equipment => equipment.Id == id);
        if (item is null)
        {
            return null;
        }
        return item;
    }
}
