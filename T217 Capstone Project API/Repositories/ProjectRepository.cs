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
            NotFound
        }

        private readonly StakeholderRisksContext _context = new StakeholderRisksContext();

        public async Task<Project> GetProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            return project;
        }

        public async Task<List<Project>> GetProjectListAsync()
        {
            var projectList = await _context.Projects.OrderBy(x => x.ProjectID).ToListAsync();

            return projectList;
        }

        public async Task<List<Project>> GetProjectListByUserAsync(int id)
        {
            List<Project> projects = new List<Project>();

            var projectUsers = await _context.ProjectUsers.Where(x => x.UserID == id).OrderBy(x => x.ProjectUserID).ToListAsync();

            foreach (var projectUser in projectUsers)
            {
                if (projectUser.CanRead == true)
                {
                    var project = await _context.Projects.FindAsync(id);
                    if (project != null)
                    {
                        projects.Add(project);
                    }
                }
            }

            return projects;
        }

        public async Task<Project> CreateProjectAsync(ProjectDTO project)
        {
            Project newProject = new Project();

            newProject.ProjectName = project.ProjectName;
            newProject.Status = project.Status;

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }

        public async Task<int> UpdateProjectAsync(int id, Project project)
        {
            if (id != project.ProjectID)
            {
                return (int)UpdateStatus.BadRequest;
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

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Users.FindAsync(id);
            if (project == null)
            {
                return false;
            }

            _context.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }

        // Checks the database if a Project with the matching ID exists.
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectID == id);
        }
    }
}
