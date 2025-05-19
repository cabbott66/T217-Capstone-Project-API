using System;
using System.Collections.Generic;
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
    public class ProjectUsersController : ControllerBase
    {
        private readonly IProjectUserRepository _repo;

        public ProjectUsersController(IProjectUserRepository repo)
        {
            _repo = repo;
        }

        // GET: api/ProjectUsers/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<ProjectUser>>> GetProjectUsersByProjectID(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var projectUsers = await _repo.GetProjectUserListByProjectAsync(id, apiKey!);

            if (!projectUsers.Any())
            {
                return NotFound();
            }

            return projectUsers;
        }

        // PUT: api/ProjectUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutProjectUser(int id, ProjectUserPermissionsDTO projectUserPermissions)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var updateStatus = await _repo.UpdateProjectUserAsync(id, apiKey!, projectUserPermissions);

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

        // POST: api/ProjectUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<ProjectUser>> AddProjectUserToProject(int projectID, int userID, ProjectUserPermissionsDTO projectUserPermissions)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newProjectUser = await _repo.AddNewProjectUserAsync(projectID, userID, apiKey!, projectUserPermissions);

            return Created();
        }

        // DELETE: api/ProjectUsers/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> DeleteProjectUser(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var deleteStatus = await _repo.DeleteProjectUserAsync(id, apiKey!);

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
