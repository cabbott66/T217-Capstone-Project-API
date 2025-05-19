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
        [HttpPost]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<Project>> PostProject(ProjectDTO project)
        {
            if (!Request.Headers.TryGetValue("x-api-key", out var apiKey)) { return BadRequest(); }

            var newProject = await _repo.CreateProjectAsync(project, apiKey!);

            return CreatedAtAction(nameof(GetProject), new { id = newProject.ProjectID }, newProject);
        }

        // PUT api/<ProjectsController>/5
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
