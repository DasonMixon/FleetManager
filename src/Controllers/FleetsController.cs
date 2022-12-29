using FleetManager.Models.K8sManifests;
using FleetManager.Models.Requests.Fleet;
using FleetManager.Models.Responses.Fleet;
using FleetManager.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetsController : ControllerBase
    {
        private readonly IFleetService _fleetService;
        private readonly IValidator<CreateFleetRequest> _createFleetRequestValidator;
        private readonly IValidator<UpdateFleetRequest> _updateFleetRequestValidator;
        private readonly IValidator<DeleteFleetRequest> _deleteFleetRequestValidator;

        public FleetsController(IFleetService fleetService,
            IValidator<CreateFleetRequest> createFleetRequestValidator,
            IValidator<UpdateFleetRequest> updateFleetRequestValidator,
            IValidator<DeleteFleetRequest> deleteFleetRequestValidator)
        {
            _fleetService = fleetService;
            _createFleetRequestValidator = createFleetRequestValidator;
            _updateFleetRequestValidator = updateFleetRequestValidator;
            _deleteFleetRequestValidator = deleteFleetRequestValidator;
        }

        [HttpGet("{namespace}/{name}")]
        public async Task<ActionResult<Fleet>> Get(string @namespace, string name)
        {
            return Ok(await _fleetService.Get(@namespace, name));
        }

        [HttpGet("{namespace}")]
        public async Task<ActionResult<IEnumerable<Fleet>>> List(string @namespace)
        {
            return Ok(await _fleetService.List(@namespace));
        }

        [HttpPost]
        public async Task<ActionResult<FleetCreatedResponse>> Create(CreateFleetRequest request)
        {
            var validationResult = _createFleetRequestValidator.Validate(request);

            return validationResult.IsValid
                ? Ok(await _fleetService.Create(request))
                : validationResult.BuildResult();
        }

        [HttpPut]
        public async Task<ActionResult<FleetUpdatedResponse>> Update(UpdateFleetRequest request)
        {
            var validationResult = _updateFleetRequestValidator.Validate(request);

            return validationResult.IsValid
                ? Ok(await _fleetService.Update(request))
                : validationResult.BuildResult();
        }

        [HttpDelete]
        public async Task<ActionResult<FleetDeletedResponse>> Delete(DeleteFleetRequest request)
        {
            var validationResult = _deleteFleetRequestValidator.Validate(request);
            return validationResult.IsValid
                ? Ok(await _fleetService.Delete(request))
                : validationResult.BuildResult();
        }
    }
}
