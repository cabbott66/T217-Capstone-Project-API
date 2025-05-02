using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    public class StakeholderRepository : IStakeholderRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound,
            NotAuthorized
        }

        private readonly StakeholderRisksContext _context;

        public StakeholderRepository(StakeholderRisksContext context) 
        {
            _context = context;
        }

        public async Task<Stakeholder> GetStakeholderAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        where stakeholders.ProjectID == projectUsers.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select stakeholders;

            var stakeholder = await query.FirstOrDefaultAsync();

            if (stakeholder != null)
            {
                return stakeholder;
            }

            return new Stakeholder();
        }

        public async Task<List<Stakeholder>> GetStakeholderListAsync(string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        where stakeholders.ProjectID == projectUsers.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select stakeholders;

            var stakeholderList = await query.OrderBy(x => x.StakeholderID).ToListAsync();

            return stakeholderList;
        }

        public async Task<List<Stakeholder>> GetStakeholderListByProjectAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        where stakeholders.ProjectID == projectUsers.ProjectID
                        where stakeholders.ProjectID == id
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select stakeholders;

            var stakeholderList = await query.OrderBy(x => x.StakeholderID).ToListAsync();

            return stakeholderList;
        }


        public async Task<Stakeholder> CreateStakeholderAsync(StakeholderDTO stakeholderDTO, string apiKey)
        {
            Stakeholder stakeholder = new Stakeholder();

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        where projectUsers.ProjectID == stakeholderDTO.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select projectUsers;

            if (await query.FirstOrDefaultAsync() != null)
            {
                stakeholder.StakeholderName = stakeholderDTO.StakeholderName;
                stakeholder.ProjectID = stakeholderDTO.ProjectID;
                stakeholder.CaFI = stakeholderDTO.CaFI;
                stakeholder.BlobID = stakeholderDTO.BlobID;
                stakeholder.Description = stakeholderDTO.Description;
                stakeholder.CreatedDateTime = DateTime.Now;
                stakeholder.EditDateTime = DateTime.Now;

                _context.Add(stakeholder);
                await _context.SaveChangesAsync();

                return stakeholder;
            }

            return stakeholder;
        }

        public async Task<int> UpdateStakeholderAsync(int id, Stakeholder stakeholder, string apiKey)
        {
            if (id != stakeholder.StakeholderID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var projectUser = await _context.ProjectUsers
                .Where(x => x.ProjectID == stakeholder.ProjectID && x.UserID == userId && x.CanEdit == true)
                .FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            stakeholder.EditDateTime = DateTime.Now;
            _context.Stakeholders.Entry(stakeholder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StakeholderExists(id))
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

        public async Task<int> DeleteStakeholderAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholders in _context.Stakeholders
                            on projects.ProjectID equals stakeholders.ProjectID
                        where stakeholders.StakeholderID == id
                        where stakeholders.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            var stakeholder = await _context.Stakeholders.FindAsync(id);
            if (stakeholder == null)
            {
                return (int)UpdateStatus.NotFound;
            }

            _context.Remove(stakeholder);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        // Checks the database if a Stakeholder with the matching ID exists.
        private bool StakeholderExists(int id)
        {
            return _context.Stakeholders.Any(e => e.StakeholderID == id);
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
