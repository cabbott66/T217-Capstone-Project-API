using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Risks;
using System.Text.Json.Serialization;

namespace T217_Capstone_Project_API.Models.Projects
{
    public class StakeholderGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StakeholderGroupID { get; set; }

        [Required]
        public string StakeholderGroupName { get; set; }

        [ForeignKey(nameof(Project.ProjectID))]
        [Required]
        public int ProjectID { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }

        public int BlobID { get; set; }
    }
}
