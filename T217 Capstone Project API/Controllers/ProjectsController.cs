using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [ServiceFilter(typeof(UserAuthenticationFilterAdmin))]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _repo.GetProjectListAsync();

            if (projects == null)
            {
                return NotFound();
            }

            return projects;
        }

        [HttpGet("GetProjectsForUser")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsForUser()
        {
            int userId = await GetCurrentUserID();
            var projects = await _repo.GetProjectListByUserAsync(userId);

            if (projects == null)
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
            var project = await _repo.GetProjectAsync(id);

            if (project == null)
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
            int currentUserId = await GetCurrentUserID();
            if (currentUserId == 0)
            {
                return BadRequest();
            }

            var newProject = await _repo.CreateProjectAsync(project);

            ProjectUser projectUser = new ProjectUser();
            projectUser.UserID = currentUserId;
            projectUser.ProjectID = newProject.ProjectID;
            projectUser.CanRead = true;
            projectUser.CanWrite = true;
            projectUser.CanEdit = true;
            projectUser.IsAdmin = true;

            var newProjectUser = await _projectUserRepo.CreateProjectUserAsync(projectUser);

            return CreatedAtAction(nameof(GetProject), new { id = newProject.ProjectID }, newProject);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UserAuthenticationFilter))]
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
        [ServiceFilter(typeof(UserAuthenticationFilter))]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var wasDeleted = await _repo.DeleteProjectAsync(id);

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
