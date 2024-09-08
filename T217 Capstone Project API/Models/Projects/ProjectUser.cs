namespace T217_Capstone_Project_API.Models.Projects
{
    public class ProjectUser
    {
        public int ProjectUserID { get; set; }
        public int UserID { get; set; }
        public Project Project { get; set; }

        // [Read][Write][Edit]
        public IEnumerable<int> PermissionLevel { get; set; }
        public bool IsAdmin { get; set; }
    }
}
