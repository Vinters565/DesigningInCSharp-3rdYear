using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Domain.Common.Results;

namespace Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(
        this Result<T> result, ControllerBase thisController, Func<T, ActionResult>? onValue = null)
    {
        if (result.IsError) 
            return HandleError(result.Error, thisController);

        return onValue?.Invoke(result.Value) ?? thisController.Ok(result.Value);
    }
    
    public static ActionResult ToActionResult(this Result result, ControllerBase thisController)
    {
        if (result.IsError) 
            return HandleError(result.Error, thisController);

        return thisController.NoContent();
    }

    private static ActionResult HandleError(Error error, ControllerBase thisController)
    {
        return thisController.StatusCode((int)error.HttpStatus, error.Message);
    }
}