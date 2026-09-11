using Microsoft.AspNetCore.Mvc;
using HackNewsApp.Application.Common;
using HackNewsApp.Application.Contracts.ResultExecution;
using HackNewsApp.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HackNewsApp.Api.ResultExecution
{
    public sealed class ResultExecutor : IResultExecutor
    {
        public IActionResult Execute<T>(ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(result.Value);
            }

            return result.ErrorType switch
            {
                ErrorType.Validation => controller.BadRequest(
                    CreateProblemDetails(StatusCodes.Status400BadRequest,
                    "Validation Error!", result.Error)),

                ErrorType.NotFound => controller.NotFound(
                    CreateProblemDetails(StatusCodes.Status404NotFound,
                    "Resource Not Found!", result.Error)),

                ErrorType.Unauthorised => controller.Unauthorized(),

                ErrorType.Forbidden => controller.Forbid(),

                ErrorType.Conflict => controller.Conflict(
                    CreateProblemDetails(StatusCodes.Status409Conflict,
                    "Conflict!", result.Error)),

                ErrorType.ExternalService => controller.StatusCode(StatusCodes.Status502BadGateway,
                    CreateProblemDetails(StatusCodes.Status502BadGateway, "External Service Error!", result.Error)),

                _ => controller.StatusCode(StatusCodes.Status500InternalServerError,
                    CreateProblemDetails(StatusCodes.Status500InternalServerError,
                    "Unexpected Error!", result.Error))
            };


            return controller.Ok(result.Value);
        }

        private static ProblemDetails CreateProblemDetails(int statusCode, string title,
            string? detail)
        {
            return new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };
        }


    }
}
