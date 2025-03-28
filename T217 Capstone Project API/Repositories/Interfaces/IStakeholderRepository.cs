using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    public interface IStakeholderRepository
    {
        // TODO: Update XML Commenting.

        /// <summary>
        /// Returns the Stakeholder from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<Stakeholder> GetStakeholderAsync(int id, string apiKey);

        /// <summary>
        /// Returns a list of all Stakeholder from the database.
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<Stakeholder>> GetStakeholderListAsync(string apiKey);

        /// <summary>
        /// Returns a list of all Stakeholder that the User has access to.
        /// </summary>
        /// <param name="id">The ID of the current Project.</param>
        /// <returns></returns>
        public abstract Task<List<Stakeholder>> GetStakeholderListByProjectAsync(int id, string apiKey);

        /// <summary>
        /// Creates a new Stakeholder in the database.
        /// </summary>
        /// <param name="stakeholderDTO">The StakeholderDTO to be added..</param>
        /// <returns></returns>
        public abstract Task<Stakeholder> CreateStakeholderAsync(StakeholderDTO stakeholderDTO, string apiKey);

        /// <summary>
        /// Updates the Stakeholder with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder to be updated.</param>
        /// <param name="project">The replacement Stakeholder.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateStakeholderAsync(int id, Stakeholder stakeholder, string apiKey);

        /// <summary>
        /// Deletes a Stakeholder from the database.
        /// </summary>
        /// <param name="id">The ID of the Stakeholder to be deleted.</param>
        /// <returns></returns>
        public abstract Task<int> DeleteStakeholderAsync(int id, string apiKey);
    }
}
