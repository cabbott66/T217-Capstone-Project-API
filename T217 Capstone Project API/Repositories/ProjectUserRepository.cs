using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.DTO.ProjectDTOs;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    /// <inheritdoc/>
    public class ProjectUserRepository : IProjectUserRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound,
            NotAuthorized
        }

        private readonly StakeholderRisksContext _context;

        /// <inheritdoc/>
        public ProjectUserRepository(StakeholderRisksContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<ProjectUser> GetProjectUserAsync(int id)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            return projectUser;
        }

        /// <inheritdoc/>
        public async Task<List<ProjectUser>> GetProjectUserListAsync()
        {
            var projectUserList = await _context.ProjectUsers.OrderBy(x => x.ProjectUserID).ToListAsync();

            return projectUserList;
        }

        /// <inheritdoc/>
        public async Task<List<ProjectUser>> GetProjectUserListByUserAsync(int id)
        {
            var projectUserList = await _context.ProjectUsers.Where(x => x.UserID == id).OrderBy(x => x.ProjectUserID).ToListAsync();

            return projectUserList;
        }

        /// <inheritdoc/>
        public async Task<List<ProjectUser>> GetProjectUserListByProjectAsync(int id, string apiKey)
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

            var user = await query.FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var projectUserList = await _context.ProjectUsers.Where(x => x.ProjectID == id).OrderBy(x => x.ProjectUserID).ToListAsync();

            return projectUserList;
        }

        /// <inheritdoc/>
        public async Task<ProjectUser> GetProjectUserByUserAndProjectAsync(int userId, int projectId)
        {
            var projectUser = await _context.ProjectUsers.Where(x => x.UserID == userId && x.ProjectID == projectId).FirstAsync();

            return projectUser;
        }

        /// <inheritdoc/>
        public async Task<ProjectUser> CreateProjectUserAsync(ProjectUser projectUser)
        {
            _context.ProjectUsers.Add(projectUser);
            await _context.SaveChangesAsync();
            
            return projectUser;
        }

        /// <inheritdoc/>
        public async Task<ProjectUser> AddNewProjectUserAsync(int projectID, int newUserID, string apiKey, ProjectUserPermissionsDTO projectUserPermissions)
        {
            var userId = await GetUserIdFromApiKey(apiKey);
            ProjectUser newProjectUser = new ProjectUser();

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        where projects.ProjectID == projectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                newProjectUser.ProjectUserID = -1;
                return newProjectUser;
            }

            var user = await _context.Users.Where(x => x.UserID == newUserID).FirstAsync();

            if (user == null)
            {
                newProjectUser.ProjectUserID = -2;
                return newProjectUser;
            }
            newProjectUser.UserID = newUserID;
            newProjectUser.ProjectID = projectID;
            newProjectUser.CanWrite = projectUserPermissions.CanWrite;
            newProjectUser.CanRead = projectUserPermissions.CanRead;
            newProjectUser.CanEdit = projectUserPermissions.CanEdit;
            newProjectUser.IsAdmin = false;

            _context.ProjectUsers.Add(newProjectUser);
            await _context.SaveChangesAsync();

            return newProjectUser;
        }

        /// <inheritdoc/>
        public async Task<int> UpdateProjectUserAsync(int id, string apiKey, ProjectUserPermissionsDTO projectUserPermissions)
        {
            var userId = await GetUserIdFromApiKey(apiKey);
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            if (projectUser == null) 
            {
                return (int)UpdateStatus.NotFound;
            }

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        where projects.ProjectID == projectUser.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var user = await query.FirstOrDefaultAsync();

            if (user == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            projectUser.CanWrite = projectUserPermissions.CanWrite;
            projectUser.CanRead = projectUserPermissions.CanRead;
            projectUser.CanEdit = projectUserPermissions.CanEdit;

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

        /// <inheritdoc/>
        public async Task<int> DeleteProjectUserAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotFound;
            }
            else if (projectUser.UserID == userId)
            {
                return (int)UpdateStatus.BadRequest;
            }

                var query = from projects in _context.Projects
                            join projectUsers in _context.ProjectUsers
                                on projects.ProjectID equals projectUsers.ProjectID
                            where projects.ProjectID == projectUser.ProjectID
                            where projectUsers.ProjectID == projects.ProjectID
                            where projectUsers.UserID == userId
                            where projectUsers.IsAdmin == true
                            select projectUsers;

            var user = await query.FirstOrDefaultAsync();

            if (user == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            _context.Remove(projectUser);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        // Checks the database if a User with the matching ID exists.
        private bool ProjectUserExists(int id)
        {
            return _context.ProjectUsers.Any(e => e.ProjectUserID == id);
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
