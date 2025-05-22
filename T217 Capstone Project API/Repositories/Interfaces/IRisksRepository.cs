using T217_Capstone_Project_API.Models.Risks;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    /// <summary>
    /// Repository used to connect the application to the Risks tables in the Database.
    /// </summary>
    public interface IRisksRepository
    {
        #region PersonalRisk
        /// <summary>
        /// Returns the PersonalRisks from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired PersonalRisks.</param>
        /// <returns></returns>
        public abstract Task<PersonalRisks> GetPersonalRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the PersonalRisks from the database with the matching StakeholderID.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<PersonalRisks> GetPersonalRiskFromStakeholderAsync(int stakeholderId, string apiKey);

        /// <summary>
        /// Creates a new PersonalRisks in the database.
        /// </summary>
        /// <param name="personalRisk">The PersonalRisks to be added.</param>
        /// <returns></returns>
        public abstract Task<PersonalRisks> CreatePersonalRiskAsync(PersonalRisksDTO personalRisk, string apiKey);

        /// <summary>
        /// Updates the PersonalRisks with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisks to be updated.</param>
        /// <param name="personalRisk">The replacement PersonalRisks.</param>
        /// <returns></returns>
        public abstract Task<int> UpdatePersonalRiskAsync(int id, PersonalRisks personalRisk, string apiKey);

        /// <summary>
        /// Deletes a PersonalRisks from the database.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisks to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeletePersonalRiskAsync(int id, string apiKey);
        #endregion

        #region ProjectRisk
        /// <summary>
        /// Returns the ProjectRisks from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired ProjectRisks.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisks> GetProjectRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the ProjectRisks from the database with the matching ProjectID.
        /// </summary>
        /// <param name="id">The ID of the Project.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisks> GetProjectRiskFromProjectAsync(int projectId, string apiKey);

        /// <summary>
        /// Creates a new ProjectRisks in the database.
        /// </summary>
        /// <param name="projectRisk">The ProjectRisks to be added.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisks> CreateProjectRiskAsync(ProjectRisksDTO projectRisk, string apiKey);

        /// <summary>
        /// Updates the ProjectRisks with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisks to be updated.</param>
        /// <param name="projectRisk">The replacement ProjectRisks.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateProjectRiskAsync(int id, ProjectRisks projectRisk, string apiKey);

        /// <summary>
        /// Deletes a ProjectRisks from the database.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisks to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteProjectRiskAsync(int id, string apiKey);
        #endregion

        #region InterpersonalRisk
        /// <summary>
        /// Returns the InterpersonalRisks from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired InterpersonalRisks.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisks> GetInterpersonalRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the InterpersonalRisks from the database with the matching StakeholderID.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisks> GetInterpersonalRiskFromStakeholderAsync(int stakeholderId, string apiKey);

        /// <summary>
        /// Creates a new InterpersonalRisks in the database.
        /// </summary>
        /// <param name="interpersonalRisk">The InterpersonalRisks to be added.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisks> CreateInterpersonalRiskAsync(InterpersonalRisksDTO interpersonalRisk, string apiKey);

        /// <summary>
        /// Updates the InterpersonalRisks with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisks to be updated.</param>
        /// <param name="interpersonalRisk">The replacement InterpersonalRisks.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateInterpersonalRiskAsync(int id, InterpersonalRisks interpersonalRisk, string apiKey);

        /// <summary>
        /// Deletes a InterpersonalRisks from the database.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisks to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteInterpersonalRiskAsync(int id, string apiKey);
        #endregion

        #region EnvironmentalRisk
        /// <summary>
        /// Returns the EnvironmentalRisks from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired EnvironmentalRisks.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisks> GetEnvironmentalRiskAsync(int id, string apiKey);

        /// <summary>
        /// Returns the EnvironmentalRisks from the database with the matching StakeholderID.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisks> GetEnvironmentalRiskFromStakeholderAsync(int stakeholderId, string apiKey);

        /// <summary>
        /// Creates a new EnvironmentalRisks in the database.
        /// </summary>
        /// <param name="environmentalRisk">The EnvironmentalRisks to be added.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisks> CreateEnvironmentalRiskAsync(EnvironmentalRisksDTO environmentalRisk, string apiKey);

        /// <summary>
        /// Updates the EnvironmentalRisks with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisks to be updated.</param>
        /// <param name="environmentalRisk">The replacement EnvironmentalRisks.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateEnvironmentalRiskAsync(int id, EnvironmentalRisks environmentalRisk, string apiKey);

        /// <summary>
        /// Deletes a EnvironmentalRisks from the database.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisks to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteEnvironmentalRiskAsync(int id, string apiKey);
        #endregion
    }
}
