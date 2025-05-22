using Microsoft.AspNetCore.Mvc;
using T217_Capstone_Project_API.Authentication;
using T217_Capstone_Project_API.Models.DTO.RisksDTOs;
using T217_Capstone_Project_API.Models.Risks;
using T217_Capstone_Project_API.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T217_Capstone_Project_API.Controllers
{
    /// <summary>
    /// API endpoint controller that manages all endpoints related to the Risks.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly IRisksRepository _repo;

        public RisksController(IRisksRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<RisksController>/GetEnvironmentalRisk/5
        /// <summary>
        /// Gets the EnvironmentalRisks with the matching ID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisks.</param>
        /// <returns>The EnvironmentalRisks with the matching ID, or a 404 Not Found status code.</returns>
        [HttpGet("GetEnvironmentalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisks>> GetEnvironmentalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var envRisk = await _repo.GetEnvironmentalRiskAsync(id, apiKey!);

            if (envRisk.EnvironmentalRiskID == 0)
            {
                return NotFound();
            }

            return envRisk;
        }

        // GET: api/<RisksController>/GetInterpersonalRisk/5
        /// <summary>
        /// Gets the InterpersonalRisks with the matching ID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisks.</param>
        /// <returns>The InterpersonalRisks with the matching ID, or a 404 Not Found status code.</returns>
        [HttpGet("GetInterpersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<InterpersonalRisks>> GetInterpersonalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var interRisk = await _repo.GetInterpersonalRiskAsync(id, apiKey!);

            if (interRisk.InterpersonalRiskID == 0)
            {
                return NotFound();
            }

            return interRisk;
        }

        // GET: api/<RisksController>/GetPersonalRisk/5
        /// <summary>
        /// Gets the PersonalRisks with the matching ID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisks.</param>
        /// <returns>The PersonalRisks with the matching ID, or a 404 Not Found status code.</returns>
        [HttpGet("GetPersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<PersonalRisks>> GetPersonalRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var personRisk = await _repo.GetPersonalRiskAsync(id, apiKey!);

            if (personRisk.PersonalRiskID == 0)
            {
                return NotFound();
            }

            return personRisk;
        }

        // GET: api/<RisksController>/GetProjectRisk/5
        /// <summary>
        /// Gets the ProjectRisks with the matching ID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisks.</param>
        /// <returns>The ProjectRisks with the matching ID, or a 404 Not Found status code.</returns>
        [HttpGet("GetProjectRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<ProjectRisks>> GetProjectRisk(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var projectRisk = await _repo.GetProjectRiskAsync(id, apiKey!);

            if (projectRisk.ProjectRiskID == 0)
            {
                return NotFound();
            }

            return projectRisk;
        }

        // GET: api/<RisksController>/GetStakeholderRisksByStakeholder/5
        /// <summary>
        /// Gets the EnvironmentalRisks, InterpersonalRisks, and PersonalRisks associated with the supplied StakeholderID 
        /// if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder</param>
        /// <returns>The EnvironmentalRisks, InterpersonalRisks, and PersonalRisks associated with the StakeholderID, or a 404 Not Found status code.</returns>
        [HttpGet("GetStakeholderRisksByStakeholder/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<StakeholderRisksDTO>> GetStakeholderRisksByStakeholder(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var envRisk = await _repo.GetEnvironmentalRiskFromStakeholderAsync(id, apiKey!);
            var interRisk = await _repo.GetInterpersonalRiskFromStakeholderAsync(id, apiKey!);
            var personRisk = await _repo.GetPersonalRiskFromStakeholderAsync(id, apiKey!);

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

        // GET: api/<RisksController>/GetEnvironmentalRiskByStakeholder/5
        /// <summary>
        /// Gets the EnvironmentalRisks associated with the suppled StakeholderID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder</param>
        /// <returns>The EnvironmentalRisks associated with the Stakeholder, or a 404 Not Found status code.</returns>
        [HttpGet("GetEnvironmentalRiskByStakeholder/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisks>> GetEnvironmentalRiskByStakeholder(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var envRisk = await _repo.GetEnvironmentalRiskFromStakeholderAsync(id, apiKey!);

            if (envRisk.EnvironmentalRiskID == 0)
            { 
                return NotFound(); 
            }

            return envRisk;
        }

        // GET: api/<RisksController>/GetInterpersonalRiskByStakeholder/5
        /// <summary>
        /// Gets the InterpersonalRisks associated with the suppled StakeholderID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder</param>
        /// <returns>The InterpersonalRisks associated with the Stakeholder, or a 404 Not Found status code.</returns>
        [HttpGet("GetInterpersonalRiskByStakeholder/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<InterpersonalRisks>> GetInterpersonalRiskByStakeholder(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var interRisk = await _repo.GetInterpersonalRiskFromStakeholderAsync(id, apiKey!);

            if (interRisk.InterpersonalRiskID == 0)
            {
                return NotFound();
            }

            return interRisk;
        }

        // GET: api/<RisksController>/GetPersonalRiskByStakeholder/5
        /// <summary>
        /// Gets the PersonalRisks associated with the suppled StakeholderID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder</param>
        /// <returns>The PersonalRisks associated with the Stakeholder, or a 404 Not Found status code.</returns>
        [HttpGet("GetPersonalRiskByStakeholder/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<PersonalRisks>> GetPersonalRiskByStakeholder(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var personRisk = await _repo.GetPersonalRiskFromStakeholderAsync(id, apiKey!);

            if (personRisk.PersonalRiskID == 0)
            {
                return NotFound();
            }

            return personRisk;
        }

        // GET: api/<RisksController>/GetProjectRisksByProjectID/5
        /// <summary>
        /// Gets the ProjectRisks associated with the suppled ProjectID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Project</param>
        /// <returns>The ProjectRisks associated with the Project, or a 404 Not Found status code.</returns>
        [HttpGet("GetProjectRisksByProjectID/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<ProjectRisks>> GetProjectRisksByProjectID(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var projectRisk = await _repo.GetProjectRiskFromProjectAsync(id, apiKey!);

            if (projectRisk.ProjectRiskID == 0)
            {
                return NotFound();
            }

            return projectRisk;
        }

        // POST api/<RisksController>/PostEnvironmentalRisk
        /// <summary>
        /// Creates a new EnvironmentalRisks with values supplied in the EnvironmentalRisksDTO if the user has the required authorisation. 
        /// </summary>
        /// <param name="envRisk">The EnvironmentalRisksDTO that will be used to create the EnvironmentalRisks</param>
        /// <returns>The newly created EnvironmentalRisks</returns>
        [HttpPost("PostEnvironmentalRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<EnvironmentalRisks>> PostEnvironmentalRisk(EnvironmentalRisksDTO envRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newEnvRisk = await _repo.CreateEnvironmentalRiskAsync(envRisk, apiKey!);

            return CreatedAtAction(nameof(GetEnvironmentalRisk), new { id = newEnvRisk.EnvironmentalRiskID }, newEnvRisk);
        }

        // POST api/<RisksController>/PostInterpersonalRisk
        /// <summary>
        /// Creates a new InterpersonalRisks with values supplied in the InterpersonalRisksDTO if the user has the required authorisation. 
        /// </summary>
        /// <param name="interRisk">The InterpersonalRisksDTO that will be used to create the InterpersonalRisks</param>
        /// <returns>The newly created InterpersonalRisks</returns>
        [HttpPost("PostInterpersonalRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<InterpersonalRisks>> PostInterpersonalRisk(InterpersonalRisksDTO interRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newInterRisk = await _repo.CreateInterpersonalRiskAsync(interRisk, apiKey!);

            return CreatedAtAction(nameof(GetInterpersonalRisk), new { id = newInterRisk.InterpersonalRiskID }, newInterRisk);
        }

        // POST api/<RisksController>/PostPersonalRisk
        /// <summary>
        /// Creates a new PersonalRisks with values supplied in the PersonalRisksDTO if the user has the required authorisation. 
        /// </summary>
        /// <param name="personRisk">The PersonalRisksDTO that will be used to create the PersonalRisks</param>
        /// <returns>The newly created PersonalRisks</returns>
        [HttpPost("PostPersonalRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<PersonalRisks>> PostPersonalRisk(PersonalRisksDTO personRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newPersonalRisk = await _repo.CreatePersonalRiskAsync(personRisk, apiKey!);

            return CreatedAtAction(nameof(GetPersonalRisk), new { id = newPersonalRisk.PersonalRiskID }, newPersonalRisk);
        }

        // POST api/<RisksController>/PostProjectRisk
        /// <summary>
        /// Creates a new ProjectRisks with values supplied in the ProjectRisksDTO if the user has the required authorisation. 
        /// </summary>
        /// <param name="projectRisk">The ProjectRisksDTO that will be used to create the ProjectRisks</param>
        /// <returns>The newly created ProjectRisks</returns>
        [HttpPost("PostProjectRisk")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<ProjectRisks>> PostProjectRisk(ProjectRisksDTO projectRisk)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newProjectRisk = await _repo.CreateProjectRiskAsync(projectRisk, apiKey!);

            return CreatedAtAction(nameof(GetPersonalRisk), new { id = newProjectRisk.ProjectRiskID }, newProjectRisk);
        }

        // POST api/<RisksController>/PutEnvironmentalRisk/5
        /// <summary>
        /// Updates the EnvironmentalRisks with the supplied ID with the new EnvironmentalRisks if the user has the required authorisation. 
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisks that will be replaced.</param>
        /// <param name="environmentalRisk">The new EnvironmentalRisks that will replace the existing one</param>
        /// <returns>A status code depending on the results of the update.</returns>
        [HttpPut("PutEnvironmentalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutEnvironmentalRisk(int id, EnvironmentalRisks environmentalRisk)
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

        // POST api/<RisksController>/PutInterpersonalRisk/5
        /// <summary>
        /// Updates the InterpersonalRisks with the supplied ID with the new InterpersonalRisks if the user has the required authorisation. 
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisks that will be replaced.</param>
        /// <param name="interpersonalRisk">The new InterpersonalRisks that will replace the existing one</param>
        /// <returns>A status code depending on the results of the update.</returns>
        [HttpPut("PutInterpersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutInterpersonalRisk(int id, InterpersonalRisks interpersonalRisk)
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

        // POST api/<RisksController>/PutPersonalRisk/5
        /// <summary>
        /// Updates the PersonalRisks with the supplied ID with the new PersonalRisks if the user has the required authorisation. 
        /// </summary>
        /// <param name="id">The ID of the PersonalRisks that will be replaced.</param>
        /// <param name="personalRisk">The new PersonalRisks that will replace the existing one</param>
        /// <returns>A status code depending on the results of the update.</returns>
        [HttpPut("PutPersonalRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutPersonalRisk(int id, PersonalRisks personalRisk)
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

        // POST api/<RisksController>/PutProjectRisk/5
        /// <summary>
        /// Updates the PutProjectRisk with the supplied ID with the new PutProjectRisk if the user has the required authorisation. 
        /// </summary>
        /// <param name="id">The ID of the PutProjectRisk that will be replaced.</param>
        /// <param name="projectRisk">The new PutProjectRisk that will replace the existing one</param>
        /// <returns>A status code depending on the results of the update.</returns>
        [HttpPut("PutProjectRisk/{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> PutProjectRisk(int id, ProjectRisks projectRisk)
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

        // DELETE api/<RisksController>/DeleteEnvironmentalRisk/5
        /// <summary>
        /// Removes the EnvironmentalRisks with the matching ID from the database if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisks to be deleted.</param>
        /// <returns>A status code depending on the results of the deletion.</returns>
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

        // DELETE api/<RisksController>/DeleteInterpersonalRisk/5
        /// <summary>
        /// Removes the InterpersonalRisks with the matching ID from the database if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisks to be deleted.</param>
        /// <returns>A status code depending on the results of the deletion.</returns>
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

        // DELETE api/<RisksController>/DeletePersonalRisk/5
        /// <summary>
        /// Removes the PersonalRisks with the matching ID from the database if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisks to be deleted.</param>
        /// <returns>A status code depending on the results of the deletion.</returns>
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

        // DELETE api/<RisksController>/DeleteProjectRisk/5
        /// <summary>
        /// Removes the ProjectRisks with the matching ID from the database if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisks to be deleted.</param>
        /// <returns>A status code depending on the results of the deletion.</returns>
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
