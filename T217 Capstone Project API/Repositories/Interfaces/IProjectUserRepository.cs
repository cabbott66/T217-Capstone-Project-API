using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    public interface IProjectUserRepository
    {
        // TODO: Add XML commenting.
        public abstract Task<ProjectUser> GetProjectUserAsync(int id);

        public abstract Task<List<ProjectUser>> GetProjectUserListAsync();

        public abstract Task<List<ProjectUser>> GetProjectUserListByUserAsync(int id);

        public abstract Task<List<ProjectUser>> GetProjectUserListByProjectAsync(int id);

        public abstract Task<ProjectUser> GetProjectUserByUserAndProjectAsync(int userId, int projectId);

        public abstract Task<ProjectUser> CreateProjectUserAsync(ProjectUser projectUser);

        public abstract Task<int> UpdateProjectUserAsync(int id, ProjectUser user);

        public abstract Task<bool> DeleteProjectUserAsync(int id);
    }
}
