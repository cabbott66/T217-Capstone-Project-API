using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;
using System.Text.Json.Serialization;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class PersonalRisk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PersonalRiskID { get; set; }

        [ForeignKey(nameof(Stakeholder.StakeholderID))]
        [Required]
        public int StakeholderID { get; set; }

        [JsonIgnore]
        public Stakeholder? Stakeholder { get; set; }

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
