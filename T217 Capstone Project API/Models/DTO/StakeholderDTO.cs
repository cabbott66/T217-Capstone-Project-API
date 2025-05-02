using System.ComponentModel.DataAnnotations.Schema;

namespace T217_Capstone_Project_API.Models.DTO
{
    public class StakeholderDTO
    {
        public string StakeholderName { get; set; }
        public int ProjectID { get; set; }
        public string CaFI {  get; set; }
        public int BlobID { get; set; }
        public string Description { get; set; }
    }
}
