using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Returns the Project from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired Project.</param>
        /// <returns></returns>
        public abstract Task<Project> GetProjectAsync(int id);

        /// <summary>
        /// Returns a list of all Projects from the database.
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<Project>> GetProjectListAsync();

        /// <summary>
        /// Creates a new Project in the database.
        /// </summary>
        /// <param name="project">The ProjectDTO to be added.</param>
        /// <returns></returns>
        public abstract Task<Project> CreateProjectAsync(ProjectDTO project);

        /// <summary>
        /// Updates the Project with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the project to be updated.</param>
        /// <param name="project">The replacement Project.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateProjectAsync(int id, Project project);

        /// <summary>
        /// Deletes a Project from the database.
        /// </summary>
        /// <param name="id">The ID of the Project to be deleted.</param>
        /// <returns></returns>
        public abstract Task<bool> DeleteProjectAsync(int id);
    }
}
