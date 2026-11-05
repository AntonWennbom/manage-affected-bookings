using ManageAffectedBookings.Application.Data;
using ManageAffectedBookings.Application.Enums;
using ManageAffectedBookings.Application.Models;
using ManageAffectedBookings.Application.Services;

namespace ManageAffectedBookings.Application.Application;

public static class UnavailableEquipmentFlow
{
    private static BookingService _bookingService = null!;
    private static EquipmentService _equipmentService = null!;
    private static Customer _currentUser = SeedData.Users
    .First(user => user.Role == Roles.Employee);

    public static void Run(BookingService bookingService, EquipmentService equipmentService)
    {
        _bookingService = bookingService;
        _equipmentService = equipmentService;

        Console.WriteLine("Unavailable Equipment Flow");
        Console.WriteLine("--------------------------");

        SelectDemoUser();

        var equipmentItem = new EquipmentItem();

        try
        {
            equipmentItem = SelectDemoEquipmentItem();

        }
        catch (Exception exception)
        {

            Console.WriteLine(exception.Message);
        }

        var equipment = _equipmentService.GetEquipmentById(equipmentItem.EquipmentId);

        if (equipment is null)
        {
            Console.WriteLine("Equipment not found.");
            return;
        }

        Console.WriteLine($"Selected demo Equipment Item: {equipment.Name}, ({equipmentItem.Status})");

        if (!UserHasPermission(_currentUser))
        {
            Console.WriteLine("You do not have permission to mark equipment as unavailable.");
            return;
        }

        string reason = EnterReason(equipment);

        try
        {
            _equipmentService.MarkUnavailable(equipmentItem.Id, reason!);
            Console.WriteLine($"Equipment Item '{equipment.Name}' marked as unavailable for reason: {reason}");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
        }

        ShowAffectedBookings(equipmentItem);

        var itemAsUnavailable = _equipmentService.MarkUnavailable(equipmentItem.Id, reason);

        var updatedEquipmentItem = _equipmentService.GetEquipmentItemById(equipmentItem.Id);
        var updatedEquipment = _equipmentService.GetEquipmentById(equipmentItem.EquipmentId);

        if (updatedEquipmentItem is null)
        {
            Console.WriteLine("Error: Equipment item not found after update.");
            return;
        }

        if (updatedEquipment is null)
        {
            Console.WriteLine("Error: Equipment not found after update.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("------Updated Item------");
        Console.WriteLine($"{updatedEquipment.Name} ({updatedEquipmentItem.Status} for reason: {updatedEquipmentItem.UnavailableReason})");

        Console.ReadLine();
    }

    private static string EnterReason(Equipment equipment)
    {
        Console.WriteLine($"Marking Equipment Item as Unavailable: {equipment.Name}");
        Console.WriteLine($"Enter the reason for marking the equipment as unavailable:");
        while (true)
        {
            var reason = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(reason))
            {
                return reason;
            }
            Console.WriteLine("Reason cannot be empty. Please enter a valid reason:");
        }
    }

    private static void ShowAffectedBookings(EquipmentItem equipmentItem)
    {
        var affectedBookings =
                    _bookingService.GetAffectedBookings(equipmentItem.Id);

        if (affectedBookings.Count == 0)
        {
            Console.WriteLine("No affected bookings found.");
        }

        else
        {
            Console.WriteLine();
            Console.WriteLine("############### Affected Bookings - Contact customer if necessary to cancel booking ###############");

            foreach (var booking in affectedBookings)
            {
                Console.WriteLine();
                Console.WriteLine("##################################");
                Console.WriteLine();
                Console.WriteLine($"""
        Affected Booking: {booking.bookingId}
        Customer Name: {booking.CustomerName}
        Customer Email: {booking.CustomerEmail}
        Customer Phone: {booking.CustomerPhone}
        Period: {booking.Period.Start} to {booking.Period.End}
        """);
            }
        }
    }

    private static void SelectDemoUser()
    {
        Console.WriteLine($"Set current user:");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Employee");
        Console.WriteLine("3. Admin");
        while (true)
        {
            var userInput = Console.ReadLine();
            if (userInput is "1" or "2" or "3")
            {
                _currentUser = userInput switch
                {
                    "1" => SeedData.Users[0],
                    "2" => SeedData.Users[1],
                    "3" => SeedData.Users[2],
                    _ => _currentUser
                };
                break;
            }
            Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
        }

        Console.WriteLine($"You Selected user: {_currentUser.Name} ({_currentUser.Role})");
        Console.WriteLine("--------------------------");
    }



    private static bool UserHasPermission(Customer user)
    {
        return user.Role == Roles.Employee || user.Role == Roles.Admin;
    }

    private static EquipmentItem SelectDemoEquipmentItem()
    {
        Console.WriteLine("Select demo scenario:");
        Console.WriteLine("1. Drill (2 affected bookings)");
        Console.WriteLine("2. Drill (0 affected bookings)");

        while (true)
        {
            var input = Console.ReadLine();
            if (input is "1" or "2")
            {
                return input switch
                {
                    "1" => SeedData.EquipmentItems[0],
                    "2" => SeedData.EquipmentItems[1],

                    _ => throw new InvalidOperationException("Invalid choice")
                };
            }
            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }
    }
}