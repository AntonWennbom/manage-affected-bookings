using ManageAffectedBookings.Application.Enums;

namespace ManageAffectedBookings.Application.Models;

public class User(Guid id, string name, string email, string phone, Roles role)
{
    public Guid Id = id;
    public string Name = name;
    public string Email = email;
    public string Phone = phone;
    public Roles Role = role;
}
