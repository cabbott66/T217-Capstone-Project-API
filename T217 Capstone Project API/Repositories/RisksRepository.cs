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

        public async Task<EnvironmentalRisk> CreateEnvironmentalRiskAsync(EnvironmentalRiskDTO environmentalRisk, string apiKey)
        {
            EnvironmentalRisk newEnvRisk = new EnvironmentalRisk();

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projects in _context.Projects
                            on projectUsers.ProjectID equals projects.ProjectID
                        join stakeholders in _context.Stakeholders
                            on projects.ProjectID equals stakeholders.ProjectID
                        where stakeholders.StakeholderID == environmentalRisk.StakeholderID
                        where stakeholders.ProjectID == projects.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select stakeholders;

            if (await query.FirstOrDefaultAsync() != null)
            {
                newEnvRisk.StakeholderID = environmentalRisk.StakeholderID;
                newEnvRisk.ChangeVolume= environmentalRisk.ChangeVolume;
                newEnvRisk.Infrastructure = environmentalRisk.Infrastructure;
                newEnvRisk.Industry = environmentalRisk.Industry;
                newEnvRisk.OfficePolitics = environmentalRisk.OfficePolitics;

                _context.Add(newEnvRisk);
                await _context.SaveChangesAsync();

                return newEnvRisk;
            }

            return new EnvironmentalRisk();
        }

        public async Task<InterpersonalRisk> CreateInterpersonalRiskAsync(InterpersonalRiskDTO interpersonalRisk, string apiKey)
        {
            InterpersonalRisk newInterRisk = new InterpersonalRisk();

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projects in _context.Projects
                            on projectUsers.ProjectID equals projects.ProjectID
                        join stakeholderGroups in _context.Stakeholders
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.StakeholderID == interpersonalRisk.StakeholderID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select stakeholderGroups;

            if (await query.FirstOrDefaultAsync() != null)
            {
                newInterRisk.StakeholderID = interpersonalRisk.StakeholderID;
                newInterRisk.Support = interpersonalRisk.Support;
                newInterRisk.SupportiveManagement = interpersonalRisk.SupportiveManagement;
                newInterRisk.Trust = interpersonalRisk.Trust;
                newInterRisk.Respect = interpersonalRisk.Respect;
                newInterRisk.Communication = interpersonalRisk.Communication;
                newInterRisk.SharingSuccess = interpersonalRisk.SharingSuccess;

                _context.Add(newInterRisk);
                await _context.SaveChangesAsync();

                return newInterRisk;
            }

            return new InterpersonalRisk();
        }

        public async Task<PersonalRisk> CreatePersonalRiskAsync(PersonalRiskDTO personalRisk, string apiKey)
        {
            PersonalRisk newPersonRisk = new PersonalRisk();

            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projects in _context.Projects
                            on projectUsers.ProjectID equals projects.ProjectID
                        join stakeholderGroups in _context.Stakeholders
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        where stakeholderGroups.StakeholderID == personalRisk.StakeholderID
                        where stakeholderGroups.ProjectID == projects.ProjectID
                        where projectUsers.ProjectID == projects.ProjectID
                        where projectUsers.UserID == userId
                        where projectUsers.CanWrite == true
                        select stakeholderGroups;

            if (await query.FirstOrDefaultAsync() != null)
            {
                newPersonRisk.StakeholderID = personalRisk.StakeholderID;
                newPersonRisk.Location = personalRisk.Location;
                newPersonRisk.Workload = personalRisk.Workload;
                newPersonRisk.Involvement = personalRisk.Involvement;
                newPersonRisk.EducationTraining = personalRisk.EducationTraining;
                newPersonRisk.Kpi = personalRisk.Kpi;
                newPersonRisk.Impact = personalRisk.Impact;
                newPersonRisk.RoleType = personalRisk.RoleType;
                newPersonRisk.ServiceLength = personalRisk.ServiceLength;
                newPersonRisk.Age = personalRisk.Age;
                newPersonRisk.Status = personalRisk.Status;
                newPersonRisk.Experience = personalRisk.Experience;
                newPersonRisk.Interest = personalRisk.Interest;
                newPersonRisk.History = personalRisk.History;
                newPersonRisk.Personalities = personalRisk.Personalities;
                newPersonRisk.PriorRole = personalRisk.PriorRole;

                _context.Add(newPersonRisk);
                await _context.SaveChangesAsync();

                return newPersonRisk;
            }

            return new PersonalRisk();
        }

        public async Task<ProjectRisk> CreateProjectRiskAsync(ProjectRiskDTO projectRisk, string apiKey)
        {
            ProjectRisk newProjectRisk = new ProjectRisk(); 

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
                newProjectRisk.ProjectID = projectRisk.ProjectID;
                newProjectRisk.TypeOfChange = projectRisk.TypeOfChange;
                newProjectRisk.ProjectLength = projectRisk.ProjectLength;
                newProjectRisk.Culture = projectRisk.Culture;
                newProjectRisk.CulturalAlignment = projectRisk.CulturalAlignment;
                newProjectRisk.Resourcing = projectRisk.Resourcing;
                newProjectRisk.ProjectGoals = projectRisk.ProjectGoals;
                newProjectRisk.Priority = projectRisk.Priority;

                _context.Add(newProjectRisk);
                await _context.SaveChangesAsync();

                return newProjectRisk;
            }

            return new ProjectRisk();
        }

        public async Task<int> DeleteEnvironmentalRiskAsync(int id, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projects in _context.Projects
                        join projectUsers in _context.ProjectUsers
                            on projects.ProjectID equals projectUsers.ProjectID
                        join stakeholderGroups in _context.Stakeholders
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        join envRisks in _context.EnvironmentalRisks
                            on stakeholderGroups.StakeholderID equals envRisks.StakeholderID
                        where envRisks.EnvironmentalRiskID == id
                        where envRisks.StakeholderID == stakeholderGroups.StakeholderID
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
                        join stakeholderGroups in _context.Stakeholders
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        join interpRisks in _context.InterpersonalRisks
                            on stakeholderGroups.StakeholderID equals interpRisks.StakeholderID
                        where interpRisks.InterpersonalRiskID == id
                        where interpRisks.StakeholderID == stakeholderGroups.StakeholderID
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
                        join stakeholderGroups in _context.Stakeholders
                            on projects.ProjectID equals stakeholderGroups.ProjectID
                        join personRisks in _context.PersonalRisks
                            on stakeholderGroups.StakeholderID equals personRisks.StakeholderID
                        where personRisks.PersonalRiskID == id
                        where personRisks.StakeholderID == stakeholderGroups.StakeholderID
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
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join envRisks in _context.EnvironmentalRisks
                            on stakeholders.StakeholderID equals envRisks.StakeholderID
                        where envRisks.EnvironmentalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where envRisks.StakeholderID == stakeholders.StakeholderID
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
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join interRisks in _context.InterpersonalRisks
                            on stakeholders.StakeholderID equals interRisks.StakeholderID
                        where interRisks.InterpersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where interRisks.StakeholderID == stakeholders.StakeholderID
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
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join personRisks in _context.PersonalRisks
                            on stakeholders.StakeholderID equals personRisks.StakeholderID
                        where personRisks.PersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where personRisks.StakeholderID == stakeholders.StakeholderID
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

        public async Task<EnvironmentalRisk> GetEnvironmentalRiskFromStakeholderAsync(int stakeholderId, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join envRisks in _context.EnvironmentalRisks
                            on stakeholders.StakeholderID equals envRisks.StakeholderID
                        where envRisks.StakeholderID == stakeholderId
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where envRisks.StakeholderID == stakeholders.StakeholderID
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

        public async Task<InterpersonalRisk> GetInterpersonalRiskFromStakeholderAsync(int stakeholderId, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join interRisks in _context.InterpersonalRisks
                            on stakeholders.StakeholderID equals interRisks.StakeholderID
                        where interRisks.StakeholderID == stakeholderId
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where interRisks.StakeholderID == stakeholders.StakeholderID
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

        public async Task<PersonalRisk> GetPersonalRiskFromStakeholderAsync(int stakeholderId, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join personRisks in _context.PersonalRisks
                            on stakeholders.StakeholderID equals personRisks.StakeholderID
                        where personRisks.StakeholderID == stakeholderId
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where personRisks.StakeholderID == stakeholders.StakeholderID
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

        public async Task<ProjectRisk> GetProjectRiskFromProjectAsync(int projectId, string apiKey)
        {
            var userId = await GetUserIdFromApiKey(apiKey);

            var query = from projectUsers in _context.ProjectUsers
                        join projectRisks in _context.ProjectRisks
                            on projectUsers.ProjectID equals projectRisks.ProjectID
                        where projectRisks.ProjectID == projectId
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
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join envRisks in _context.EnvironmentalRisks
                            on stakeholders.StakeholderID equals envRisks.StakeholderID
                        where envRisks.EnvironmentalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where envRisks.StakeholderID == stakeholders.StakeholderID
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
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join interRisks in _context.InterpersonalRisks
                            on stakeholders.StakeholderID equals interRisks.StakeholderID
                        where interRisks.InterpersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where interRisks.StakeholderID == stakeholders.StakeholderID
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
                        join stakeholders in _context.Stakeholders
                            on projectUsers.ProjectID equals stakeholders.ProjectID
                        join personRisks in _context.PersonalRisks
                            on stakeholders.StakeholderID equals personRisks.StakeholderID
                        where personRisks.PersonalRiskID == id
                        where projectUsers.ProjectID == stakeholders.ProjectID
                        where personRisks.StakeholderID == stakeholders.StakeholderID
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
            var user = await _context.Users.Where(x => x.ApiKey == apiKey).FirstOrDefaultAsync();

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
