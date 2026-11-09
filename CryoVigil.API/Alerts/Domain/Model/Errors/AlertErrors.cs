using CryoVigil.API.Shared.Domain.Model.Errors;

namespace CryoVigil.API.Alerts.Domain.Model.Errors;

public static class AlertErrors
{
    public static readonly Error AlertNotFound =
        Error.NotFound("Alerts.AlertNotFound", "The specified alert was not found.");

    public static readonly Error DuplicateAlertCode =
        Error.Conflict("Alerts.DuplicateAlertCode", "An alert with the same code already exists.");

    public static Error DuplicateAlertCodeWithCode(string code) =>
        Error.Conflict("Alerts.DuplicateAlertCode", $"An alert with code '{code}' already exists.");

    public static readonly Error InvalidLifecycleTransition =
        Error.Validation("Alerts.InvalidLifecycleTransition", "The requested lifecycle transition is not permitted.");

    public static readonly Error CorrectiveActionRequired =
        Error.Validation("Alerts.CorrectiveActionRequired", "Critical alerts require a valid corrective action note to be resolved.");
}
