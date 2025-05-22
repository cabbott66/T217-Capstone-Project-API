using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO.ProjectDTOs;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    /// <inheritdoc/>
    public class ProjectRepository : IProjectRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound,
            NotAuthorized
        }

        private readonly StakeholderRisksContext _context;

        public ProjectRepository(StakeholderRisksContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Project> GetProjectAsync(int id, string apiKey)
        {
            
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        where projects.ProjectID == id
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select projects;

            var selectedProject = query.FirstOrDefault();

            if (selectedProject != null)
            {
                return selectedProject;
            }
            
            return new Project();
        }

        /// <inheritdoc/>
        public async Task<List<Project>> GetProjectListAsync(string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select projects;

            var projectsList = await query.OrderBy(x => x.ProjectID).ToListAsync();

            return projectsList;
        }


        /// <inheritdoc/>
        public async Task<Project> CreateProjectAsync(ProjectDTO project, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);
            Project newProject = new Project();

            newProject.ProjectName = project.ProjectName;
            newProject.ProjectDescription = project.ProjectDescription;
            newProject.Status = project.Status;

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            ProjectUser newProjectUser = new ProjectUser(userId, newProject.ProjectID, true, true, true, true);

            _context.ProjectUsers.Add(newProjectUser);
            await _context.SaveChangesAsync();

            return newProject;
        }

        /// <inheritdoc/>
        public async Task<int> UpdateProjectAsync(int id, Project project, string apiKey)
        {
            if (id != project.ProjectID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        where projects.ProjectID == id
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanEdit == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            project.EditDateTime = DateTime.Now;

            _context.Projects.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return (int)UpdateStatus.NotFound;
                }
                else
                {
                    throw;
                }
            }
            return (int)UpdateStatus.Success;
        }

        /// <inheritdoc/>
        public async Task<int> DeleteProjectAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        where projects.ProjectID == id
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return (int)UpdateStatus.NotFound;
            }

            _context.Remove(project);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        // Checks the database if a Project with the matching ID exists.
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectID == id);
        }

        // Finds the User with the matching API key and returns their UserID.
        private async Task<int> GetUserIdFromApiKey(string apiKey)
        {
            var user = await _context.Users.Where(x => x.ApiKey == apiKey).FirstAsync();

            if (user == null)
            {
                return -1;
            }
            else
            {
                return user.UserID;
            }
        }
    }
}
