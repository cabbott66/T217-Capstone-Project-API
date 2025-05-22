using T217_Capstone_Project_API.Models.Risks;

namespace T217_Capstone_Project_API.Models.DTO.RisksDTOs
{
    public class StakeholderRisksDTO
    {
        public EnvironmentalRisks EnvironmentalRisk { get; set; }
        public InterpersonalRisks InterpersonalRisk { get; set; }
        public PersonalRisks PersonalRisk { get; set; }
        public int BlobID { get; set; }
    }
}
