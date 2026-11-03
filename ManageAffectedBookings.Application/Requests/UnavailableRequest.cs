namespace ManageAffectedBookings.Application.Requests;

public record UnavailableRequest
(
    Guid EquipmentId,
    string Reason
);
