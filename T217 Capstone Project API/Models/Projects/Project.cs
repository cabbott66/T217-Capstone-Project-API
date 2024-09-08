namespace T217_Capstone_Project_API.Models.Projects
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime EditDateTime { get; set; } = DateTime.Now;
        public string Status { get; set; }
    }
}
