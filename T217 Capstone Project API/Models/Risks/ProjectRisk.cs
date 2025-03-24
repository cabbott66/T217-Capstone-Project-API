using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class ProjectRisk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ProjectRiskID { get; set; }

        [ForeignKey(nameof(Project.ProjectID))]
        [Required]
        public int ProjectID { get; set; }

        public Project Project { get; set; }

        public int TypeOfChange { get; set; }

        public int ProjectLength { get; set; }

        public int Culture { get; set; }

        public int CulturalAlignment { get; set; }

        public int Resourcing {  get; set; }

        public int ProjectGoals { get; set; }
        public int Priority { get; set; }
    }
}
