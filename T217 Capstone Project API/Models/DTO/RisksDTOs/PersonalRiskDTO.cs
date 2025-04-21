using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class PersonalRiskDTO
    {
        public int StakeholderID { get; set; }

        public int Location {  get; set; }

        public int Workload { get; set; }

        public int Involvement { get; set; }

        public int EducationTraining { get; set; }

        public int Kpi { get; set; }

        public int Impact { get; set; }

        public int RoleType { get; set; }

        public int ServiceLength { get; set; }

        public int Age { get; set; }

        public int Status { get; set; }

        public int Experience { get; set; }

        public int Interest { get; set; }

        public int History { get; set; }

        public int Personalities { get; set; }

        public int PriorRole { get; set; }
    }
}
