using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API;
using T217_Capstone_Project_API.Authentication;
using T217_Capstone_Project_API.Models.DTO.ProjectDTOs;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Controllers
{
    /// <summary>
    /// API endpoint controller that manages all endpoints related to Stakeholders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StakeholdersController : ControllerBase
    {
        private readonly IStakeholderRepository _repo;

        public StakeholdersController(IStakeholderRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Stakeholders
        /// <summary>
        /// Gets a list of all the Stakeholders that the user associated with the included API Key in the header has access to read.
        /// </summary>
        /// <returns>An list of the Stakeholders the user has access to, or a 404 Not Found status code.</returns>
        [HttpGet]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<Stakeholder>>> GetStakeholders()
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var stakeholders = await _repo.GetStakeholderListAsync(apiKey!);

            if (!stakeholders.Any())
            {
                return NotFound();
            }

            return stakeholders;
        }

        // GET: api/Stakeholders/GetByProjectID
        /// <summary>
        /// Gets a list of Stakeholders associated with the ProjectID, and returns it if the user has the required authorisation. 
        /// </summary>
        /// <param name="projectId">The ID of the associated Project.</param>
        /// <returns>The list of Stakeholders associated with the ProjectID, or a 404 Not Found status code.</returns>
        [HttpGet("GetByProjectID")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<Stakeholder>>> GetStakeholderByProjectID(int projectId)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var stakeholderGroups = await _repo.GetStakeholderListByProjectAsync(projectId, apiKey!);

            if (!stakeholderGroups.Any())
            {
                return NotFound();
            }

            return stakeholderGroups;
        }

        // GET: api/Stakeholders/5
        /// <summary>
        /// Gets the Stakeholder with the associated ID, and returns it if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the desired Stakeholder.</param>
        /// <returns>The Stakeholder with the matching ID, or a 404 Not Found status code.</returns>
        [HttpGet("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<Stakeholder>> GetStakeholder(int id)
        {
            if(!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var stakeholder = await _repo.GetStakeholderAsync(id, apiKey!);

            if (stakeholder == null)
            {
                return NotFound();
            }

            return stakeholder;
        }

        // PUT: api/Stakeholders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Updates the Stakeholder with the supplied ID with the new Stakeholder if the user has the required authorisation. 
        /// The ID must match the StakeholderID of the supplied Stakeholder.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder to be updated.</param>
        /// <param name="stakeholder">The new Stakeholder that will replace the existing one.</param>
        /// <returns>A status code depending on the results of the update.</returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutStakeholder(int id, Stakeholder stakeholder)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            int updateStatus = await _repo.UpdateStakeholderAsync(id, stakeholder, apiKey!);

            if (id != stakeholder.StakeholderID)
            {
                return BadRequest();
            }

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

        // POST: api/Stakeholders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Creates a new Stakeholder with the values supplied in the StakeholderDTO.
        /// </summary>
        /// <param name="stakeholder">The StakeholderDTO to be created.</param>
        /// <returns>The newly created Stakeholder.</returns>
        [HttpPost]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<Stakeholder>> PostStakeholder(StakeholderDTO stakeholder)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newStakeholder = await _repo.CreateStakeholderAsync(stakeholder, apiKey!);

            return CreatedAtAction("GetStakeholder", new { id = newStakeholder.StakeholderID }, newStakeholder);
        }

        // DELETE: api/Stakeholders/5
        /// <summary>
        /// Removes the Stakeholder with the matching ID from the database if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder to be deleted.</param>
        /// <returns>A status code depending on the results of the deletion.</returns>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> DeleteStakeholder(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var deleteStatus = await _repo.DeleteStakeholderAsync(id, apiKey!);

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
