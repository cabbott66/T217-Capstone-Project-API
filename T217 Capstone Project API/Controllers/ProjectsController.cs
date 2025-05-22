using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using T217_Capstone_Project_API.Authentication;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO.ProjectDTOs;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T217_Capstone_Project_API.Controllers
{
    /// <summary>
    /// API endpoint controller that manages all endpoints related to Projects.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _repo;

        public ProjectsController(IProjectRepository repo)
        {
            _repo = repo;   
        }

        // GET: api/<ProjectsController>
        /// <summary>
        /// Gets a list of all the Projects that the user associated with the included API Key in the header has access to read.
        /// </summary>
        /// <returns>An list of the Projects the user has access to, or a 404 Not Found status code.</returns>
        [HttpGet]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var projects = await _repo.GetProjectListAsync(apiKey!);

            if (!projects.Any())
            {
                return NotFound();
            }

            return projects;

        }

        // GET api/<ProjectsController>/5
        /// <summary>
        /// Gets the Project with the associated ID, and returns it if it exists and the user associated with the included 
        /// API Key in the header has access to it.
        /// </summary>
        /// <param name="id">The ID of the desired Project.</param>
        /// <returns>The Project with the matching ID, or a 404 Not Found status code.</returns>
        [HttpGet("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var project = await _repo.GetProjectAsync(id, apiKey!);

            if (project.ProjectID == 0)
            {
                return NotFound();
            }

            return project;
        }

        // POST api/<ProjectsController>
        /// <summary>
        /// Creates a new Project and associated ProjectUser with the values supplied in the ProjectDTO.
        /// </summary>
        /// <param name="project"></param>
        /// <returns>The newly created Project.</returns>
        [HttpPost]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<Project>> PostProject(ProjectDTO project)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newProject = await _repo.CreateProjectAsync(project, apiKey!);

            return CreatedAtAction(nameof(GetProject), new { id = newProject.ProjectID }, newProject);
        }

        // PUT api/<ProjectsController>/5
        /// <summary>
        /// Updates the Project with the supplied ID with the new Project if the user has the required authorisation. 
        /// The ID must match the project ID of the supplied Project.
        /// </summary>
        /// <param name="id">The ID of the Project to be updated.</param>
        /// <param name="project">The new Project that will replace the existing one.</param>
        /// <returns>A status code depending on the results of the update.</returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            int updateStatus = await _repo.UpdateProjectAsync(id, project, apiKey!);

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

        // DELETE api/<ProjectsController>/5
        /// <summary>
        /// Removes the project with the matching ID from the database if the user has the required authorisation.
        /// </summary>
        /// <param name="id">The ID of the Project to be deleted.</param>
        /// <returns>A status code depending on the results of the deletion.</returns>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> DeleteProject(int id)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }
            var deleteStatus = await _repo.DeleteProjectAsync(id, apiKey!);

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
