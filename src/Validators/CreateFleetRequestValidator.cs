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
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(50); // TODO: This shouldn't be hardcoded, think about how to handle upper limits
        }
    }
}
