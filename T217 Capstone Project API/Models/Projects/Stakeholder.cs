using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Risks;
using System.Text.Json.Serialization;

namespace T217_Capstone_Project_API.Models.Projects
{
    public class Stakeholder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StakeholderID { get; set; }

        //[ForeignKey(nameof(StakeholderGroup.StakeholderGroupID))]
        //[Required]
        //public int StakeholderGroupID { get; set; }

        //public StakeholderGroup StakeholderGroup { get; set; }

        public string StakeholderName { get; set; }
        [Required]

        [ForeignKey(nameof(Project.ProjectID))]
        [Required]

        public int ProjectID { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime EditDateTime { get; set; }
        public string CaFI {  get; set; }

        public string TestData { get; set; }
    }
}
