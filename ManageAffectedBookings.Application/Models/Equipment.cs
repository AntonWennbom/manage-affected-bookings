using ManageAffectedBookings.Application.Enums;

namespace ManageAffectedBookings.Application.Models;

public class Equipment(Guid id, string name, string description, ToolType toolType)
{
    public Guid Id = id;
    public string Name = name;
    public string Description = description;
    public ToolType ToolType = toolType;
}
