using FleetManager.Filters;
using FluentValidation;
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

        public static IRuleBuilder<T, TProperty> When<T, TProperty>(this IRuleBuilderInitial<T, TProperty> rule, Func<T, bool> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            return rule.Configure(config =>
            {
                config.ApplyCondition(ctx => predicate(ctx.InstanceToValidate), applyConditionTo);
            });
        }

        public static IRuleBuilder<T, TProperty> WhenNotNull<T, TProperty>(this IRuleBuilderInitial<T, TProperty> rule, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            return rule.Configure(config =>
            {
                config.ApplyCondition(ctx => ctx.InstanceToValidate is not null, applyConditionTo);
            });
        }
    }
}

