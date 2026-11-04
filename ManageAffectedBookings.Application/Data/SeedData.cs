using ManageAffectedBookings.Application.Enums;
using ManageAffectedBookings.Application.Models;

namespace ManageAffectedBookings.Application.Data;

public static class SeedData
{
    public static List<Equipment> Equipments =>
    [
    new()
    {
        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        Name = "Makita Drill",
        Description = "18V Cordless Drill",
        ToolType = ToolType.Drill
    },

    new()
    {
        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
        Name = "Bosch Jigsaw",
        Description = "Professional Jigsaw",
        ToolType = ToolType.Jigsaw
    }
    ];

    public static List<EquipmentItem> EquipmentItems =>
    [
    new()
    {
        Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
        EquipmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        Status = EquipmentStatus.Available
    },

    new()
    {
        Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
        EquipmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        Status = EquipmentStatus.Available
    },

    new()
    {
        Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
        EquipmentId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
        Status = EquipmentStatus.Unavailable,
        UnavailableReason = "Broken motor"
    }
    ];

public static List<Booking> Bookings =>
    [
    // Ska visas vid val av utrustning 1
    new()
    {
        Id = Guid.NewGuid(),
        EquipmentItem = EquipmentItems[0],
        UserId = Users[0].Id,
        Status = BookingStatus.Confirmed,
        Period = new BookingPeriod
        {
            Start = DateOnly.FromDateTime(DateTime.Today.AddDays(7)),
        End = DateOnly.FromDateTime(DateTime.Today.AddDays(10))
        }
    },

    // Ska visas vid val av utrustning 1
    new()
    {
        Id = Guid.NewGuid(),
        EquipmentItem = EquipmentItems[0],
        UserId = Users[1].Id,
        Status = BookingStatus.Confirmed,
        Period = new BookingPeriod
        {
            Start = DateOnly.FromDateTime(DateTime.Today.AddDays(20)),
            End = DateOnly.FromDateTime(DateTime.Today.AddDays(22))
        }
    },

    // Ska filtreras bort (avbokad)
    new()
    {
        Id = Guid.NewGuid(),
        EquipmentItem = EquipmentItems[0],
        UserId = Users[0].Id,
        Status = BookingStatus.Cancelled,
        Period = new BookingPeriod
        {
            Start = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
            End = DateOnly.FromDateTime(DateTime.Today.AddDays(35))
        }
    },

    // Ska filtreras bort (gammal)
    new()
    {
        Id = Guid.NewGuid(),
        EquipmentItem = EquipmentItems[0],
        UserId = Users[1].Id,
        Status = BookingStatus.Completed,
        Period = new BookingPeriod
        {
            Start = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)),
            End = DateOnly.FromDateTime(DateTime.Today.AddDays(-5))
        }
    }
    ];

public static List<User> Users =>
    [
         new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
        Name = "Anna Andersson",
        Email = "anna@test.se",
        Phone = "070-1111111",
        Role = Roles.Customer
    },

    new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
        Name = "Erik Eriksson",
        Email = "erik@test.se",
        Phone = "070-2222222",
        Role = Roles.Employee
    },

    new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
        Name = "Pelle Personal",
        Email = "pelle@company.se",
        Phone = "070-3333333",
        Role = Roles.Admin
    },
    new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
        Name = "Kund Kundsson",
        Email = "kund@company.se",
        Phone = "070-3333333",
        Role = Roles.Customer
    }
    ];
}
