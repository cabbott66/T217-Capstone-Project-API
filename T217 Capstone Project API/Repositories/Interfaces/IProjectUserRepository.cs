using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    /// <summary>
    /// Repository used to connect the application to the ProjectUsers table in the Database.
    /// </summary>
    public interface IProjectUserRepository
    {
        /// <summary>
        /// Returns the ProjectUser from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the ProjectUser</param>
        /// <returns>The ProjectUser with the matching ID.</returns>
        public abstract Task<ProjectUser> GetProjectUserAsync(int id);

        /// <summary>
        /// Returns a list of ProjectUsers.
        /// </summary>
        /// <returns>A list of ProjectUsers.</returns>
        public abstract Task<List<ProjectUser>> GetProjectUserListAsync();

        /// <summary>
        /// Returns a list of ProjectUsers associated with the UserID.
        /// </summary>
        /// <param name="id">The ID of the User.</param>
        /// <returns>A list of ProjectUsers associated with the UserID..</returns>
        public abstract Task<List<ProjectUser>> GetProjectUserListByUserAsync(int id);

        /// <summary>
        /// Returns a list of ProjectUsers associated with teh ProjectID.
        /// </summary>
        /// <param name="id">The ID of the Project.</param>
        /// <returns>A list of ProjectUsers assoicated with the ProjectID..</returns>
        public abstract Task<List<ProjectUser>> GetProjectUserListByProjectAsync(int id, string apiKey);

        /// <summary>
        /// Returns the ProjectUser associated with both the UserID and ProjectID.
        /// </summary>
        /// <param name="userId">The ID of the User.</param>
        /// <param name="projectId">The ID of the Project.</param>
        /// <returns>The ProjectUser assocaited with both the UserID and ProjectID.</returns>
        public abstract Task<ProjectUser> GetProjectUserByUserAndProjectAsync(int userId, int projectId);

        /// <summary>
        /// Creates a new ProjectUser.
        /// </summary>
        /// <param name="projectUser">The ProjectUser to be created.</param>
        /// <returns>The created ProjectUser.</returns>
        public abstract Task<ProjectUser> CreateProjectUserAsync(ProjectUser projectUser);

        /// <summary>
        /// Creates a new ProjectUser for a User, associated with an existing Project. 
        /// </summary>
        /// <param name="projectID">The ID of the Project.</param>
        /// <param name="newUserID">The ID of the User.</param>
        /// <param name="projectUserPermissions">The permissions that the new ProjectUser will have.</param>
        /// <returns></returns>
        public abstract Task<ProjectUser> AddNewProjectUserAsync(int projectID, int newUserID, string apiKey, ProjectUserPermissionsDTO projectUserPermissions);

        /// <summary>
        /// Updates the permissions of the ProjectUser with the associated ID.
        /// </summary>
        /// <param name="id">The ID of the ProjectUser.</param>
        /// <param name="projectUserPermissions">The new permissions for the ProjectUser.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateProjectUserAsync(int id, string apiKey, ProjectUserPermissionsDTO projectUserPermissions);

        /// <summary>
        /// Deletes the ProjectUser with the associated ID.
        /// </summary>
        /// <param name="id">The ID of the ProjectUser.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteProjectUserAsync(int id, string apiKey);
    }
}
