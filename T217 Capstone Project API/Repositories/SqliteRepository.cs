using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.DTO;

namespace T217_Capstone_Project_API.Repositories
{
    public class SqliteRepository
    {
        private readonly StakeholderRisksContext _context = new StakeholderRisksContext();
    }
}
