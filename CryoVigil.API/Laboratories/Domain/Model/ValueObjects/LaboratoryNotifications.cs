namespace CryoVigil.API.Laboratories.Domain.Model.ValueObjects;

public record LaboratoryNotifications(
    bool Email = true,
    bool Sms = true,
    bool Push = false,
    bool CriticalOnly = true);
