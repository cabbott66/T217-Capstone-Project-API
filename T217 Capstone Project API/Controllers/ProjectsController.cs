using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO;
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _repo.GetProjectListAsync();

            if (projects == null)
            {
                return NotFound();
            }

            return projects;
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _repo.GetProjectAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST api/<ProjectsController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Project>> PostProject(ProjectDTO project)
        {
            var newProject = await _repo.CreateProjectAsync(project);

            return CreatedAtAction(nameof(GetProject), new { id = newProject.ProjectID }, newProject);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            int updateStatus = await _repo.UpdateProjectAsync(id, project);

            if (id != project.ProjectID)
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

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var wasDeleted = await _repo.DeleteProjectAsync(id);

            if (!wasDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
