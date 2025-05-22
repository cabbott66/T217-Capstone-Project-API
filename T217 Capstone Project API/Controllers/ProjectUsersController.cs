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
    /// <summary>
    /// API endpoint controller that manages all endpoints related to ProjectUsers.
    /// </summary>
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
        /// <summary>
        /// Returns all the ProjectUsers associated with the supplied Project ID if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Project.</param>
        /// <returns>A list of all ProjectUsers for the associated Project, or a 404 Not Found status code.</returns>
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
        /// <summary>
        /// Updates the ProjectUser with the supplied ID with the new ProjectUser if the user has the required authorisation. 
        /// The ID must match the ProjectUser ID of the supplied ProjectUser.
        /// </summary>
        /// <param name="id">The ID of the ProjectUser to be updated.</param>
        /// <param name="projectUserPermissions">The new permissions to replace the existing ProjectUser.</param>
        /// <returns>A status code depending on the results of the update.</returns>
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
        /// <summary>
        /// Creates a new ProjectUser associated with the supplied User ID and Project ID, with the supplied permissions 
        /// if the user has the required authorisation. 
        /// </summary>
        /// <param name="projectID">The ID of the Project which the ProjectUser will be created for.</param>
        /// <param name="userID">The ID of the User which the ProjectUser will be created for.</param>
        /// <param name="projectUserPermissions">The permissions of the ProjectUser</param>
        /// <returns>Returns a 201 Created status message.</returns>
        [HttpPost]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<ProjectUser>> AddProjectUserToProject(int projectID, int userID, ProjectUserPermissionsDTO projectUserPermissions)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newProjectUser = await _repo.AddNewProjectUserAsync(projectID, userID, apiKey!, projectUserPermissions);

            return Created();
        }

        // DELETE: api/ProjectUsers/5
        /// <summary>
        /// Removes the ProjectUser with the matching ID from the database if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the ProjectUser to be deleted.</param>
        /// <returns>A status code depending on the results of the deletion.</returns>
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
