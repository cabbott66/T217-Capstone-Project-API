using T217_Capstone_Project_API.Controllers;
using T217_Capstone_Project_API.Models;

namespace T217_Capstone_Project_API.Repositories
{
    public class JsonStakeholderRepository
    {
        public void CreateStakeholder(Stakeholder stakeholder)
        {
            if (stakeholder != null)
            {
                JsonManager.LoadList();
                JsonManager.StakeholderList.Add(stakeholder);
                JsonManager.SaveList();
            }
        }

        public void DeleteStakeholder(int id)
        {
            JsonManager.LoadList();
            JsonManager.StakeholderList.RemoveAll(x => x.StakeholderID == id);
            JsonManager.SaveList();
        }

        public Stakeholder GetStakeHolder(int id)
        {
            JsonManager.LoadList();
            Stakeholder sh = JsonManager.StakeholderList.FirstOrDefault(x => x.StakeholderID == id);
            return sh;
        }

        public List<Stakeholder> GetStakeHoldersList()
        {
            JsonManager.LoadList();
            return JsonManager.StakeholderList;
        }

        public void UpdateStakeholder(int id, Stakeholder newStakeholder)
        {
            JsonManager.LoadList();
            int index = JsonManager.StakeholderList.FindIndex(x => x.StakeholderID == id);
            JsonManager.StakeholderList[index] = newStakeholder;
            JsonManager.SaveList();
        }
    }
}
