using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json;
using T217_Capstone_Project_API.Models;
using T217_Capstone_Project_API.Models.Projects;

namespace T217_Capstone_Project_API.Controllers
{
    public static class JsonManager
    {
        public static List<Stakeholder>? StakeholderList { get; set; }

        public static void SaveList()
        {
            string stakeholderJson = JsonSerializer.Serialize(StakeholderList);
            File.WriteAllText(@"StakeholderList.json", stakeholderJson);
        }

        public static void LoadList()
        {
            if (File.Exists(@"StakeholderList.json") && File.Exists(@"UserList.json"))
            {
                string stakeholderJsonFile = File.ReadAllText(@"StakeholderList.json");

                StakeholderList = JsonSerializer.Deserialize<List<Stakeholder>>(stakeholderJsonFile);
            }
            else StakeholderList = new List<Stakeholder>();
        }
    }
}
