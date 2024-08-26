using T217_Capstone_Project_API.Models;

namespace T217_Capstone_Project_API.Repositories
{
    public interface IStakeholderRepository
    {
        public Stakeholder GetStakeHolder(int id);
        public IEnumerable<Stakeholder> GetStakeHoldersList();
        public void CreateStakeholder(Stakeholder stakeholder);
        public OperationResult<Stakeholder> UpdateStakeholder(int id, Stakeholder newStakeholder);
        public OperationResult<Stakeholder> DeleteStakeholder(int id);
    }
}
