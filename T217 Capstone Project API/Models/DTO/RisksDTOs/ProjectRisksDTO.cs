using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class ProjectRisksDTO
    {
        public int ProjectID { get; set; }

        public int TypeOfChange { get; set; }

        public int ProjectLength { get; set; }

        public int CulturalAlignment { get; set; }

        public int Resourcing {  get; set; }

        public int ProjectGoals { get; set; }

        public int Priority { get; set; }
    }
}
