using System.ComponentModel.DataAnnotations.Schema;

namespace T217_Capstone_Project_API.Models.DTO
{
    public class StakeholderGroupDTO
    {
        public string StakeholderGroupName { get; set; }
        public int ProjectID { get; set; }
        public int BlobID { get; set; }
    }
}
