using FleetManager.Exceptions;
using FleetManager.Models.K8sManifests;
using FleetManager.Models.Requests.Fleet;
using FleetManager.Models.Responses.Fleet;
using FleetManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetsController : ControllerBase
    {
        private readonly IFleetService _fleetService;

        public FleetsController(IFleetService fleetService)
        {
            _fleetService = fleetService;
        }

        [HttpGet("{namespace}/{name}")]
        public async Task<ActionResult<Fleet>> Get(string @namespace, string name)
        {
            ControllerParameterValidationException.ThrowIfNullEmptyOrWhitespace(
                Param.From(@namespace),
                Param.From(name));

            return Ok(await _fleetService.Get(@namespace, name));
        }

        [HttpGet("{namespace}")]
        public async Task<ActionResult<IEnumerable<Fleet>>> List(string @namespace)
        {
            ControllerParameterValidationException.ThrowIfNullEmptyOrWhitespace(
                Param.From(@namespace));

            return Ok(await _fleetService.List(@namespace));
        }

        [HttpPost]
        public async Task<ActionResult<FleetCreatedResponse>> Create(CreateFleetRequest request)
        {
            return Ok(await _fleetService.Create(request));
        }

        // TODO: Maybe this should be PATCH since you can update individual parts of the fleet deployment
        [HttpPut]
        public async Task<ActionResult<FleetUpdatedResponse>> Update(UpdateFleetRequest request)
        {
            return Ok(await _fleetService.Update(request));
        }

        [HttpDelete]
        public async Task<ActionResult<FleetDeletedResponse>> Delete(DeleteFleetRequest request)
        {
            return Ok(await _fleetService.Delete(request));
        }
    }
}
