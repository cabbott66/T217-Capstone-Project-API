using T217_Capstone_Project_API.Models.Risks;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    public interface IRisksRepository
    {
        #region PersonalRisk
        /// <summary>
        /// Returns the PersonalRisk from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired PersonalRisk.</param>
        /// <returns></returns>
        public abstract Task<PersonalRisk> GetPersonalRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the PersonalRisk from the database with the matching StakeholderID.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<PersonalRisk> GetPersonalRiskFromStakeholderAsync(int stakeholderId, string apiKey);

        /// <summary>
        /// Creates a new PersonalRisk in the database.
        /// </summary>
        /// <param name="personalRisk">The PersonalRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<PersonalRisk> CreatePersonalRiskAsync(PersonalRiskDTO personalRisk, string apiKey);

        /// <summary>
        /// Updates the PersonalRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisk to be updated.</param>
        /// <param name="personalRisk">The replacement PersonalRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdatePersonalRiskAsync(int id, PersonalRisk personalRisk, string apiKey);

        /// <summary>
        /// Deletes a PersonalRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeletePersonalRiskAsync(int id, string apiKey);
        #endregion

        #region ProjectRisk
        /// <summary>
        /// Returns the ProjectRisk from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired ProjectRisk.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisk> GetProjectRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the ProjectRisk from the database with the matching ProjectID.
        /// </summary>
        /// <param name="id">The ID of the Project.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisk> GetProjectRiskFromProjectAsync(int projectId, string apiKey);

        /// <summary>
        /// Creates a new ProjectRisk in the database.
        /// </summary>
        /// <param name="projectRisk">The ProjectRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisk> CreateProjectRiskAsync(ProjectRiskDTO projectRisk, string apiKey);

        /// <summary>
        /// Updates the ProjectRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisk to be updated.</param>
        /// <param name="projectRisk">The replacement ProjectRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateProjectRiskAsync(int id, ProjectRisk projectRisk, string apiKey);

        /// <summary>
        /// Deletes a ProjectRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteProjectRiskAsync(int id, string apiKey);
        #endregion

        #region InterpersonalRisk
        /// <summary>
        /// Returns the InterpersonalRisk from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired InterpersonalRisk.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisk> GetInterpersonalRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the InterpersonalRisk from the database with the matching StakeholderID.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisk> GetInterpersonalRiskFromStakeholderAsync(int stakeholderId, string apiKey);

        /// <summary>
        /// Creates a new InterpersonalRisk in the database.
        /// </summary>
        /// <param name="interpersonalRisk">The InterpersonalRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisk> CreateInterpersonalRiskAsync(InterpersonalRiskDTO interpersonalRisk, string apiKey);

        /// <summary>
        /// Updates the InterpersonalRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisk to be updated.</param>
        /// <param name="interpersonalRisk">The replacement InterpersonalRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateInterpersonalRiskAsync(int id, InterpersonalRisk interpersonalRisk, string apiKey);

        /// <summary>
        /// Deletes a InterpersonalRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteInterpersonalRiskAsync(int id, string apiKey);
        #endregion

        #region EnvironmentalRisk
        /// <summary>
        /// Returns the EnvironmentalRisk from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired EnvironmentalRisk.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisk> GetEnvironmentalRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the EnvironmentalRisk from the database with the matching StakeholderID.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisk> GetEnvironmentalRiskFromStakeholderAsync(int stakeholderId, string apiKey);

        /// <summary>
        /// Creates a new EnvironmentalRisk in the database.
        /// </summary>
        /// <param name="environmentalRisk">The EnvironmentalRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisk> CreateEnvironmentalRiskAsync(EnvironmentalRiskDTO environmentalRisk, string apiKey);

        /// <summary>
        /// Updates the EnvironmentalRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisk to be updated.</param>
        /// <param name="environmentalRisk">The replacement EnvironmentalRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateEnvironmentalRiskAsync(int id, EnvironmentalRisk environmentalRisk, string apiKey);

        /// <summary>
        /// Deletes a EnvironmentalRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteEnvironmentalRiskAsync(int id, string apiKey);
        #endregion
    }
}
