using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace T217_Capstone_Project_API.Models.DTO.UserDTOs
{
    public class CreateUserDTO
    {
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Password { get; set; }
    }
}
