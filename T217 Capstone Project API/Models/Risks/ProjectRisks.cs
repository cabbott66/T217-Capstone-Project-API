using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;
using System.Text.Json.Serialization;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class ProjectRisks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ProjectRiskID { get; set; }

        [ForeignKey(nameof(Project.ProjectID))]
        [Required]
        public int ProjectID { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }

        public int TypeOfChange { get; set; }

        public int ProjectLength { get; set; }

        public int CulturalAlignment { get; set; }

        public int Resourcing { get; set; }

        public int ProjectGoals { get; set; }
        public int Priority { get; set; }
    }
}
