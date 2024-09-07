using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T217_Capstone_Project_API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserID { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserFirstName { get; set; }

        [Required]
        public string UserLastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ApiKey { get; set; } = Guid.NewGuid().ToString();
    }
}
 