using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    public class StakeholderGroupRepository : IStakeholderGroupRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound
        }

        private readonly StakeholderRisksContext _context = new StakeholderRisksContext();

        public async Task<StakeholderGroup> GetStakeholderGroupAsync(int id)
        {
            var stakeholderGroup = await _context.StakeholderGroups.FindAsync(id);

            return stakeholderGroup;
        }

        public async Task<List<StakeholderGroup>> GetStakeholderGroupListAsync()
        {
            var stakeholderGroupList = await _context.StakeholderGroups.OrderBy(x => x.StakeholderGroupID).ToListAsync();

            return stakeholderGroupList;
        }

        public async Task<List<StakeholderGroup>> GetStakeholderGroupListByProjectAsync(int id)
        {
            var stakeholderGroupList = await _context.StakeholderGroups.Where(x => x.ProjectID == id).OrderBy(x => x.StakeholderGroupID).ToListAsync();

            return stakeholderGroupList;
        }

        public async Task<StakeholderGroup> CreateStakeholderGroupAsync(StakeholderGroupDTO stakeholderGroupDTO)
        {
            StakeholderGroup newStakeholderGroup = new StakeholderGroup();

            newStakeholderGroup.StakeholderGroupName = stakeholderGroupDTO.StakeholderGroupName;
            newStakeholderGroup.ProjectID = stakeholderGroupDTO.ProjectID;

            _context.Add(newStakeholderGroup);
            await _context.SaveChangesAsync();

            return newStakeholderGroup;
        }

        public async Task<int> UpdateStakeholderGroupAsync(int id, StakeholderGroup stakeholderGroup)
        {
            if (id != stakeholderGroup.StakeholderGroupID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            _context.StakeholderGroups.Entry(stakeholderGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StakeholderGroupExists(id))
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

        public async Task<bool> DeleteStakeholderGroupAsync(int id)
        {
            var stakeholderGroup = await _context.StakeholderGroups.FindAsync(id);
            if (stakeholderGroup == null)
            {
                return false;
            }

            _context.Remove(stakeholderGroup);
            await _context.SaveChangesAsync();

            return true;
        }

        // Checks the database if a StakeholderGroup with the matching ID exists.
        private bool StakeholderGroupExists(int id)
        {
            return _context.StakeholderGroups.Any(e => e.StakeholderGroupID == id);
        }
    }
}
