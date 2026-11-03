using ManageAffectedBookings.Application.Enums;
using ManageAffectedBookings.Application.Models;

namespace ManageAffectedBookings.Application.Services;

public class EquipmentService(List<EquipmentItem> items)
{
    private readonly List<EquipmentItem> _items = items;

    public bool MarkUnavailable(Guid id, string reason)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item is null)
            return false;

        item.Status = EquipmentStatus.Unavailable;
        item.UnavailableReason = reason;

        return true;
    }
}
