using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Risks;

namespace T217_Capstone_Project_API.Models.Projects
{
    public class Stakeholder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StakeholderID { get; set; }

        [ForeignKey(nameof(StakeholderGroup.StakeholderGroupID))]
        [Required]
        public int StakeholderGroupID { get; set; }

        public StakeholderGroup StakeholderGroup { get; set; }

        public string StakeholderName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime EditDateTime { get; set; }
        public string CaFI {  get; set; }

        public string TestData { get; set; }
    }
}
