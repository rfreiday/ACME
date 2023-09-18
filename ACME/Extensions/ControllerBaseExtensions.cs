using Microsoft.AspNetCore.Mvc;

namespace ACME.Extensions;

public static class ControllerBaseExtensions
{
    public static BadRequestObjectResult BadRequestMessage(this ControllerBase controller, Exception ex)
    {
        if (ex is null)
        {
            throw new ArgumentNullException(nameof(ex));
        }
        if (ex.InnerException is not null)
        {
            return new BadRequestObjectResult(ex.InnerException.Message);
        }
        return new BadRequestObjectResult(ex.Message);
    }
}
