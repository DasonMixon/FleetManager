using FleetManager.Models.Requests.Fleet;
using FluentValidation;

namespace FleetManager.Validators
{
    public class DeleteFleetRequestValidator : AbstractValidator<DeleteFleetRequest>
    {
        public DeleteFleetRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Namespace)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);
        }
    }
}
