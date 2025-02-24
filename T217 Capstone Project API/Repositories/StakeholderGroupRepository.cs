using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models;
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
            NotFound,
            NotAuthorized
        }

        private readonly StakeholderRisksContext _context = new StakeholderRisksContext();

        public async Task<StakeholderGroup> GetStakeholderGroupAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.StakeholderGroupID == id
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select stakeholderGroups;

            var stakeholderGroup = await query.FirstOrDefaultAsync();

            if (stakeholderGroup != null)
            {
                return stakeholderGroup;
            }

            return new StakeholderGroup();
        }

        public async Task<List<StakeholderGroup>> GetStakeholderGroupListAsync(string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select stakeholderGroups;

            var stakeholderGroupList = await query.OrderBy(x => x.StakeholderGroupID).ToListAsync();

            return stakeholderGroupList;
        }

        public async Task<List<StakeholderGroup>> GetStakeholderGroupListByProjectAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projects.ProjectID == id
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select stakeholderGroups;

            var stakeholderGroupList = await query.OrderBy(x => x.StakeholderGroupID).ToListAsync();

            return stakeholderGroupList;
        }


        public async Task<StakeholderGroup> CreateStakeholderGroupAsync(StakeholderGroupDTO stakeholderGroupDTO, string apiKey)
        {
            StakeholderGroup stakeholderGroup = new StakeholderGroup();

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        where projectUsers.ProjectID == stakeholderGroupDTO.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select projectUsers;

            if (await query.FirstOrDefaultAsync() != null)
            {
                stakeholderGroup.StakeholderGroupName = stakeholderGroupDTO.StakeholderGroupName;
                stakeholderGroup.ProjectID = stakeholderGroupDTO.ProjectID;

                _context.Add(stakeholderGroup);
                await _context.SaveChangesAsync();

                return stakeholderGroup;
            }

            return stakeholderGroup;
        }

        public async Task<int> UpdateStakeholderGroupAsync(int id, StakeholderGroup stakeholderGroup, string apiKey)
        {
            if (id != stakeholderGroup.StakeholderGroupID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var projectUser = await _context.ProjectUsers
                .Where(x => x.ProjectID == stakeholderGroup.ProjectID && x.UserID == userId && x.CanEdit == true)
                .FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
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

        public async Task<int> DeleteStakeholderGroupAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.StakeholderGroupID == id
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            var stakeholderGroup = await _context.StakeholderGroups.FindAsync(id);
            if (stakeholderGroup == null)
            {
                return (int)UpdateStatus.NotFound;
            }

            _context.Remove(stakeholderGroup);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        // Checks the database if a StakeholderGroup with the matching ID exists.
        private bool StakeholderGroupExists(int id)
        {
            return _context.StakeholderGroups.Any(e => e.StakeholderGroupID == id);
        }

        // Finds the User with the matching API key and returns their UserID.
        private async Task<int> GetUserIdFromApiKey(string apiKey)
        {
            var user = await _context.Users.Where(x => x.ApiKey == apiKey).FirstAsync();

            if (user == null)
            {
                return -1;
            }
            else
            {
                return user.UserID;
            }
        }
    }
}
