using ManageAffectedBookings.Application.Application;
using ManageAffectedBookings.Application.Data;
using ManageAffectedBookings.Application.Services;


var bookingService =
            new BookingService(SeedData.Bookings, SeedData.Users);

var equipmentService =
    new EquipmentService(SeedData.EquipmentItems, SeedData.Equipments);

UnavailableEquipmentFlow.Run(bookingService, equipmentService);
