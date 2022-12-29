using FleetManager.Models.Requests.Fleet;
using FluentValidation;

namespace FleetManager.Validators
{
    public class UpdateFleetRequestValidator : AbstractValidator<UpdateFleetRequest>
    {
        public UpdateFleetRequestValidator()
        {
            RuleFor(r => r.Replicas)
                .WhenNotNull()
                .GreaterThanOrEqualTo(Constants.MinimumResourceCount.FleetReplicas)
                .LessThanOrEqualTo(Constants.MaximumResourceCount.FleetReplicas);

            RuleFor(r => r.Name)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Namespace)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Image)
                .WhenNotNull()
                .Must(c => Uri.IsWellFormedUriString(c, UriKind.RelativeOrAbsolute));

            RuleFor(r => r.LimitCpu)
                .WhenNotNull()
                .Matches(Constants.Regex.Cpu);

            RuleFor(r => r.RequestCpu)
                .WhenNotNull()
                .Matches(Constants.Regex.Cpu);

            RuleFor(r => r.LimitMemory)
                .WhenNotNull()
                .Matches(Constants.Regex.Memory);

            RuleFor(r => r.RequestMemory)
                .WhenNotNull()
                .Matches(Constants.Regex.Memory);
        }
    }
}
