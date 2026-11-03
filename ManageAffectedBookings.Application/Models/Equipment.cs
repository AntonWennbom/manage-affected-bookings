using ManageAffectedBookings.Application.Enums;

namespace ManageAffectedBookings.Application.Models;

public class Equipment()
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ToolType ToolType { get; set; }
}
