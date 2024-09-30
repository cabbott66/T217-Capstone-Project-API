using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace T217_Capstone_Project_API.Models.Projects
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ProjectID { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime EditDateTime { get; set; } = DateTime.Now;

        [Required]
        [DefaultValue("Open")]
        public string Status { get; set; }
    }
}
