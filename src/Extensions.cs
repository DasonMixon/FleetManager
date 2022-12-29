using FleetManager.Filters;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager
{
	public static class Extensions
	{
        public static ObjectResult BuildResult(this ValidationResult result)
        {
            return new BadRequestObjectResult(new GenericHttpResponse(result.Errors.Select(v => v.ErrorMessage).ToArray()));
        }
    }
}
