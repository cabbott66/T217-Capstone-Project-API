﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using T217_Capstone_Project_API.Models.Projects;
using System.Text.Json.Serialization;

namespace T217_Capstone_Project_API.Models.Risks
{
    public class InterpersonalRisks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int InterpersonalRiskID { get; set; }

        [ForeignKey(nameof(Stakeholder.StakeholderID))]
        [Required]
        public int StakeholderID { get; set; }

        [JsonIgnore]
        public Stakeholder? Stakeholder { get; set; }

        public int Support { get; set; }

        public int SupportiveManagement { get; set; }

        public int Trust {  get; set; }

        public int Respect { get; set; }

        public int Communication { get; set; }

        public int SharingSuccess { get; set; }
    }
}
