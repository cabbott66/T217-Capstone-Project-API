namespace T217_Capstone_Project_API.Models.DTO
{
    public class ProjectUserDTO
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public bool CanRead { get; set; } = false;
        public bool CanWrite { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
