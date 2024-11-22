using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace ForumApi.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult Error(this ControllerBase controller, OperationResult operationResult)
        {
            var error = operationResult.Errors.FirstOrDefault();
            return controller.StatusCode(error.StatusCode, error.Message);
        }
    }
}
