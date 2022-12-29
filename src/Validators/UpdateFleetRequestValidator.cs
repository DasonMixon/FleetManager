using FleetManager.Models.Requests.Fleet;
using FluentValidation;

namespace FleetManager.Validators
{
    public class UpdateFleetRequestValidator : AbstractValidator<UpdateFleetRequest>
    {
        public UpdateFleetRequestValidator()
        {
            RuleFor(r => r.Replicas)
                .GreaterThanOrEqualTo(Constants.MinimumResourceCount.FleetReplicas)
                .LessThanOrEqualTo(Constants.MaximumResourceCount.FleetReplicas)
                .When(r => r.Replicas is not null);

            RuleFor(r => r.Name)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Namespace)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Image)
                .Must(c => Uri.IsWellFormedUriString(c, UriKind.RelativeOrAbsolute))
                .When(r => r.Image is not null);

            RuleFor(r => r.LimitCpu)
                .Matches(Constants.Regex.Cpu)
                .When(r => r.LimitCpu is not null);

            RuleFor(r => r.RequestCpu)
                .Matches(Constants.Regex.Cpu)
                .When(r => r.RequestCpu is not null);

            RuleFor(r => r.LimitMemory)
                .Matches(Constants.Regex.Memory)
                .When(r => r.LimitMemory is not null);

            RuleFor(r => r.RequestMemory)
                .Matches(Constants.Regex.Memory)
                .When(r => r.RequestMemory is not null);
        }
    }
}
