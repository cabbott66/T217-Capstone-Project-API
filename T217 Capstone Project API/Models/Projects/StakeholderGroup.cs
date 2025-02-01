using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Risks;

namespace T217_Capstone_Project_API.Models.Projects
{
    public class StakeholderGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StakeholderGroupID { get; set; }

        [ForeignKey(nameof(Project.ProjectID))]
        [Required]
        public int ProjectID { get; set; }

        public Project Project { get; set; }
    }
}
