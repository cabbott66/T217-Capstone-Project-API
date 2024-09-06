namespace T217_Capstone_Project_API.Models.Projects
{
    public class Stakeholder
    {
        public int StakeholderID { get; set; }
        public int StakeholderGroupID { get; set; }
        public string StakeholderName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime EditDateTime { get; set; }
        public string CaFI {  get; set; }

        public string TestData { get; set; }
    }
}
