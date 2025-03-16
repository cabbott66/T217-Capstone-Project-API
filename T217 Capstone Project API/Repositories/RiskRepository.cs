using T217_Capstone_Project_API.Models.Risks;
using T217_Capstone_Project_API.Repositories.Interfaces;

namespace T217_Capstone_Project_API.Repositories
{
    public class RiskRepository : IRisksRepository
    {
        enum UpdateStatus
        {
            Success,
            BadRequest,
            NotFound,
            NotAuthorized
        }

        private readonly StakeholderRisksContext _context;

        public RiskRepository(StakeholderRisksContext context)
        {
            _context = context;
        }

        public Task<EnvironmentalRisk> CreateEnvironmentalRiskAsync(EnvironmentalRisk environmentalRisk, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<InterpersonalRisk> CreateInterpersonalRiskAsync(InterpersonalRisk interpersonalRisk, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalRisk> CreatePersonalRiskAsync(PersonalRisk personalRisk, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectRisk> CreateProjectRiskAsync(ProjectRisk projectRisk, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEnvironmentalRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteInterpersonalRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePersonalRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteProjectRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<EnvironmentalRisk> GetEnvironmentalRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<InterpersonalRisk> GetInterpersonalRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalRisk> GetPersonalRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectRisk> GetProjectRiskAsync(int id, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateEnvironmentalRiskAsync(int id, EnvironmentalRisk environmentalRisk, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateInterpersonalRiskAsync(int id, InterpersonalRisk interpersonalRisk, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePersonalRiskAsync(int id, PersonalRisk personalRisk, string apiKey)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProjectRiskAsync(int id, ProjectRisk projectRisk, string apiKey)
        {
            throw new NotImplementedException();
        }
    }
}
