using T217_Capstone_Project_API.Models.Risks;

namespace T217_Capstone_Project_API.Models.DTO.RisksDTOs
{
    public class StakeholderRisksDTO
    {
        public EnvironmentalRisk EnvironmentalRisk { get; set; }
        public InterpersonalRisk InterpersonalRisk { get; set; }
        public PersonalRisk PersonalRisk { get; set; }
        public int BlobID { get; set; }
    }
}
