using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Repositories.Interfaces
{
    public interface IStakeholderGroupRepository
    {
        /// <summary>
        /// Returns the StakeholderGroup from the database with the matching ID.
        /// </summary>
        /// <param name="id">The ID of the desired StakeholderGroup.</param>
        /// <returns></returns>
        public abstract Task<StakeholderGroup> GetStakeholderGroupAsync(int id);

        /// <summary>
        /// Returns a list of all StakeholderGroup from the database.
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<StakeholderGroup>> GetStakeholderGroupListAsync();

        /// <summary>
        /// Returns a list of all StakeholderGroup that the User has access to.
        /// </summary>
        /// <param name="id">The ID of the current Project.</param>
        /// <returns></returns>
        public abstract Task<List<StakeholderGroup>> GetStakeholderGroupListByProjectAsync(int id);

        /// <summary>
        /// Creates a new StakeholderGroup in the database.
        /// </summary>
        /// <param name="stakeholderGroupDTO">The StakeholderDTO to be added..</param>
        /// <returns></returns>
        public abstract Task<StakeholderGroup> CreateStakeholderGroupAsync(StakeholderGroupDTO stakeholderGroupDTO);

        /// <summary>
        /// Updates the StakeholderGroup with the matching ID with new values.
        /// </summary>
        /// <param name="id">The ID of the StakeholderGroup to be updated.</param>
        /// <param name="project">The replacement StakeholderGroup.</param>
        /// <returns></returns>
        public abstract Task<int> UpdateStakeholderGroupAsync(int id, StakeholderGroup stakeholderGroup);

        /// <summary>
        /// Deletes a StakeholderGroup from the database.
        /// </summary>
        /// <param name="id">The ID of the StakeholderGroup to be deleted.</param>
        /// <returns></returns>
        public abstract Task<bool> DeleteStakeholderGroupAsync(int id);
    }
}
