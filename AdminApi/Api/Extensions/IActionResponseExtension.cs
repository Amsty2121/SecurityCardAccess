using Domain.Exceptions;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Extensions
{
    public static class IActionResponseExtension
    {
        public static IActionResult ToOk<TResult, TContract>(
            this Result<TResult> result, Func<TResult, TContract> mapper)
        {
            return result.Match<IActionResult>(obj =>
            {
                return new OkObjectResult(mapper(obj));

            }, exception =>
            {
                if (exception is CustomException ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }

                return new StatusCodeResult(500);
            });
        }

    }
}
