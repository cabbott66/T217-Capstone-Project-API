using T217_Capstone_Project_API.Models.DTO.ProjectDTOs;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    /// <summary>
    /// Repository used to connect the application to the Projects table in the Database.
    /// </summary>
    public interface IProjectRepository
    {
        // TODO: Update XML commenting.

        /// <summary>
        /// Returns the Project from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired Project.</param>
        /// <returns></returns>
        public abstract Task<Project> GetProjectAsync(int id, string apiKey);

        /// <summary>
        /// Returns a list of all Projects from the database.
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<Project>> GetProjectListAsync(string apiKey);

        /// <summary>
        /// Creates a new Project and associated ProjectUser in the database.
        /// </summary>
        /// <param name="project">The ProjectDTO to be added.</param>
        /// <returns></returns>
        public abstract Task<Project> CreateProjectAsync(ProjectDTO project, string apiKey);

        /// <summary>
        /// Updates the Project with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the project to be updated.</param>
        /// <param name="project">The replacement Project.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateProjectAsync(int id, Project project, string apiKey);

        /// <summary>
        /// Deletes a Project from the database.
        /// </summary>
        /// <param name="id">The ID of the Project to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteProjectAsync(int id, string apiKey);
    }
}
