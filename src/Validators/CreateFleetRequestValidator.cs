using FleetManager.Models.Requests.Fleet;
using FluentValidation;

namespace FleetManager.Validators
{
    public class CreateFleetRequestValidator : AbstractValidator<CreateFleetRequest>
    {
        private const string ResourceNameRegex = "/^[A-Z]+$/i";
        private const string CpuRegex = "[0-9]+m";
        private const string MemoryRegex = "[0-9]+Mi";

        public CreateFleetRequestValidator()
        {
            RuleFor(r => r.Replicas)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(50); // TODO: This shouldn't be hardcoded, think about how to handle upper limits

            RuleFor(r => r.Name)
                .NotEmpty()
                .Matches(ResourceNameRegex);

            RuleFor(r => r.Namespace)
                .NotEmpty()
                .Matches(ResourceNameRegex);

            RuleFor(r => r.Image)
                .NotEmpty()
                .Must(c => Uri.IsWellFormedUriString(c, UriKind.RelativeOrAbsolute));

            RuleFor(r => r.LimitCpu)
                .NotEmpty()
                .Matches(CpuRegex);

            RuleFor(r => r.RequestCpu)
                .NotEmpty()
                .Matches(CpuRegex);

            RuleFor(r => r.LimitMemory)
                .NotEmpty()
                .Matches(MemoryRegex);

            RuleFor(r => r.RequestMemory)
                .NotEmpty()
                .Matches(MemoryRegex);
        }
    }
}
