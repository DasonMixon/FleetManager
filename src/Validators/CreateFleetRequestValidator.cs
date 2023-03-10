using FleetManager.Models.Requests.Fleet;
using FluentValidation;

namespace FleetManager.Validators
{
    public class CreateFleetRequestValidator : AbstractValidator<CreateFleetRequest>
    {
        public CreateFleetRequestValidator()
        {
            RuleFor(r => r.Replicas)
                .NotEmpty()
                .GreaterThanOrEqualTo(Constants.MinimumResourceCount.FleetReplicas)
                .LessThanOrEqualTo(Constants.MaximumResourceCount.FleetReplicas);

            RuleFor(r => r.Name)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Namespace)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Image)
                .NotEmpty()
                .Must(c => Uri.IsWellFormedUriString(c, UriKind.RelativeOrAbsolute));

            RuleFor(r => r.LimitCpu)
                .NotEmpty()
                .Matches(Constants.Regex.Cpu);

            RuleFor(r => r.RequestCpu)
                .NotEmpty()
                .Matches(Constants.Regex.Cpu);

            RuleFor(r => r.LimitMemory)
                .NotEmpty()
                .Matches(Constants.Regex.Memory);

            RuleFor(r => r.RequestMemory)
                .NotEmpty()
                .Matches(Constants.Regex.Memory);
        }
    }
}
