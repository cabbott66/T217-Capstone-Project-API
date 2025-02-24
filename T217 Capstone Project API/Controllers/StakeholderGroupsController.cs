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
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StakeholderGroupsController : ControllerBase
    {
        private readonly IStakeholderGroupRepository _repo;

        public StakeholderGroupsController(IStakeholderGroupRepository repo)
        {
            _repo = repo;
        }

        // GET: api/StakeholderGroups
        [HttpGet]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<StakeholderGroup>>> GetStakeholderGroups()
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var stakeholderGroups = await _repo.GetStakeholderGroupListAsync(apiKey!);

            if (!stakeholderGroups.Any())
            {
                return NotFound();
            }

            return stakeholderGroups;
        }

        // GET: api/StakeholderGroups/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<StakeholderGroup>> GetStakeholderGroup(int id)
        {
            if(!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var stakeholderGroup = await _repo.GetStakeholderGroupAsync(id, apiKey!);

            if (stakeholderGroup == null)
            {
                return NotFound();
            }

            return stakeholderGroup;
        }

        // PUT: api/StakeholderGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutStakeholderGroup(int id, StakeholderGroup stakeholderGroup)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            int updateStatus = await _repo.UpdateStakeholderGroupAsync(id, stakeholderGroup, apiKey!);

            if (id != stakeholderGroup.StakeholderGroupID)
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

        // POST: api/StakeholderGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<StakeholderGroup>> PostStakeholderGroup(StakeholderGroupDTO stakeholderGroup)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newStakeholderGroup = await _repo.CreateStakeholderGroupAsync(stakeholderGroup, apiKey!);

            return CreatedAtAction("GetStakeholderGroup", new { id = newStakeholderGroup.StakeholderGroupID }, newStakeholderGroup);
        }

        // DELETE: api/StakeholderGroups/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> DeleteStakeholderGroup(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var deleteStatus = await _repo.DeleteStakeholderGroupAsync(id, apiKey!);

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
