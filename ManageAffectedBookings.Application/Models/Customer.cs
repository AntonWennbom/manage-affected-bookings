using ManageAffectedBookings.Application.Enums;

namespace ManageAffectedBookings.Application.Models;

public class Customer()
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EmailAdress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string OrganizationNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Roles Role { get; set; }
}
