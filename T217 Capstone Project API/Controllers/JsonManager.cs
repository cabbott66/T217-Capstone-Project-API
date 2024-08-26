using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json;
using T217_Capstone_Project_API.Models;

namespace T217_Capstone_Project_API.Controllers
{
    public static class JsonManager
    {
        public static List<Stakeholder>? StakeholderList { get; set; }
        readonly static string path = @"StakeholderList.json";

        public static void SaveList()
        {
            string json = JsonSerializer.Serialize(StakeholderList);
            File.WriteAllText(path, json);
        }

        public static void LoadList()
        {
            if (File.Exists(path))
            {
                string file = File.ReadAllText(path);

                StakeholderList = JsonSerializer.Deserialize<List<Stakeholder>>(file);
            }
            else StakeholderList = new List<Stakeholder>();
        }
    }
}
