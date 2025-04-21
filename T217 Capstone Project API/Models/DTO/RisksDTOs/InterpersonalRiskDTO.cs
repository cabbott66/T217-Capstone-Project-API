using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class InterpersonalRiskDTO
    {
        public int StakeholderID { get; set; }

        public int Support { get; set; }

        public int SupportiveManagement { get; set; }

        public int Trust {  get; set; }

        public int Respect { get; set; }

        public int Communication { get; set; }

        public int SharingSuccess { get; set; }
    }
}
