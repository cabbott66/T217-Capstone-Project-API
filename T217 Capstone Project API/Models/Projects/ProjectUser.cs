using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace T217_Capstone_Project_API.Models.Projects
{
    public class ProjectUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ProjectUserID { get; set; }

        [ForeignKey(nameof(User.UserID))]
        [Required]
        public int UserID { get; set; }

        [Required]
        [JsonIgnore]
        public User User { get; set; }

        // Foreign Key
        [ForeignKey(nameof(Project.ProjectID))]
        [Required]
        public int ProjectID { get; set; }

        [Required]
        [JsonIgnore]
        public Project Project { get; set; }

        // Permissions
        [Required]
        [DefaultValue(false)]
        public bool CanRead { get; set; } = false;

        [Required]
        [DefaultValue(false)]
        public bool CanWrite { get; set; } = false;

        [Required]
        [DefaultValue(false)]
        public bool CanEdit { get; set; } = false;

        [Required]
        [DefaultValue(false)]
        public bool IsAdmin { get; set; } = false;
    }
}
