using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Errors;
using CryoVigil.API.Shared.Application.Model;
using CryoVigil.API.Shared.Interfaces.Rest.ProblemDetails;
using CryoVigil.API.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CryoVigil.API.Alerts.Interfaces.Rest.Transform;

public static class AlertActionResultAssembler
{
    private static int ToStatusCodeFromAlertError(AlertError error)
    {
        return error switch
        {
            AlertError.AlertNotFound => StatusCodes.Status404NotFound,
            AlertError.DuplicateAlertCode => StatusCodes.Status409Conflict,
            AlertError.InvalidLifecycleTransition => StatusCodes.Status400BadRequest,
            AlertError.CorrectiveActionRequired => StatusCodes.Status400BadRequest,
            AlertError.OperationCancelled => StatusCodes.Status409Conflict,
            AlertError.DatabaseError => StatusCodes.Status500InternalServerError,
            AlertError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateAlertResult(
        ControllerBase controller,
        Result<Alert> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Alert, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = result.Error is AlertError alertError ? alertError : AlertError.InternalServerError;
        var statusCode = ToStatusCodeFromAlertError(error);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, error, result.Message);
    }

    public static IActionResult ToActionResultFromUpdateAlertResult(
        ControllerBase controller,
        Result<Alert> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Alert, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = result.Error is AlertError alertError ? alertError : AlertError.InternalServerError;
        var statusCode = ToStatusCodeFromAlertError(error);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, error, result.Message);
    }

    public static IActionResult ToActionResultFromGetAlertByIdResult(
        ControllerBase controller,
        Alert? alert,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Alert, IActionResult> successAction)
    {
        if (alert is null)
        {
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                StatusCodes.Status404NotFound,
                AlertError.AlertNotFound,
                "The specified alert was not found.");
        }

        return successAction(alert);
    }
}
