namespace T217_Capstone_Project_API.Models.Projects
{
    public class ProjectUser
    {
        public int ProjectUserID { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public int PermissionLevel { get; set; }
    }
}
