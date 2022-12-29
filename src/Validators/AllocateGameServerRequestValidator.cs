using FleetManager.Models.Requests.GameServer;
using FluentValidation;

namespace FleetManager.Validators
{
    public class AllocateGameServerRequestValidator : AbstractValidator<AllocateGameServerRequest>
    {
        public AllocateGameServerRequestValidator()
        {
            RuleFor(r => r.FleetName)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);

            RuleFor(r => r.Namespace)
                .NotEmpty()
                .Matches(Constants.Regex.ResourceName);
        }
    }
}
