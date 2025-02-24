using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using T217_Capstone_Project_API.Authentication;
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
        private readonly IUserRepository _userRepo;
        private readonly IProjectUserRepository _projectUserRepo;

        public ProjectsController(IProjectRepository repo, IUserRepository userRepo, IProjectUserRepository projectUserRepo)
        {
            _repo = repo;   
            _userRepo = userRepo;
            _projectUserRepo = projectUserRepo;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            string apiKey = Request.Headers["x-api-key"];

            var projects = await _repo.GetProjectListAsync(apiKey);

            if (!projects.Any())
            {
                return NotFound();
            }

            return projects;

        }

        [HttpGet("GetProjectsAdmin")]
        [ServiceFilter(typeof(UserAuthenticationFilterAdmin))]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsAdmin()
        {
            string apiKey = Request.Headers["x-api-key"];
            var projects = await _repo.GetProjectListAsync(apiKey);

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
            string apiKey = Request.Headers["x-api-key"];
            var project = await _repo.GetProjectAsync(id, apiKey);

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
            string apiKey = Request.Headers["x-api-key"];

            var newProject = await _repo.CreateProjectAsync(project, apiKey);

            return CreatedAtAction(nameof(GetProject), new { id = newProject.ProjectID }, newProject);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            string apiKey = Request.Headers["x-api-key"];

            int updateStatus = await _repo.UpdateProjectAsync(id, project, apiKey);

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
            string apiKey = Request.Headers["x-api-key"];
            var deleteStatus = await _repo.DeleteProjectAsync(id, apiKey);

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
