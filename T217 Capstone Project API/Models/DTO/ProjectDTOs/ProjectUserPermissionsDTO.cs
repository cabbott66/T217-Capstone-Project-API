namespace T217_Capstone_Project_API.Models.DTO
{
    public class ProjectUserPermissionsDTO
    {
        public bool CanRead { get; set; } = false;

        public bool CanWrite { get; set; } = false;

        public bool CanEdit { get; set; } = false;
    }
}
