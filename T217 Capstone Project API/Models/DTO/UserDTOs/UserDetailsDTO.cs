namespace T217_Capstone_Project_API.Models.DTO.UserDTOs
{
    public class UserDetailsDTO
    {
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public UserDetailsDTO(string userEmail, string userFirstName, string userLastName)
        {
            UserEmail = userEmail;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
        }
    }
}
