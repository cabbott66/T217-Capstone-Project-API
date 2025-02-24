using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound,
            NotAuthorized
        }

        private readonly StakeholderRisksContext _context = new StakeholderRisksContext();

        public async Task<Project> GetProjectAsync(int id, string apiKey)
        {
            
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from project in _context.Projects
                        join projectUser in _context.ProjectUsers
                            on project.ProjectID equals projectUser.ProjectID
                        where project.ProjectID == id
                        where projectUser.UserID == userId
                        where projectUser.CanRead == true
                        select project;

            var selectedProject = query.FirstOrDefault();

            if (selectedProject != null)
            {
                return selectedProject;
            }
            
            return new Project();
        }

        public async Task<List<Project>> GetProjectListAsync(string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var projectUsers = await _context.ProjectUsers.Where(x => x.UserID == userId && x.CanRead == true).ToListAsync();

            var query = from project in _context.Projects
                        join projectUser in _context.ProjectUsers
                            on project.ProjectID equals projectUser.ProjectID
                            where projectUser.UserID == userId
                            where projectUser.CanRead == true
                        select project;

            var projects = await query.OrderBy(x => x.ProjectID).ToListAsync();

            return projects;
        }

        //public async Task<List<Project>> GetProjectListUserReadAccessAsync(int id)
        //{
        //    List<Project> projects = new List<Project>();

        //    var projectUsers = await _context.ProjectUsers.Where(x => x.UserID == id).OrderBy(x => x.ProjectUserID).ToListAsync();

        //    foreach (var projectUser in projectUsers)
        //    {
        //        if (projectUser.CanRead == true || projectUser.IsAdmin == true)
        //        {
        //            var project = await _context.Projects.FindAsync(id);
        //            if (project != null)
        //            {
        //                projects.Add(project);
        //            }
        //        }
        //    }

        //    return projects;
        //}

        //public async Task<List<Project>> GetProjectListByBatchIdsAsync(List<int> ids)
        //{
        //    List<Project> projectList = new List<Project>();

        //    foreach (var id in ids)
        //    {
        //        var project = await _context.Projects.FindAsync(id);
        //        if (project != null)
        //        {
        //            projectList.Add(project);
        //        }
        //    }

        //    projectList = projectList.OrderBy(x => x.ProjectID).ToList();
        //    return projectList;
        //}

        public async Task<Project> CreateProjectAsync(ProjectDTO project, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);
            Project newProject = new Project();

            newProject.ProjectName = project.ProjectName;
            newProject.Status = project.Status;

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            ProjectUser newProjectUser = new ProjectUser(userId, newProject.ProjectID, true, true, true, true);

            _context.ProjectUsers.Add(newProjectUser);
            await _context.SaveChangesAsync();

            return newProject;
        }

        public async Task<int> UpdateProjectAsync(int id, Project project, string apiKey)
        {
            if (id != project.ProjectID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var projectUser = await _context.ProjectUsers.Where(x => x.ProjectID == id && x.UserID == userId && x.CanEdit == true).FirstOrDefaultAsync();

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

        public async Task<int> DeleteProjectAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var projectUser = await _context.ProjectUsers.Where(x => x.ProjectID == id && x.UserID == userId && x.IsAdmin == true).FirstOrDefaultAsync();

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
