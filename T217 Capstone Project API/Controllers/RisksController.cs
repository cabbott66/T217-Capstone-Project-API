using Microsoft.AspNetCore.Mvc;
using T217_Capstone_Project_API.Authentication;
using T217_Capstone_Project_API.Models.DTO.RisksDTOs;
using T217_Capstone_Project_API.Models.Risks;
using T217_Capstone_Project_API.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T217_Capstone_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly IRisksRepository _repo;

        public RisksController(IRisksRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<RisksController>
        [HttpGet("GetStakeholderRisks/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<StakeholderRisksDTO>> GetStakeholderRisks(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var envRisk = await _repo.GetEnvironmentalRiskFromStakeholderAsync(id, apiKey!);
            var interRisk = await _repo.GetInterpersonalRiskAsync(id, apiKey!);
            var personRisk = await _repo.GetPersonalRiskAsync(id, apiKey!);

            if (envRisk.EnvironmentalRiskID == 0 && interRisk.InterpersonalRiskID == 0 && personRisk.PersonalRiskID == 0)
            {
                return NotFound();
            }

            StakeholderRisksDTO stakeholderRisks = new StakeholderRisksDTO();
            stakeholderRisks.EnvironmentalRisk = envRisk;
            stakeholderRisks.InterpersonalRisk = interRisk;
            stakeholderRisks.PersonalRisk = personRisk;

            return stakeholderRisks;
        }

        // GET: api/<RisksController>
        [HttpGet("GetEnvironmentalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisk>> GetEnvironmentalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var envRisk = await _repo.GetEnvironmentalRiskFromStakeholderAsync(id, apiKey!);

            if (envRisk.EnvironmentalRiskID == 0)
            { 
                return NotFound(); 
            }

            return envRisk;
        }

        [HttpGet("GetPersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<PersonalRisk>> GetPersonalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var personRisk = await _repo.GetPersonalRiskAsync(id, apiKey!);

            if (personRisk.PersonalRiskID == 0)
            {
                return NotFound();
            }

            return personRisk;
        }

        [HttpGet("GetProjectRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<ProjectRisk>> GetProjectRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var projectRisk = await _repo.GetProjectRiskAsync(id, apiKey!);

            if (projectRisk.ProjectRiskID == 0)
            {
                return NotFound();
            }

            return projectRisk;
        }

        [HttpGet("GetInterpersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<InterpersonalRisk>> GetInterpersonalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var interRisk = await _repo.GetInterpersonalRiskAsync(id, apiKey!);

            if (interRisk.InterpersonalRiskID == 0)
            {
                return NotFound();
            }

            return interRisk;
        }

        // POST api/<RisksController>
        [HttpPost("PostEnvironmentalRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisk>> PostEnvironmentalRisk(EnvironmentalRiskDTO envRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newEnvRisk = await _repo.CreateEnvironmentalRiskAsync(envRisk, apiKey!);

            return CreatedAtAction(nameof(GetEnvironmentalRisk), new { id = newEnvRisk.EnvironmentalRiskID }, newEnvRisk);
        }

        // POST api/<RisksController>
        [HttpPost("PostInterpersonalRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisk>> PostInterpersonalRisk(InterpersonalRiskDTO interRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newInterRisk = await _repo.CreateInterpersonalRiskAsync(interRisk, apiKey!);

            return CreatedAtAction(nameof(GetInterpersonalRisk), new { id = newInterRisk.InterpersonalRiskID }, newInterRisk);
        }

        // POST api/<RisksController>
        [HttpPost("PostPersonalRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisk>> PostPersonalRisk(PersonalRiskDTO personRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newPersonalRisk = await _repo.CreatePersonalRiskAsync(personRisk, apiKey!);

            return CreatedAtAction(nameof(GetInterpersonalRisk), new { id = newPersonalRisk.PersonalRiskID }, newPersonalRisk);
        }

        // POST api/<RisksController>
        [HttpPost("PostProjectRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisk>> PostProjectRisk(ProjectRiskDTO projectRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newProjectRisk = await _repo.CreateProjectRiskAsync(projectRisk, apiKey!);

            return CreatedAtAction(nameof(GetInterpersonalRisk), new { id = newProjectRisk.ProjectRiskID }, newProjectRisk);
        }

        // PUT api/<RisksController>/5
        [HttpPut("PutEnvironmentalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutEnvironmentalRisk(int id, EnvironmentalRisk environmentalRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            int updateStatus = await _repo.UpdateEnvironmentalRiskAsync(id, environmentalRisk, apiKey!);

            switch (updateStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }

        // PUT api/<RisksController>/5
        [HttpPut("PutInterpersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutInterpersonalRisk(int id, InterpersonalRisk interpersonalRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            int updateStatus = await _repo.UpdateInterpersonalRiskAsync(id, interpersonalRisk, apiKey!);

            switch (updateStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }

        // PUT api/<RisksController>/5
        [HttpPut("PutPersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutPersonalRisk(int id, PersonalRisk personalRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            int updateStatus = await _repo.UpdatePersonalRiskAsync(id, personalRisk, apiKey!);

            switch (updateStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }

        // PUT api/<RisksController>/5
        [HttpPut("PutProjectRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> PutProjectRisk(int id, ProjectRisk projectRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            int updateStatus = await _repo.UpdateProjectRiskAsync(id, projectRisk, apiKey!);

            switch (updateStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }

        // DELETE api/<RisksController>/5
        [HttpDelete("DeleteEnvironmentalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> DeleteEnvironmentalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var deleteStatus = await _repo.DeleteEnvironmentalRiskAsync(id, apiKey!);

            switch (deleteStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }

        // DELETE api/<RisksController>/5
        [HttpDelete("DeleteInterpersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> DeleteInterpersonalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var deleteStatus = await _repo.DeleteInterpersonalRiskAsync(id, apiKey!);

            switch (deleteStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }

        // DELETE api/<RisksController>/5
        [HttpDelete("DeletePersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> DeletePersonalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var deleteStatus = await _repo.DeletePersonalRiskAsync(id, apiKey!);

            switch (deleteStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }

        // DELETE api/<RisksController>/5
        [HttpDelete("DeleteProjectRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> DeleteProjectRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var deleteStatus = await _repo.DeleteProjectRiskAsync(id, apiKey!);

            switch (deleteStatus)
            {
                case 0:
                    return NoContent();
                case 1:
                    return BadRequest();
                case 2:
                    return NotFound();
                case 3:
                    return StatusCode(StatusCodes.Status403Forbidden);
                default:
                    return BadRequest();
            }
        }
    }
}
