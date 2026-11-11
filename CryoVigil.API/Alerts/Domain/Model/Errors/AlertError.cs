namespace CryoVigil.API.Alerts.Domain.Model.Errors;

public enum AlertError
{
    None,
    AlertNotFound,
    DuplicateAlertCode,
    InvalidLifecycleTransition,
    CorrectiveActionRequired,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
