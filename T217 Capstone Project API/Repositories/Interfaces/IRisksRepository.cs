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
        public abstract Task<PersonalRisk> GetPersonalRiskAsync(int id);

        /// <summary>
        /// Creates a new PersonalRisk in the database.
        /// </summary>
        /// <param name="personalRisk">The PersonalRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<PersonalRisk> CreatePersonalRiskAsync(PersonalRisk personalRisk);

        /// <summary>
        /// Updates the PersonalRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisk to be updated.</param>
        /// <param name="personalRisk">The replacement PersonalRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdatePersonalRiskAsync(int id, PersonalRisk personalRisk);

        /// <summary>
        /// Deletes a PersonalRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the PersonalRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<bool> DeletePersonalRiskAsync(int id);
        #endregion

        #region ProjectRisk
        /// <summary>
        /// Returns the ProjectRisk from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired ProjectRisk.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisk> GetProjectRiskAsync(int id);

        /// <summary>
        /// Creates a new ProjectRisk in the database.
        /// </summary>
        /// <param name="projectRisk">The ProjectRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<ProjectRisk> CreateProjectRiskAsync(ProjectRisk projectRisk);

        /// <summary>
        /// Updates the ProjectRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisk to be updated.</param>
        /// <param name="projectRisk">The replacement ProjectRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateProjectRiskAsync(int id, ProjectRisk projectRisk);

        /// <summary>
        /// Deletes a ProjectRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the ProjectRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<bool> DeleteProjectRiskAsync(int id);
        #endregion

        #region InterpersonalRisk
        /// <summary>
        /// Returns the InterpersonalRisk from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired InterpersonalRisk.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisk> GetInterpersonalRiskAsync(int id);

        /// <summary>
        /// Creates a new InterpersonalRisk in the database.
        /// </summary>
        /// <param name="interpersonalRisk">The InterpersonalRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<InterpersonalRisk> CreateInterpersonalRiskAsync(InterpersonalRisk interpersonalRisk);

        /// <summary>
        /// Updates the InterpersonalRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisk to be updated.</param>
        /// <param name="interpersonalRisk">The replacement InterpersonalRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateInterpersonalRiskAsync(int id, InterpersonalRisk interpersonalRisk);

        /// <summary>
        /// Deletes a InterpersonalRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the InterpersonalRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<bool> DeleteInterpersonalRiskAsync(int id);
        #endregion

        #region EnvironmentalRisk
        /// <summary>
        /// Returns the EnvironmentalRisk from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired EnvironmentalRisk.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisk> GetEnvironmentalRiskAsync(int id);

        /// <summary>
        /// Creates a new EnvironmentalRisk in the database.
        /// </summary>
        /// <param name="environmentalRisk">The EnvironmentalRisk to be added.</param>
        /// <returns></returns>
        public abstract Task<EnvironmentalRisk> CreateEnvironmentalRiskAsync(EnvironmentalRisk environmentalRisk);

        /// <summary>
        /// Updates the EnvironmentalRisk with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisk to be updated.</param>
        /// <param name="environmentalRisk">The replacement EnvironmentalRisk.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateEnvironmentalRiskAsync(int id, EnvironmentalRisk environmentalRisk);

        /// <summary>
        /// Deletes a EnvironmentalRisk from the database.
        /// </summary>
        /// <param name="id">The ID of the EnvironmentalRisk to be deleted.</param>
        /// <returns></returns>
        public abstract Task<bool> DeleteEnvironmentalRiskAsync(int id);
        #endregion
    }
}
