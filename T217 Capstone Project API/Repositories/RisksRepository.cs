using Microsoft.EntityFrameworkCore;
using T217_Capstone_Project_API.Models.DTO;
using T217_Capstone_Project_API.Models.Projects;
using T217_Capstone_Project_API.Models.Risks;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    public class RisksRepository : IRisksRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound,
            NotAuthorized
        }

        private readonly StakeholderRisksContext _context;

        public RisksRepository(StakeholderRisksContext context)
        {
            _context = context;
        }

        public async Task<EnvironmentalRisk> CreateEnvironmentalRiskAsync(EnvironmentalRisk environmentalRisk, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projects in _context.Projects
                            on projectUsers.ProjectID equals projects.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.StakeholderGroupID == environmentalRisk.StakeholderGroupID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select stakeholderGroups;

            if (await query.FirstOrDefaultAsync() != null)
            {
                _context.Add(environmentalRisk);
                await _context.SaveChangesAsync();

                return environmentalRisk;
            }

            return new EnvironmentalRisk();
        }

        public async Task<InterpersonalRisk> CreateInterpersonalRiskAsync(InterpersonalRisk interpersonalRisk, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projects in _context.Projects
                            on projectUsers.ProjectID equals projects.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.StakeholderGroupID == interpersonalRisk.StakeholderGroupID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select stakeholderGroups;

            if (await query.FirstOrDefaultAsync() != null)
            {
                _context.Add(interpersonalRisk);
                await _context.SaveChangesAsync();

                return interpersonalRisk;
            }

            return new InterpersonalRisk();
        }

        public async Task<PersonalRisk> CreatePersonalRiskAsync(PersonalRisk personalRisk, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projects in _context.Projects
                            on projectUsers.ProjectID equals projects.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.StakeholderGroupID == personalRisk.StakeholderGroupID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select stakeholderGroups;

            if (await query.FirstOrDefaultAsync() != null)
            {
                _context.Add(personalRisk);
                await _context.SaveChangesAsync();

                return personalRisk;
            }

            return new PersonalRisk();
        }

        public async Task<ProjectRisk> CreateProjectRiskAsync(ProjectRisk projectRisk, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projects in _context.Projects
                            on projectUsers.ProjectID equals projects.ProjectID
                        where projects.ProjectID == projectRisk.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select projects;

            if (await query.FirstOrDefaultAsync() != null)
            {
                _context.Add(projectRisk);
                await _context.SaveChangesAsync();

                return projectRisk;
            }

            return new ProjectRisk();
        }

        public async Task<int> DeleteEnvironmentalRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        join envRisks in _context.EnvironmentalRisks
                            on stakeholderGroups.StakeholderGroupID equals envRisks.StakeholderGroupID
                        where envRisks.EnvironmentalRiskID == id
                        where envRisks.StakeholderGroupID == stakeholderGroups.StakeholderGroupID
                                                where projectUsers.ProjectID == projects.ProjectID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            var envRisk = await _context.EnvironmentalRisks.FindAsync(id);
            if (envRisk == null)
            {
                return (int)UpdateStatus.NotFound;
            }

            _context.Remove(envRisk);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        public async Task<int> DeleteInterpersonalRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        join interpRisks in _context.InterpersonalRisks
                            on stakeholderGroups.StakeholderGroupID equals interpRisks.StakeholderGroupID
                        where interpRisks.InterpersonalRiskID == id
                        where interpRisks.StakeholderGroupID == stakeholderGroups.StakeholderGroupID
                        where projectUsers.ProjectID == projects.ProjectID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            var interRisk = await _context.InterpersonalRisks.FindAsync(id);
            if (interRisk == null)
            {
                return (int)UpdateStatus.NotFound;
            }

            _context.Remove(interRisk);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        public async Task<int> DeletePersonalRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.StakeholderGroups
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        join personRisks in _context.PersonalRisks
                            on stakeholderGroups.StakeholderGroupID equals personRisks.StakeholderGroupID
                        where personRisks.PersonalRiskID == id
                        where personRisks.StakeholderGroupID == stakeholderGroups.StakeholderGroupID
                        where projectUsers.ProjectID == projects.ProjectID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            var personRisk = await _context.PersonalRisks.FindAsync(id);
            if (personRisk == null)
            {
                return (int)UpdateStatus.NotFound;
            }

            _context.Remove(personRisk);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        public async Task<int> DeleteProjectRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join projectRisks in _context.ProjectRisks
                            on projects.ProjectID equals projectRisks.ProjectID
                        where projectRisks.ProjectRiskID == id
                        where projectRisks.ProjectID == projects.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            var projectRisk = await _context.ProjectRisks.FindAsync(id);
            if (projectRisk == null)
            {
                return (int)UpdateStatus.NotFound;
            }

            _context.Remove(projectRisk);
            await _context.SaveChangesAsync();

            return (int)UpdateStatus.Success;
        }

        public async Task<EnvironmentalRisk> GetEnvironmentalRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.StakeholderGroups
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join envRisks in _context.EnvironmentalRisks
                            on stakeholders.StakeholderGroupID equals envRisks.StakeholderGroupID
                        where envRisks.EnvironmentalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where envRisks.StakeholderGroupID == stakeholders.StakeholderGroupID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select envRisks;

            var envRisk = await query.FirstOrDefaultAsync();

            if (envRisk != null)
            {
                return envRisk;
            }

            return new EnvironmentalRisk();
        }

        public async Task<InterpersonalRisk> GetInterpersonalRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.StakeholderGroups
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join interRisks in _context.InterpersonalRisks
                            on stakeholders.StakeholderGroupID equals interRisks.StakeholderGroupID
                        where interRisks.InterpersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where interRisks.StakeholderGroupID == stakeholders.StakeholderGroupID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select interRisks;

            var interRisk = await query.FirstOrDefaultAsync();

            if (interRisk != null)
            {
                return interRisk;
            }

            return new InterpersonalRisk();
        }

        public async Task<PersonalRisk> GetPersonalRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.StakeholderGroups
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join personRisks in _context.PersonalRisks
                            on stakeholders.StakeholderGroupID equals personRisks.StakeholderGroupID
                        where personRisks.PersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where personRisks.StakeholderGroupID == stakeholders.StakeholderGroupID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select personRisks;

            var personRisk = await query.FirstOrDefaultAsync();

            if (personRisk != null)
            {
                return personRisk;
            }

            return new PersonalRisk();
        }

        public async Task<ProjectRisk> GetProjectRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projectRisks in _context.ProjectRisks
                            on projectUsers.ProjectID equals projectRisks.ProjectID
                        where projectRisks.ProjectRiskID == id
                        where projectUsers.ProjectID == projectRisks.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanRead == true
                        select projectRisks;

            var projectRisk = await query.FirstOrDefaultAsync();

            if (projectRisk != null)
            {
                return projectRisk;
            }

            return new ProjectRisk();
        }

        // TODO: Update project edit datetime on update.

        public async Task<int> UpdateEnvironmentalRiskAsync(int id, EnvironmentalRisk environmentalRisk, string apiKey)
        {
            if (id != environmentalRisk.EnvironmentalRiskID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.StakeholderGroups
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join envRisks in _context.EnvironmentalRisks
                            on stakeholders.StakeholderGroupID equals envRisks.StakeholderGroupID
                        where envRisks.EnvironmentalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where envRisks.StakeholderGroupID == stakeholders.StakeholderGroupID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            _context.EnvironmentalRisks.Entry(environmentalRisk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.EnvironmentalRisks.Any(e => e.EnvironmentalRiskID == id)))
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

        public async Task<int> UpdateInterpersonalRiskAsync(int id, InterpersonalRisk interpersonalRisk, string apiKey)
        {
            if (id != interpersonalRisk.InterpersonalRiskID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.StakeholderGroups
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join interRisks in _context.InterpersonalRisks
                            on stakeholders.StakeholderGroupID equals interRisks.StakeholderGroupID
                        where interRisks.InterpersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where interRisks.StakeholderGroupID == stakeholders.StakeholderGroupID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            _context.InterpersonalRisks.Entry(interpersonalRisk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.InterpersonalRisks.Any(e => e.InterpersonalRiskID == id)))
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

        public async Task<int> UpdatePersonalRiskAsync(int id, PersonalRisk personalRisk, string apiKey)
        {
            if (id != personalRisk.PersonalRiskID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.StakeholderGroups
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join personRisks in _context.PersonalRisks
                            on stakeholders.StakeholderGroupID equals personRisks.StakeholderGroupID
                        where personRisks.PersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where personRisks.StakeholderGroupID == stakeholders.StakeholderGroupID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            _context.PersonalRisks.Entry(personalRisk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.PersonalRisks.Any(e => e.PersonalRiskID == id)))
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

        public async Task<int> UpdateProjectRiskAsync(int id, ProjectRisk projectRisk, string apiKey)
        {
            if (id != projectRisk.ProjectRiskID)
            {
                return (int)UpdateStatus.BadRequest;
            }

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projectRisks in _context.ProjectRisks
                            on projectUsers.ProjectID equals projectRisks.ProjectID
                        where projectRisks.ProjectRiskID == id
                        where projectUsers.ProjectID == projectRisk.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.IsAdmin == true
                        select projectUsers;

            var projectUser = await query.FirstOrDefaultAsync();

            if (projectUser == null)
            {
                return (int)UpdateStatus.NotAuthorized;
            }

            _context.ProjectRisks.Entry(projectRisk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.ProjectRisks.Any(e => e.ProjectRiskID == id)))
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
