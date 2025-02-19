using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API;
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
        private readonly IUserRepository _userRepo;
        private readonly IProjectUserRepository _projectUserRepo;

        public StakeholderGroupsController(IStakeholderGroupRepository repo, IUserRepository userRepo, IProjectUserRepository projectUserRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _projectUserRepo = projectUserRepo;
        }

        // GET: api/StakeholderGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StakeholderGroup>>> GetStakeholderGroups()
        {
            int userId = await GetCurrentUserID();
            var projectUsers = await _projectUserRepo.GetProjectUserListByUserAsync(userId);
            List<int> projectIds = new List<int>();

            foreach (var projectUser in projectUsers)
            {
                if (projectUser.CanRead == true || projectUser.IsAdmin == true)
                {
                    projectIds.Add(projectUser.ProjectID);
                }
            }

            var stakeholderGroups = await _repo.GetStakeholderGroupListByBatchProjectId(projectIds);

            if (!stakeholderGroups.Any())
            {
                return NotFound();
            }

            return stakeholderGroups;
        }

        // GET: api/StakeholderGroups
        [HttpGet("GetStakeholderGroupsAdmin")]
        public async Task<ActionResult<IEnumerable<StakeholderGroup>>> GetStakeholderGroupsAdmin()
        {
            var stakeholderGroups = await _repo.GetStakeholderGroupListAsync();

            if (!stakeholderGroups.Any())
            {
                return NotFound();
            }

            return stakeholderGroups;
        }

        // GET: api/StakeholderGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StakeholderGroup>> GetStakeholderGroup(int id)
        {
            var stakeholderGroup = await _repo.GetStakeholderGroupAsync(id);

            if (stakeholderGroup == null)
            {
                return NotFound();
            }

            return stakeholderGroup;
        }

        // PUT: api/StakeholderGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStakeholderGroup(int id, StakeholderGroup stakeholderGroup)
        {
            int updateStatus = await _repo.UpdateStakeholderGroupAsync(id, stakeholderGroup);

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
                default:
                    return BadRequest();
            }
        }

        // POST: api/StakeholderGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StakeholderGroup>> PostStakeholderGroup(StakeholderGroupDTO stakeholderGroup)
        {
            var newStakeholderGroup = await _repo.CreateStakeholderGroupAsync(stakeholderGroup);

            return CreatedAtAction("GetStakeholderGroup", new { id = newStakeholderGroup.StakeholderGroupID }, newStakeholderGroup);
        }

        // DELETE: api/StakeholderGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStakeholderGroup(int id)
        {
            var wasDeleted = await _repo.DeleteStakeholderGroupAsync(id);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<int> GetCurrentUserID()
        {
            string apiKey = Request.Headers["x-api-key"];
            if (apiKey == null)
            {
                return 0;
            }
            var user = await _userRepo.GetUserByApiKeyAsync(apiKey);
            return user.UserID;
        }
    }
}
