using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    public class ProjectUserRepository : IProjectUserRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound
        }

        private readonly StakeholderRisksContext _context;

        public ProjectUserRepository(StakeholderRisksContext context)
        {
            _context = context;
        }

        public async Task<ProjectUser> GetProjectUserAsync(int id)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            return projectUser;
        }

        public async Task<List<ProjectUser>> GetProjectUserListAsync()
        {
            var projectUserList = await _context.ProjectUsers.OrderBy(x => x.ProjectUserID).ToListAsync();

            return projectUserList;
        }

        public async Task<List<ProjectUser>> GetProjectUserListByUserAsync(int id)
        {
            var projectUserList = await _context.ProjectUsers.Where(x => x.UserID == id).OrderBy(x => x.ProjectUserID).ToListAsync();

            return projectUserList;
        }

        public async Task<List<ProjectUser>> GetProjectUserListByProjectAsync(int id)
        {
            var projectUserList = await _context.ProjectUsers.Where(x => x.ProjectID == id).OrderBy(x => x.ProjectUserID).ToListAsync();

            return projectUserList;
        }

        public async Task<ProjectUser> GetProjectUserByUserAndProjectAsync(int userId, int projectId)
        {
            var projectUser = await _context.ProjectUsers.Where(x => x.UserID == userId && x.ProjectID == projectId).FirstAsync();

            return projectUser;
        }

        public async Task<ProjectUser> CreateProjectUserAsync(ProjectUser projectUser)
        {
            _context.ProjectUsers.Add(projectUser);
            await _context.SaveChangesAsync();
            
            return projectUser;
        }

        public async Task<int> UpdateProjectUserAsync(int id, ProjectUser projectUser)
        {
            if (id != projectUser.ProjectUserID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            _context.ProjectUsers.Entry(projectUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectUserExists(id))
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

        public async Task<bool> DeleteProjectUserAsync(int id)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);
            if (projectUser == null)
            {
                return false;
            }

            _context.Remove(projectUser);
            await _context.SaveChangesAsync();

            return true;
        }

        // Checks the database if a User with the matching ID exists.
        private bool ProjectUserExists(int id)
        {
            return _context.ProjectUsers.Any(e => e.ProjectUserID == id);
        }
    }
}
