using FleetManager.Models;
using FleetManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetsController : ControllerBase
    {
        private readonly IKubernetesService _kubernetesService;

        public FleetsController(IKubernetesService kubernetesService)
        {
            _kubernetesService = kubernetesService;
        }

        [HttpGet("{namespace}")]
        public async Task<ActionResult<IEnumerable<Fleet>>> Get(string @namespace)
        {
            return Ok(await _kubernetesService.ListFleets(@namespace));
        }

        [HttpGet("{namespace}/{name}")]
        public async Task<ActionResult<Fleet>> Get(string @namespace, string name)
        {
            return Ok(await _kubernetesService.GetFleet(@namespace, name));
        }

        [HttpPost]
        public async Task<ActionResult<FleetCreatedResponse>> Create(CreateFleetRequest request)
        {
            return Ok(await _kubernetesService.CreateFleet(request));
        }

        [HttpPut]
        public async Task<ActionResult<FleetUpdatedResponse>> Update(UpdateFleetRequest request)
        {
            return Ok(await _kubernetesService.UpdateFleet(request));
        }

        [HttpDelete]
        public async Task<ActionResult<FleetDeletedResponse>> Delete(DeleteFleetRequest request)
        {
            return Ok(await _kubernetesService.DeleteFleet(request));
        }
    }
}
