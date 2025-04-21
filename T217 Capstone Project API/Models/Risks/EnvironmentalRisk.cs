using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;
using System.Text.Json.Serialization;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class EnvironmentalRisk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int EnvironmentalRiskID { get; set; }

        [ForeignKey(nameof(Stakeholder.StakeholderID))]
        [Required]
        public int StakeholderID { get; set; }

        [JsonIgnore]
        public Stakeholder Stakeholder { get; set; }

        public int ChangeVolume { get; set; }

        public int Infrastructure {  get; set; }

        public int Industry { get; set; }

        public int OfficePolitics { get; set; }
    }
}
