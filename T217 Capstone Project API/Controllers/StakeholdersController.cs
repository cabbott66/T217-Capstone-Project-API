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

        [HttpGet("GetByProjectID")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<Stakeholder>>> GetStakeholderGroupsByProjectID(int projectId)
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
        [HttpPost]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<Stakeholder>> PostStakeholder(StakeholderDTO stakeholder)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newStakeholder = await _repo.CreateStakeholderAsync(stakeholder, apiKey!);

            return CreatedAtAction("GetStakeholder", new { id = newStakeholder.StakeholderID }, newStakeholder);
        }

        // DELETE: api/Stakeholders/5
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
